using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class AddPostRequestDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
