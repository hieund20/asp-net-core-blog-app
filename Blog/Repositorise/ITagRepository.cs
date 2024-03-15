using Blog.API.Models.Domain;

namespace Blog.API.Repository
{
    public interface ITagRepository
    {
        Task<Tag> CreateAsync(Tag tag);
        Task<List<Tag>> GetAllAsync();
        Task<Tag?> GetByIdAsync(Guid id);
        Task<Tag?> UpdateAsync(Guid id, Tag tag);
        Task<Tag?> DeleteAsync(Guid id);
    }
}
