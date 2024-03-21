using Azure;
using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositorise
{
    public class SQLPostTagRepository : IPostTagRepository
    {
        private readonly BlogDBContext dBContext;

        public SQLPostTagRepository(BlogDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<PostTag> CreateAsync(PostTag postTag)
        {
            await dBContext.PostTags.AddAsync(postTag);
            await dBContext.SaveChangesAsync();
            return postTag;
        }

        public async Task<PostTag?> DeleteAsync(Guid postId, Guid tagId)
        {
            var existingPostTag = await dBContext.PostTags.FirstOrDefaultAsync(x => x.TagId == tagId && x.PostId == postId);

            if (existingPostTag == null)
            {
                return null;
            }

            dBContext.PostTags.Remove(existingPostTag);
            await dBContext.SaveChangesAsync();
            return existingPostTag;
        }

        public async Task<List<PostTag>> GetAllAsync()
        {
            var postTags = await dBContext.PostTags
                .Include(x => x.Post)
                .Include(x => x.Tag).ToListAsync();
            return postTags;
        }

        public async Task<PostTag?> GetByIdAsync(Guid postId, Guid tagId)
        {
            return await dBContext.PostTags.FirstOrDefaultAsync(x => x.TagId == tagId && x.PostId == postId);
        }

        public async Task<List<PostTag>> GetAllByPostIdAsync(Guid postId)
        {
            return await dBContext.PostTags.Where(x => x.PostId == postId).Include(x => x.Post).Include(x => x.Tag).ToListAsync();
        }

        public async Task<PostTag?> UpdateAsync(Guid postId, Guid tagId, PostTag postTag)
        {
            var existingPostTag = await dBContext.PostTags.FirstOrDefaultAsync(x => x.TagId == tagId && x.PostId == postId);

            if (existingPostTag == null)
            {
                return null;
            }

            existingPostTag.PostId = postTag.PostId;
            existingPostTag.TagId = postTag.TagId;

            await dBContext.SaveChangesAsync();

            return existingPostTag;
        }
    }
}
