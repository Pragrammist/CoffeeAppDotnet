using EFCore;
using EFCore.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Services.Common;
using Services.Contracts;
using Services.Dtos.Comment;
using EFCore.Heplers.IQueryableHelpers;
using Domain.Exceptions;
using Domain.Enums;

namespace Services
{
    public class CommentService : DbServiceBase<CommentDetailsDto, Comment>, ICommentService
    {
        public CommentService(IRepository repository) : base(repository)
        {

        }




        

        public async Task<CommentDetailsDto> CreateComment(CreateCommentDto data) 
        {
            var isUserCommentAlreadyExisting = await _dbRepository.Context.Comments
            .Include(u => u.User)
            .AnyAsync(c => 
                c.CoffeeId == data.CoffeeId &&
                // если пользователь с обычной ролью уже до этого создал свой комментарий
                c.UserId == data.UserId &&
                c.User.Role == UserRole.USER
            );



            if (isUserCommentAlreadyExisting)
                throw new CommentAlreadyCreatedByUserExcepion($"user id : {data.UserId}");


            return await base.Create(data);

        }
        

        public async Task DeleteComment(DeleteCommentDto data)
        {
            var userDeleteSelfComment = await _dbRepository.Context.Comments
                .Include(c => c.User)
                .AnyAsync(c => c.Id == data.CommentId && 
                    c.UserId == data.CurrentUserId);

            var canDelete = data.CurrentUserRole == UserRole.MODERATOR || userDeleteSelfComment;


            if (!canDelete)
                throw new PermissionDeniedException($"you cannot delete this comment", 410);

            await base.Delete(data.CommentId);
        }

        
        public async Task<CommentDetailsDto> EditComment(EditCommentDto data)
        {
            var isCommentExistsAndUserCanChangeComment = await _dbRepository.Context.Comments
            .Include(u => u.User)
            .AnyAsync(c => 
                c.Id == data.Id &&
                (
                    //если пользователь модератор
                    c.User.Role == UserRole.MODERATOR ||    

                    //если пользователь редактирует свой комментарий
                    c.UserId == data.UserId &&
                    c.User.Role == UserRole.USER
                )
            );


            if (!isCommentExistsAndUserCanChangeComment)
                throw new PermissionDeniedException("You can't edit this comment", 408);

            //чтобы не вылетело исключение, нужно сущность "открепить",
            //так как используется метод Attach
            var attachedComment = _dbRepository.Context.ChangeTracker.Entries<Comment>().First(c => c.Entity.Id == data.Id);

            attachedComment.State = EntityState.Detached;



            var commentEnt = data.Adapt<Comment>();

            _dbRepository.Context.Attach(commentEnt);

            return await base.Edit(data);
        }

        public async Task<CommentDetailsDto> GetCommentDetails(int id) => 
            (
                await _dbRepository.Context.Comments
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.CommentToAnswer).GetByIdAsync(id)
            ).Adapt<CommentDetailsDto>();


        public IEnumerable<CommentDto> GetComments() =>
            _dbRepository.Context.Comments.Include(c => c.User)
            .ProjectToType<CommentDto>();


        public async Task<CommentDetailsDto> CreateAnswerToComment(CreateAnswerToCommentDto data)
        {
            var comment = await _dbRepository.Context.Comments
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.CommentToAnswer).ThenInclude(a => a != null ? a.User : null)
                .FirstOrDefaultAsync(c => c.Id == data.CommentId);

            if(comment is null)
                throw new NotFoundException(data.CommentId);

            var commentIsAnswerFromModeratorToCurrentUser = 
                    comment.User.Role == UserRole.MODERATOR
                    && comment.CommentToAnswer != null && comment.CommentToAnswer.User.Id == data.UserId;
            
            data.CoffeeId = comment.CoffeeId;

            var canAnswer = data.CurrentUserRole == UserRole.MODERATOR || commentIsAnswerFromModeratorToCurrentUser;

            if (!canAnswer)
                throw new PermissionDeniedException($"you cant't answer to this comment", 409);



            return await base.Create(data);
        }
    }
}
