using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositorise
{
    public class SQLPostRepository : IPostRepository
    {
        private readonly BlogDBContext dBContext;

        public SQLPostRepository(BlogDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Post> CreateAsync(Post post)
        {
            post.CreatedDate = DateTime.Now;

            await dBContext.Posts.AddAsync(post);
            await dBContext.SaveChangesAsync();
            return post;
        }

        public async Task<Post?> DeleteAsync(Guid id)
        {
            var existingPost = await dBContext.Posts.FirstOrDefaultAsync(x => x.PostId == id);

            if (existingPost == null)
            {
                return null;
            }

            dBContext.Posts.Remove(existingPost);
            await dBContext.SaveChangesAsync();
            return existingPost;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            var posts = await dBContext.Posts.ToListAsync();
            return posts;
        }

        public  async Task<Post?> GetByIdAsync(Guid id)
        {
            return await dBContext.Posts.FirstOrDefaultAsync(x => x.PostId == id);
        }

        public async Task<Post?> UpdateAsync(Guid id, Post post)
        {
            var existingPost = await dBContext.Posts.FirstOrDefaultAsync(x => x.PostId == id);

            if (existingPost == null)
            {
                return null;
            }

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.UpdatedDate = DateTime.Now;

            await dBContext.SaveChangesAsync();

            return existingPost;
        }
    }
}
