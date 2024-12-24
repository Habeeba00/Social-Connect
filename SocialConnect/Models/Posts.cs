using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Posts
    {
        [Key]
        public string PostsId { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }
        public virtual Users Users { get; set; }

        public string Content { get; set; }
        public int LikesCount  { get; set; }
        public int CommentsCount { get; set; }

        public DateOnly CreatedAt { get; set; }



        public virtual ICollection<Comments> PostsComments { get; set; } = new List<Comments>();
        public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();

    }
}
