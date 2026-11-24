using MultiShop.Comments.Dtos;
using MultiShop.Comments.Entities;
using MultiShop.Comments.Repositories;

namespace MultiShop.Comments.Services;

public class CommentService(ICommentRepository repository) : ICommentService
{
    public async Task<List<ResultCommentDto>> GetAllCommentAsync(CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(cancellationToken);
        return items.Select(MapToResult).ToList();
    }

    public async Task<List<ResultCommentDto>> CommentListByProductIdAsync(string productId, CancellationToken cancellationToken)
    {
        var items = await repository.GetByProductIdAsync(productId, cancellationToken);
        return items.Select(MapToResult).ToList();
    }

    public async Task<GetByIdCommentDto?> GetByIdCommentAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, cancellationToken);
        return item is null ? null : MapToGetById(item);
    }

    public async Task<GetByIdCommentDto> CreateCommentAsync(CreateCommentDto dto, CancellationToken cancellationToken)
    {
        var entity = new UserComment
        {
            NameSurname = dto.NameSurname,
            ImageUrl = dto.ImageUrl,
            Email = dto.Email,
            CommentDetail = dto.CommentDetail,
            Rating = dto.Rating,
            CreatedDate = dto.CreatedDate == default ? DateTime.UtcNow : dto.CreatedDate,
            Status = dto.Status,
            ProductId = dto.ProductId
        };

        var created = await repository.CreateAsync(entity, cancellationToken);
        return MapToGetById(created);
    }

    public async Task<bool> UpdateCommentAsync(UpdateCommentDto dto, CancellationToken cancellationToken)
    {
        var entity = new UserComment
        {
            UserCommentId = dto.UserCommentId,
            NameSurname = dto.NameSurname,
            ImageUrl = dto.ImageUrl,
            Email = dto.Email,
            CommentDetail = dto.CommentDetail,
            Rating = dto.Rating,
            CreatedDate = dto.CreatedDate,
            Status = dto.Status,
            ProductId = dto.ProductId
        };

        return await repository.UpdateAsync(entity, cancellationToken);
    }

    public Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken) =>
        repository.DeleteAsync(id, cancellationToken);

    public Task<int> GetTotalCommentCountAsync(CancellationToken cancellationToken) =>
        repository.GetTotalCountAsync(cancellationToken);

    public Task<int> GetActiveCommentCountAsync(CancellationToken cancellationToken) =>
        repository.GetActiveCountAsync(cancellationToken);

    public Task<int> GetPassiveCommentCountAsync(CancellationToken cancellationToken) =>
        repository.GetPassiveCountAsync(cancellationToken);

    private static ResultCommentDto MapToResult(UserComment x) => new()
    {
        UserCommentId = x.UserCommentId,
        NameSurname = x.NameSurname,
        ImageUrl = x.ImageUrl,
        Email = x.Email,
        CommentDetail = x.CommentDetail,
        Rating = x.Rating,
        CreatedDate = x.CreatedDate,
        Status = x.Status,
        ProductId = x.ProductId
    };

    private static GetByIdCommentDto MapToGetById(UserComment x) => new()
    {
        UserCommentId = x.UserCommentId,
        NameSurname = x.NameSurname,
        ImageUrl = x.ImageUrl,
        Email = x.Email,
        CommentDetail = x.CommentDetail,
        Rating = x.Rating,
        CreatedDate = x.CreatedDate,
        Status = x.Status,
        ProductId = x.ProductId
    };
}
