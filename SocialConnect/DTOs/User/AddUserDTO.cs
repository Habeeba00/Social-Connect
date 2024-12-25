using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.DTOs.User
{
    public class AddUserDTO
    {
       public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }

        public string Bio { get; set; }
        public string Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        public string Relationship { get; set; }
        public string Education { get; set; }


    }
}
