using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialConnect.Models
{
    public class SocialConnectDBContext:IdentityDbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFollower> UsersFollowers { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public SocialConnectDBContext()
        {
            
        }
        public SocialConnectDBContext(DbContextOptions<SocialConnectDBContext>options):base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity configurations are applied

            modelBuilder.Entity<UserFollower>()
                .HasKey(uf => new { uf.FollowedUserId, uf.FollowerId }); // Composite primary key
            // Configure UserFollower relationships
            modelBuilder.Entity<UserFollower>(entity =>
            {
                entity.HasOne(uf => uf.Follower)
                      .WithMany(u => u.Following)
                      .HasForeignKey(uf => uf.FollowerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(uf => uf.FollowedUser)
                      .WithMany(u => u.Followers)
                      .HasForeignKey(uf => uf.FollowedUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });



            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(c => c.Posts)
                    .WithMany(p => p.PostComments)
                    .HasForeignKey(c => c.Post_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.UserComment)
                    .WithMany(u => u.UserComments)
                    .HasForeignKey(c => c.User_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Comment-Post relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Posts)
                .WithMany(p => p.PostComments)
                .HasForeignKey(c => c.Post_Id)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete

            // Configure Comment-User relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.UserComment)
                .WithMany(u => u.UserComments)
                .HasForeignKey(c => c.User_Id)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete

            // Configure Post-User relationship
            modelBuilder.Entity<Post>()
                .HasOne(p => p.UserPost)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.user_id)
                .OnDelete(DeleteBehavior.Restrict); // No cascading delete

            //// Define PostComment relationship with no cascade delete
            //modelBuilder.Entity<PostComment>()
            //    .HasOne(pc => pc.Post)
            //    .WithMany(p => p.PostComments)
            //    .HasForeignKey(pc => pc.Post_Id)
            //    .OnDelete(DeleteBehavior.Restrict);  // or .OnDelete(DeleteBehavior.SetNull)

            //modelBuilder.Entity<PostComment>()
            //    .HasOne(pc => pc.Comment)
            //    .WithMany()
            //    .HasForeignKey(pc => pc.comment_Id)
            //    .OnDelete(DeleteBehavior.Restrict);  // or .OnDelete(DeleteBehavior.SetNull)

            //// Define UserComment relationship with no cascade delete
            //modelBuilder.Entity<UserComment>()
            //    .HasOne(uc => uc.Users)
            //    .WithMany(u => u.Comments)
            //    .HasForeignKey(uc => uc.User_Id)
            //    .OnDelete(DeleteBehavior.Restrict);  // or .OnDelete(DeleteBehavior.SetNull)

            //modelBuilder.Entity<UserComment>()
            //    .HasOne(uc => uc.Comment)
            //    .WithMany()
            //    .HasForeignKey(uc => uc.comment_Id)
            //    .OnDelete(DeleteBehavior.Restrict);  // or .OnDelete(DeleteBehavior.SetNull)

        }


    }
}
