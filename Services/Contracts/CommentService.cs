using Services.Dtos.Comment;


namespace Services.Contracts
{
    public interface ICommentService
    {
        public Task<CommentDetailsDto> CreateComment(CreateCommentDto data);

        public Task<CommentDetailsDto> EditComment(EditCommentDto data);

        public Task DeleteComment(DeleteCommentDto data);

        public Task<CommentDetailsDto> GetCommentDetails(int id);

        public IEnumerable<CommentDto> GetComments();

        public Task<CommentDetailsDto> CreateAnswerToComment(CreateAnswerToCommentDto data);

    }
}
