using SocialConnect.Models;
using SocialConnect.Repository;
using SocialConnect.UnitOfWorks;

namespace SocialConnect.UnitOfWorks
{
    public class UnitOfWork
    {
        GenericRepository<Post> postsRepo;
        GenericRepository<Like> likesRepo;
        GenericRepository<Comment> commentsRepo;
        
        SocialConnectDBContext db;
        public UnitOfWork(SocialConnectDBContext db)
        {
            this.db = db;
        }

        public GenericRepository<Post> postsGenericRepository
        { 
            get {
                if(postsRepo == null)
                    postsRepo = new GenericRepository<Post>(db);

                 return postsRepo;
            }
        }
        public GenericRepository<Like> likesGenericRepository
        {
            get
            {
                if (likesRepo == null)
                    likesRepo = new GenericRepository<Like>(db);

                return likesRepo;
            }

        }
        public GenericRepository<Comment> commentsGenericRepository
        {
            get
            {
                if (commentsRepo == null)
                    commentsRepo = new GenericRepository<Comment>(db);

                return commentsRepo;
            }
        }
        public void save()
        {
            db.SaveChanges();
        }
    }
}
