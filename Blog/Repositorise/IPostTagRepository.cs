using Blog.API.Models.Domain;

namespace Blog.API.Repository
{
    public interface IPostTagRepository
    {
        Task<PostTag> CreateAsync(PostTag postTag);
        Task<List<PostTag>> GetAllAsync();
        Task<PostTag?> GetByIdAsync(Guid postId, Guid tagId);
        Task<List<PostTag>> GetAllByPostIdAsync(Guid postId);
        Task<PostTag?> UpdateAsync(Guid postId, Guid tagId, PostTag postTag);
        Task<PostTag?> DeleteAsync(Guid postId, Guid tagId);
    }
}
