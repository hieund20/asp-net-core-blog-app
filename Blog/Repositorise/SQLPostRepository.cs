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

        public async Task<List<Post>> GetAllAsync(string? filterOn = null, 
                                                string? filterQuery = null, 
                                                string? sortBy = null, 
                                                bool isAscending = true, 
                                                int pageNumber = 1, 
                                                int pageSize = 6)
        {
            var posts = dBContext.Posts.AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    posts = posts.Where(x => x.Title.Contains(filterQuery));
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    posts = isAscending ? posts.OrderBy(x => x.Title) : posts.OrderByDescending(x => x.Title);
                }
                else if (sortBy.Equals("CreatedDate", StringComparison.OrdinalIgnoreCase))
                {
                    posts = isAscending ? posts.OrderBy(x => x.CreatedDate) : posts.OrderByDescending(x => x.CreatedDate);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await posts.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public  async Task<Post?> GetByIdAsync(Guid id)
        {
            return await dBContext.Posts.FirstOrDefaultAsync(x => x.PostId == id);
        }

        public async Task<int> GetTotalAsync()
        {
            return await dBContext.Posts.CountAsync();
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
