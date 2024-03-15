using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositorise
{
    public class SQLCommentRepository : ICommentRepository
    {
        private readonly BlogDBContext dBContext;

        public SQLCommentRepository(BlogDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            comment.CreateDate = DateTime.Now;
            await dBContext.Comments.AddAsync(comment);
            await dBContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(Guid id)
        {
            var existingComment = await dBContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (existingComment == null)
            {
                return null;
            }

            dBContext.Comments.Remove(existingComment);
            await dBContext.SaveChangesAsync();
            return existingComment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await dBContext.Comments.ToListAsync();
            return comments;
        }

        public async Task<List<Comment>> GetAllByPostIdAsync(Guid PostId)
        {
            var comments = await dBContext.Comments.Where(x => x.PostId == PostId).ToListAsync();
            return comments;
        }

        public  async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await dBContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
        }

        public async Task<Comment?> UpdateAsync(Guid id, Comment comment)
        {
            var existingComment = await dBContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Content = comment.Content;

            await dBContext.SaveChangesAsync();

            return existingComment;
        }
    }
}
