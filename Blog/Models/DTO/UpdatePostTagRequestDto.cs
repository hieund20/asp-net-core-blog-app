using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class UpdatePostTagRequestDto
    {
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public Guid TagId { get; set; }
    }
}
