using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class Followers
    {
        [Key]
        public string Id { get; set; }

        //[ForeignKey("FollowerUser")]
        //public string FollowerId { get; set; }
        //public Users FollowerUser { get; set; }


        //[ForeignKey("FolloweeUser")]
        //public string FolloweeId { get; set; }
        //public Users FolloweeUser { get; set; }

        public DateOnly CreatedAt { get; set; }

    }
}
