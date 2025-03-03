using System.ComponentModel.DataAnnotations;

namespace SocialConnect.DTOs.Comment
{
    public class AddCommentDTO
    {
        [Required(ErrorMessage = "Comment content is required.")]
        [StringLength(500, ErrorMessage = "Comment content cannot be more than 500 characters.")]
        public string CommentContent { get; set; }

    }
}
