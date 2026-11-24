using Microsoft.EntityFrameworkCore;
using MultiShop.Comments.Entities;

namespace MultiShop.Comments.Context;

public class CommentContext(DbContextOptions<CommentContext> options) : DbContext(options)
{
    public DbSet<UserComment> UserComments => Set<UserComment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserComment>(entity =>
        {
            entity.HasKey(x => x.UserCommentId);
            entity.Property(x => x.NameSurname).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(200).IsRequired();
            entity.Property(x => x.CommentDetail).HasMaxLength(2000).IsRequired();
            entity.Property(x => x.ProductId).HasMaxLength(100).IsRequired();
            entity.Property(x => x.ImageUrl).HasMaxLength(500);
        });
    }
}
