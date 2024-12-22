using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Comments
    {

        [Key]
        public string CommentsId { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }
        public virtual Users Users { get; set; }
        public string CommentContent { get; set; }
        public DateTime DateTime { get; set; }


        [ForeignKey("Posts")]
        public string PostsId { get; set; }

        public virtual Posts Posts { get; set; }
    }
}
