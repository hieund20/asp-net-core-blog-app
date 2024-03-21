using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class AddPostTagRequestDto
    {
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public Guid TagId { get; set; }
    }
}
