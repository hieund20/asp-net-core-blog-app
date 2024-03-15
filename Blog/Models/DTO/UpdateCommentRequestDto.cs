using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class UpdateCommentRequestDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public Guid PostId { get; set; }
    }
}
