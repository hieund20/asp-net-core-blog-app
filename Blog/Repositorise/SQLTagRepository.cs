using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositorise
{
    public class SQLTagRepository : ITagRepository
    {
        private readonly BlogDBContext dBContext;

        public SQLTagRepository(BlogDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            await dBContext.Tags.AddAsync(tag);
            await dBContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await dBContext.Tags.FirstOrDefaultAsync(x => x.TagId == id);

            if (existingTag == null)
            {
                return null;
            }

            dBContext.Tags.Remove(existingTag);
            await dBContext.SaveChangesAsync();
            return existingTag;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            var tags = await dBContext.Tags.ToListAsync();
            return tags;
        }

        public  async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await dBContext.Tags.FirstOrDefaultAsync(x => x.TagId == id);
        }

        public async Task<Tag?> UpdateAsync(Guid id, Tag tag)
        {
            var existingTag = await dBContext.Tags.FirstOrDefaultAsync(x => x.TagId == id);

            if (existingTag == null)
            {
                return null;
            }

            existingTag.Name = tag.Name;

            await dBContext.SaveChangesAsync();

            return existingTag;
        }
    }
}
