using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class UserFollower
    {
        public Guid UserFollowerId { get; set; }

        [ForeignKey("Follower")]
        public string FollowerId { get; set; }
        public virtual User Follower { get; set; }

        [ForeignKey("FollowedUser")] 
        public string FollowedUserId { get; set; }
        public virtual User FollowedUser { get; set; }
        public bool IsDeleted { get; set; } = false;



    }
}
