using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserPost")]
        public string user_id { get; set; }
        public virtual User UserPost { get; set; }

        public string Content { get; set; }
        public int LikesCount  { get; set; }
        public int CommentsCount { get; set; }

        public DateOnly CreatedAt { get; set; }



        public virtual ICollection<Comment> PostComments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}
