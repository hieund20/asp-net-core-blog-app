namespace Blog.UI.Models.DTO
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid PostId { get; set; }

        public PostDto Post { get; set; }
    }
}
