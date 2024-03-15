using System.ComponentModel.DataAnnotations;

namespace Blog.UI.Models
{
    public class AddCommentViewModel
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
    }
}
