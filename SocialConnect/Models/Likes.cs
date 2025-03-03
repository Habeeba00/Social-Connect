using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Likes
    {
        [Key]
        public string LikeId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("UserLikes")]
        public string UserId { get; set; }
        public virtual User UserLikes { get; set; }

        [ForeignKey("Post")]
        public string Post_Id { get; set; }

        public virtual Post Post { get; set; }
        public bool IsDeleted { get; set; } = false;




    }
}
