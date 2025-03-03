using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Post
    {
        [Key]
        public string PostId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("UserPost")]
        public string UserId { get; set; }  
        public virtual User UserPost{ get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
        public int LikesCount => Likes?.Count ?? 0;
        public int CommentsCount => Comments?.Count ?? 0;
        public DateTime? UpdatedAt { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;


        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();

    }
}
