using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [ForeignKey("UserPost")]
        public string UserId { get; set; }
        public virtual User UserPost { get; set; }

        public string Content { get; set; }
        public int LikesCount  { get; set; }
        public int CommentsCount { get; set; }

        public DateOnly CreatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;




        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();

    }
}
