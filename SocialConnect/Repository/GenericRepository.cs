using Microsoft.EntityFrameworkCore;
using SocialConnect.Models;
using SocialConnect.Root;

namespace SocialConnect.Repository
{
    public class GenericRepository<TEntity> where TEntity:class 
    {
        public SocialConnectDBContext DB { get; }
        public GenericRepository(SocialConnectDBContext db)
        {
            DB = db;
            
        }

        public List<TEntity> GetAll() 
        {
            return DB.Set<TEntity>().ToList();
        }

        public TEntity GetById(string id) 
        {
            return DB.Set<TEntity>().Find(id);
        }

        public TEntity GetByName(string username) 
        {
            return DB.Set<TEntity>().Find(username);
        }

        public TEntity Add(TEntity entity) 
        {
            DB.Set<TEntity>().Add(entity);
            DB.SaveChanges();
            return entity;
        }
        public TEntity Update(TEntity entity) 
        {
            DB.Entry(entity).State = EntityState.Modified;
            DB.SaveChanges();
            return entity;
        }

        public TEntity Delete(string id) 
        {
            TEntity entity=GetById(id);
            DB.Set<TEntity>().Remove(entity);
            DB.SaveChanges();
            return entity;
        }

        public void Unfollow(string followerId, string followedUserId)
        {
            var entity = DB.Set<UserFollower>().FirstOrDefault(uf => uf.FollowerId == followerId && uf.FollowedUserId == followedUserId);

            if (entity == null)
            {
                throw new ArgumentException("Follow relationship not found.");
            }

            DB.Set<UserFollower>().Remove(entity);
            DB.SaveChanges();
        }

        public void SoftDelete(TEntity entity)
        {
            var isDeletedProperty = entity.GetType().GetProperty("IsDeleted");
            if (isDeletedProperty != null)
            {
                isDeletedProperty.SetValue(entity, true);
                Update(entity);
            }
        }
        // Get all active (non-deleted) entities
        public IQueryable<TEntity> GetAllActive()
        {
            var query = DB.Set<TEntity>().AsQueryable();

            var isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");
            if (isDeletedProperty != null)
            {
                query = query.Where(e => (bool)isDeletedProperty.GetValue(e) == false);
            }

            return query;
        }
        // Bulk soft delete
        public void SoftDeleteBulk(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                SoftDelete(entity);
            }
        }

        // Save changes with soft delete logic
        public int SaveChanges()
        {
            foreach (var entry in DB.ChangeTracker.Entries())
            {
                if (entry.Entity is IDeletableEntity deletableEntity && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    deletableEntity.IsDeleted = true;
                }
            }

            return DB.SaveChanges();
        }





    }
}
