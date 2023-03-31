using System.ComponentModel.DataAnnotations;

namespace crudone.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
