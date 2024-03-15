using Microsoft.AspNetCore.Identity;

namespace Blog.API.Models.Domain
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

        public Post Post { get; set; }
        public IdentityUser User { get; set; }
    }
}
