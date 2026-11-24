using MultiShop.Comments.Entities;

namespace MultiShop.Comments.Repositories;

public interface ICommentRepository
{
    Task<List<UserComment>> GetAllAsync(CancellationToken cancellationToken);
    Task<List<UserComment>> GetByProductIdAsync(string productId, CancellationToken cancellationToken);
    Task<UserComment?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<UserComment> CreateAsync(UserComment comment, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(UserComment comment, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken);
    Task<int> GetActiveCountAsync(CancellationToken cancellationToken);
    Task<int> GetPassiveCountAsync(CancellationToken cancellationToken);
}
