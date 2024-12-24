using System.ComponentModel.DataAnnotations.Schema;

namespace SocialConnect.Models
{
    public class UserFollower
    {
        //public int Id { get; set; }

        // Foreign key for the follower
        [ForeignKey("Follower")] //Esraa
        public string FollowerId { get; set; }
        public virtual User Follower { get; set; }

        // Foreign key for the followed user
        [ForeignKey("FollowedUser")] //Habiba
        public string FollowedUserId { get; set; }
        public virtual User FollowedUser { get; set; }
    }
}
