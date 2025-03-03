using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SocialConnect.Models
{
    public class SocialConnectDBContext : IdentityDbContext<User>
    {
        public SocialConnectDBContext(DbContextOptions<SocialConnectDBContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFollower> UsersFollowers { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Post>().HasQueryFilter(p => !p.IsDeleted && !p.UserPost.IsDeleted);
            modelBuilder.Entity<Comment>().HasQueryFilter(c => !c.IsDeleted && !c.UserComment.IsDeleted && !c.Posts.IsDeleted);
            modelBuilder.Entity<Likes>().HasQueryFilter(l => !l.UserLikes.IsDeleted && !l.Post.IsDeleted);
            modelBuilder.Entity<UserFollower>().HasQueryFilter(uf => !uf.Follower.IsDeleted && !uf.FollowedUser.IsDeleted);


            modelBuilder.Entity<UserFollower>()
                .HasKey(uf => new { uf.FollowedUserId, uf.FollowerId });

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
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.Post_Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.UserComment)
                      .WithMany(u => u.UserComments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(p => p.UserPost)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Likes>(entity =>
            {
                entity.HasOne(l => l.UserLikes)
                      .WithMany(u => u.Likes)
                      .HasForeignKey(l => l.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(l => l.Post)
                      .WithMany(p => p.Likes)
                      .HasForeignKey(l => l.Post_Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        
        
        
        }

    }
}
