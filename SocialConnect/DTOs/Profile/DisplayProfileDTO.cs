using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SocialConnect.DTOs.Like;
using SocialConnect.DTOs.Follower;

namespace SocialConnect.DTOs.User
{
    public class DisplayProfileDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Bio { get; set; }
        public string Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        public string Relationship { get; set; }
        public string Education { get; set; }
        public string Gender { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public List<DisplayFollowerDTO> Followers { get; set; }
        public List<DisplayFollowingDTO> Following { get; set; }

    }
}
