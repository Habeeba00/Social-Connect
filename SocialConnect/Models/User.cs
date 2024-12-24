using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class User:IdentityUser
    {
        #region User Profile Details
        public string Bio { get; set; }
        public string Address { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        public string Relationship  { get; set; }
        public string Education { get; set; }

        #endregion

        public virtual ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();
        public virtual ICollection<UserFollower> Following { get; set; } = new List<UserFollower>();

        public virtual ICollection<Post> Posts { get; set; }=new List<Post>();
        public virtual ICollection<Comment> UserComments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
