using System.Security.Claims;
using Domain.Enums;
using Domain.Exceptions;
using Host.Infrastructure.Consts;
using Host.Models.Coffees;
using Host.Models.Comments;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Dtos.Comment;
using Services.Dtos.Files;

namespace Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CommentController : Controller
    {
        readonly ICommentService _commentService;
        readonly IFileService _fileService;
        public CommentController(ICommentService commentService, IFileService fileService) 
        {
            _commentService = commentService;
            _fileService = fileService;
        }

        [Authorize]
        [HttpPost]
        public async  Task<IActionResult> CreateComment(CreateCommentModel createCommentModel) 
        {
            if(createCommentModel.Photos is not null)
                await WriteFiles(createCommentModel.Photos);
            
            var comment = createCommentModel.Adapt<CreateCommentDto>();
            comment.UserId = int.Parse(User.FindFirstValue(ClaimsConst.ID) ?? throw new PermissionDeniedException(createCommentModel.Text, 400));
            
            var result = await _commentService.CreateComment(comment);
            return Ok( new
                {
                    result.Id
                }
            );
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id) 
        {
            var deleteUser =new DeleteCommentDto{
                CommentId = id,
                CurrentUserId = int.Parse(User.FindFirstValue(ClaimsConst.ID) ?? throw new PermissionDeniedException(id, 400)),
                CurrentUserRole = Enum.Parse<UserRole>(User.FindFirstValue(ClaimsConst.ROLE) ?? throw new PermissionDeniedException(id, 400))
            };
            await _commentService.DeleteComment(deleteUser);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditComment(EditCommentModel editCommentModel)
        {
            if(editCommentModel.Photos is not null)
                await WriteFiles(editCommentModel.Photos);
            
            var comment = editCommentModel.Adapt<EditCommentDto>();
            comment.UserId = int.Parse(User.FindFirstValue(ClaimsConst.ID) ?? throw new PermissionDeniedException(editCommentModel.Text, 400));
            
            await _commentService.EditComment(comment);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetComment(int id)
        {
            return Ok(
                await _commentService.GetCommentDetails(id)
            );
        }

        [HttpGet("list")]
        public IActionResult GetComments()
        {
            return Ok(
                _commentService.GetComments()
            );
        }

        [HttpPost("answer")]
        public async Task<IActionResult> AnswerToComment(CreateAnswerToCommentModel createAnswer)
        {
            var answer = createAnswer.Adapt<CreateAnswerToCommentDto>();
            answer.UserId = int.Parse(User.FindFirstValue(ClaimsConst.ID) ?? throw new PermissionDeniedException(createAnswer.Id, 400));
            answer.CurrentUserRole = Enum.Parse<UserRole>(User.FindFirstValue(ClaimsConst.ROLE) ?? throw new PermissionDeniedException(createAnswer.Id, 400));
            var result = await _commentService.CreateAnswerToComment(
                answer
            );
            return Ok(new
                {
                    result.Id
                }
            );
        }

        async Task WriteFiles(IEnumerable<IFormFile> files)
        {
            var filesData = files.Select(s => new WriteFileDto {FileData = s.OpenReadStream(), Name = s.FileName });
            var filesNames = files.Select(s => s.Name);
            await _fileService.WriteFiles(filesData);
        }
    }
}
