using System.ComponentModel.DataAnnotations;

namespace HoteListing.API.Models.Users
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Password is limited to {2} to {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }
    }

}
