using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class UpdateTagRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
