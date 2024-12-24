using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        public string UserId { get; set; }
        public virtual User Users { get; set; }

        [ForeignKey("Post")]
        public int Post_Id { get; set; }

        public virtual Post Post { get; set; } 




    }
}
