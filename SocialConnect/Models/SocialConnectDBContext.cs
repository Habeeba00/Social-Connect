using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialConnect.Models
{
    public class SocialConnectDBContext:IdentityDbContext
    {
        public SocialConnectDBContext()
        {
            
        }
        public SocialConnectDBContext(DbContextOptions<SocialConnectDBContext>options):base(options) 
        {
        }
        public virtual DbSet<Users> Users {  get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Followers> Followers { get; set; }



    }
}
