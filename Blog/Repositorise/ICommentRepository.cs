using Blog.API.Models.Domain;

namespace Blog.API.Repository
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(Guid id);
        Task<Comment?> UpdateAsync(Guid id, Comment comment);
        Task<Comment?> DeleteAsync(Guid id);
        Task<List<Comment>> GetAllByPostIdAsync(Guid PostId);
    }
}
