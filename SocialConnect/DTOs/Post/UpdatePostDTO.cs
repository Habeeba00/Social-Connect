using System.ComponentModel.DataAnnotations;

namespace SocialConnect.DTOs.Post
{
    public class UpdatePostDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Content must be between 10 and 1000 characters.")]
        public string Content { get; set; }


    }
}
