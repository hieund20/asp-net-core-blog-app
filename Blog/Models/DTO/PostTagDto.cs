using Blog.API.Models.Domain;

namespace Blog.API.Models.DTO
{
    public class PostTagDto
    {
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }

        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
