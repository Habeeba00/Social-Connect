using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Likes
    {
        [Key]
        public string LikeId { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }
        public virtual Users Users { get; set; }

        [ForeignKey("Posts")]
        public string PostsId { get; set; }

        public virtual Posts Posts { get; set; } 




    }
}
