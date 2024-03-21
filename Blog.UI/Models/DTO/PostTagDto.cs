namespace Blog.UI.Models.DTO
{
    public class PostTagDto
    {
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }
        public PostDto? Post { get; set; }
        public TagDto? Tag { get; set; }
    }
}
