using Blog.API.Models.Domain;

namespace Blog.API.Repository
{
    public interface IPostRepository
    {
        Task<Post> CreateAsync(Post post);
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(Guid id);
        Task<Post?> UpdateAsync(Guid id, Post post);
        Task<Post?> DeleteAsync(Guid id);
    }
}
