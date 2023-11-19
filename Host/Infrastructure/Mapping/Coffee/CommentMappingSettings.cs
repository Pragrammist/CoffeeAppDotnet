using Mapster;
using Services.Dtos.Coffee;
using Host.Infrastructure.Helpers.PhotoDataHelpers;
using Host.Models.Comments;
using Services.Dtos.Comment;

namespace Host.Infrastructure.Mapping.Coffee
{
    public static class CommentMappingSettings
    {
        
        public static void SetCommentMapping()
        {
            TypeAdapterConfig<CreateCommentModel, CreateCommentDto>.NewConfig().Map(d => d.Photos, s => s.Photos != null ? s.Photos.JoinFileNames() : string.Empty); 

            TypeAdapterConfig<EditCommentModel, EditCommentModel>.NewConfig().Map(d => d.Photos, s => s.Photos != null ? s.Photos.JoinFileNames() : string.Empty); 

            
        }

        
    }
}