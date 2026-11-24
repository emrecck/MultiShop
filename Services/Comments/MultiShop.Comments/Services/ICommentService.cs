using MultiShop.Comments.Dtos;

namespace MultiShop.Comments.Services;

public interface ICommentService
{
    Task<List<ResultCommentDto>> GetAllCommentAsync(CancellationToken cancellationToken);
    Task<List<ResultCommentDto>> CommentListByProductIdAsync(string productId, CancellationToken cancellationToken);
    Task<GetByIdCommentDto?> GetByIdCommentAsync(int id, CancellationToken cancellationToken);
    Task<GetByIdCommentDto> CreateCommentAsync(CreateCommentDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateCommentAsync(UpdateCommentDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteCommentAsync(int id, CancellationToken cancellationToken);
    Task<int> GetTotalCommentCountAsync(CancellationToken cancellationToken);
    Task<int> GetActiveCommentCountAsync(CancellationToken cancellationToken);
    Task<int> GetPassiveCommentCountAsync(CancellationToken cancellationToken);
}
