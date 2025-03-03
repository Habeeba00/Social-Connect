using SocialConnect.DTOs.Comment;
using SocialConnect.DTOs.Like;
using SocialConnect.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.DTOs.Post
{
    public class DisplayPostDTO
    {

        public string Username { get; set; }
        public string PostId { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<DisplayLikeDTO> Likes { get; set; }
        public List<DisplayCommentDTO> comments { get; set; }


    }
}
