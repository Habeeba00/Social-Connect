using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class User : IdentityUser
    {

        #region User Profile Details
        public string FullName { get; set; }

        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Bio { get; set; }
        public string? Address { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile? Picture { get; set; }
        public string? Relationship  { get; set; }
        public string? Education { get; set; }
        public enum GenderType
        {
            Male,
            Female,
            Unspecified
        }

        public GenderType? Gender { get; set; }
        #endregion
        public bool IsDeleted { get; set; } = false;
        public string? Photopath { get; set; }
        public int? FollowersCount =>Followers?.Count??0;
        public int? FollowingCount => Following?.Count ?? 0;

        public virtual ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();
        public virtual ICollection<UserFollower> Following { get; set; } = new List<UserFollower>();
        public virtual ICollection<Post> Posts { get; set; }=new List<Post>();
        public virtual ICollection<Comment> UserComments { get; set; } = new List<Comment>();
        public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();

    }
}
