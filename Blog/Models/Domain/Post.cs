using Microsoft.AspNetCore.Identity;

namespace Blog.API.Models.Domain
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
