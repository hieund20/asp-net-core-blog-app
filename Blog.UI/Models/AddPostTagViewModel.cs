using Blog.UI.Models.DTO;

namespace Blog.UI.Models
{
    public class AddPostTagViewModel
    {
        public Guid SelectedPostId { get; set; }
        public Guid SelectedTagId { get; set; }
        public List<PostDto> Posts { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}
