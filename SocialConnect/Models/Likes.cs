using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Likes
    {
        [Key]
        public int LikeId { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }
        public virtual User Users { get; set; }

        [ForeignKey("Post")]
        public int Post_Id { get; set; }

        public virtual Post Post { get; set; }
        public bool IsDeleted { get; set; } = false;




    }
}
