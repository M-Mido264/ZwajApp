using System.ComponentModel.DataAnnotations;

namespace ZwajApp.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [StringLength(8,MinimumLength=4,ErrorMessage="Maximumlenght is 8 characters")]
        public string Password { get; set; }
    }
}