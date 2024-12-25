using Microsoft.EntityFrameworkCore;
using SocialConnect.Models;

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

        public TEntity GetById(int id) 
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

        public TEntity Delete(int id) 
        {
            TEntity entity=GetById(id);
            DB.Set<TEntity>().Remove(entity);
            DB.SaveChanges();
            return entity;
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



    }
}
