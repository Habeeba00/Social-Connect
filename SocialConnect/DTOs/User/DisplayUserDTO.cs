using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialConnect.DTOs.User
{
    public class DisplayUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        public string Relationship { get; set; }
        public string Education { get; set; }


    }
}
