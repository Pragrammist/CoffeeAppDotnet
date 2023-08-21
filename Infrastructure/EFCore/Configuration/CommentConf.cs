using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCore.Configuration
{
    public class CommentConf : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne<Comment>().WithMany().HasForeignKey(k => k.CommentId);
            builder.HasOne(c => c.Coffee).WithMany(c => c.Comments).HasForeignKey(k => k.CoffeeId);
            builder.HasOne(c => c.User).WithMany().HasForeignKey(k => k.UserId);
        }
    }
}
