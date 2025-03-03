using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.DTOs.Comment
{
    public class DisplayCommentDTO
    {
        public string Username { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
