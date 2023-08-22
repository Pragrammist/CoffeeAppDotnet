using EFCore.Models;
using Mapster;
using Services.Helpers;
using Services.Dtos.Comment;


namespace Services.MappingConfig
{
    public static class CommentMapConfig
    {
        public static void SetCommentMapping()
        {
            TypeAdapterConfig<Comment, CommentDto>.NewConfig().Map(d => d.Author, s => s.User.Login);

            TypeAdapterConfig<Comment, CommentDetailsDto>.NewConfig().Map(d => d.Photos, s => s.SplitPhotos());

            
            //.AfterMapping((source,dest) =>
            //{
            //dest.Photos = source.SplitPhotos();
            //});


        }
    }
}
