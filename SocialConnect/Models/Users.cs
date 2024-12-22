using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Users:IdentityUser
    {
        [Key]
        public string UserId { get; set; }

        #region User Profile Details
        public string Username { get; set; }
        public string Bio { get; set; }
        public string From { get; set; }
        public string CurrentCity { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
        public string Relationship  { get; set; }
        public string Education { get; set; }

        #endregion
      
        public virtual ICollection<Posts> Posts { get; set; }=new List<Posts>();
        public virtual ICollection<Comments> Comments { get; set; } = new List<Comments>();
        public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();



    }
}
