using Blog.API.Models.Domain;

namespace Blog.API.Repository
{
    public interface IPostRepository
    {
        Task<Post> CreateAsync(Post post);
        Task<List<Post>> GetAllAsync(string? filterOn = null,
                                    string? filterQuery = null,
                                    string? sortBy = null,
                                    bool isAscending = true,
                                    int pageNumber = 1,
                                    int pageSize = 6);
        Task<Post?> GetByIdAsync(Guid id);
        Task<Post?> UpdateAsync(Guid id, Post post);
        Task<Post?> DeleteAsync(Guid id);
        Task<int> GetTotalAsync();
    }
}
