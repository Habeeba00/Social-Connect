using Microsoft.EntityFrameworkCore;
using SocialConnect.Models;
using SocialConnect.Repository;

namespace SocialConnect.UnitOfWorks
{
    public class UnitOfWork
    {
        GenericRepository<User> UserGenericRepository;
        GenericRepository<Likes> LikeGenericRepository;
        GenericRepository<Post> PostGenericRepository;
        GenericRepository<Comment> CommentGenericRepository;
        GenericRepository<UserFollower> UserFollowerGenericRepository;


        SocialConnectDBContext db;
        public UnitOfWork(SocialConnectDBContext db) 
        {
            this.db = db;
        }
        //public IQueryable<User> GetActiveUsers()
        //{
        //    return db.Users.Where(u => !EF.Property<bool>(u, "IsDeleted"));
        //}


        public GenericRepository<User> UserRepo 
        {
            get 
            {
                if (UserGenericRepository == null) 
                    UserGenericRepository=new GenericRepository<User> (db);
                return UserGenericRepository;
                
            } 
        }

        public GenericRepository<Post> PostRepo 
        {
            get 
            {
                if (PostGenericRepository ==null)
                    PostGenericRepository=new GenericRepository<Post> (db);
                return PostGenericRepository;
            } 
        }

        public GenericRepository <Comment> CommentRepo 
        {
            get 
            {
                if (CommentGenericRepository ==null)
                    CommentGenericRepository =new GenericRepository<Comment> (db);
                return CommentGenericRepository;
            }
        }

        public GenericRepository<Likes> LikeRepo 
        {
            get 
            {
                if (LikeGenericRepository ==null)
                    LikeGenericRepository =new GenericRepository<Likes> (db);
                return LikeGenericRepository;
            }
        }


        public GenericRepository<UserFollower> UserFollowerRepo 
        {
            get 
            {
                if (UserFollowerGenericRepository ==null)
                    UserFollowerGenericRepository=new GenericRepository<UserFollower> (db);
                return UserFollowerGenericRepository;
            } 
        }
        public void Save()
        {
            db.SaveChanges();
        }


        public void SoftDeleteUser(int userId)
        {
            var user = UserGenericRepository.GetById(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Same logic for soft deletion
            user.IsDeleted = true;

            foreach (var post in user.Posts)
            {
                post.IsDeleted = true;
                foreach (var comment in post.Comments)
                {
                    comment.IsDeleted = true;
                }
                foreach (var like in post.Likes)
                {
                    like.IsDeleted = true;
                }
            }

            foreach (var comment in user.UserComments)
            {
                comment.IsDeleted = true;
            }

            foreach (var like in user.Likes)
            {
                like.IsDeleted = true;
            }

            foreach (var follower in user.Followers)
            {
                follower.IsDeleted = true;
            }
            foreach (var following in user.Following)
            {
                following.IsDeleted = true;
            }

            UserGenericRepository.Update(user);
            db.SaveChanges();
        }



    }
}
