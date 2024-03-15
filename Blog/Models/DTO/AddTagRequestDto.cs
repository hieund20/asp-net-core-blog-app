using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.DTO
{
    public class AddTagRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
