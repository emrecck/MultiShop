using Microsoft.EntityFrameworkCore;
using MultiShop.Comments.Context;
using MultiShop.Comments.Entities;

namespace MultiShop.Comments.Repositories;

public class CommentRepository(CommentContext context) : ICommentRepository
{
    public async Task<List<UserComment>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.UserComments.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<List<UserComment>> GetByProductIdAsync(string productId, CancellationToken cancellationToken) =>
        await context.UserComments
            .AsNoTracking()
            .Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);

    public async Task<UserComment?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await context.UserComments.FirstOrDefaultAsync(x => x.UserCommentId == id, cancellationToken);

    public async Task<UserComment> CreateAsync(UserComment comment, CancellationToken cancellationToken)
    {
        context.UserComments.Add(comment);
        await context.SaveChangesAsync(cancellationToken);
        return comment;
    }

    public async Task<bool> UpdateAsync(UserComment comment, CancellationToken cancellationToken)
    {
        var existing = await context.UserComments
            .FirstOrDefaultAsync(x => x.UserCommentId == comment.UserCommentId, cancellationToken);
        if (existing is null)
            return false;

        existing.NameSurname = comment.NameSurname;
        existing.ImageUrl = comment.ImageUrl;
        existing.Email = comment.Email;
        existing.CommentDetail = comment.CommentDetail;
        existing.Rating = comment.Rating;
        existing.CreatedDate = comment.CreatedDate;
        existing.Status = comment.Status;
        existing.ProductId = comment.ProductId;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var existing = await context.UserComments
            .FirstOrDefaultAsync(x => x.UserCommentId == id, cancellationToken);
        if (existing is null)
            return false;

        context.UserComments.Remove(existing);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<int> GetTotalCountAsync(CancellationToken cancellationToken) =>
        context.UserComments.CountAsync(cancellationToken);

    public Task<int> GetActiveCountAsync(CancellationToken cancellationToken) =>
        context.UserComments.CountAsync(x => x.Status, cancellationToken);

    public Task<int> GetPassiveCountAsync(CancellationToken cancellationToken) =>
        context.UserComments.CountAsync(x => !x.Status, cancellationToken);
}
