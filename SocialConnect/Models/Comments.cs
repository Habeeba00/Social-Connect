using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Comment
    {

        [Key]

        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("UserComment")]
        public string UserId { get; set; }
        public virtual User UserComment { get; set; }

        [ForeignKey("Posts")]
        public int Post_Id { get; set; }
        public virtual Post Posts { get; set; }
        public bool IsDeleted { get; set; } = false; // Default to not deleted


    }
}
