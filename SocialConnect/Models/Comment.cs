using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Comment
    {

        [Key]
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("UserComment")]
        public string User_Id { get; set; }
        public virtual User UserComment { get; set; }

        [ForeignKey("Posts")]
        public int Post_Id { get; set; }
        public virtual Post Posts { get; set; }

    }
}
