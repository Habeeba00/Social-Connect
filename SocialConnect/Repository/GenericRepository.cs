using SocialConnect.Models;

namespace SocialConnect.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        public SocialConnectDBContext Db { get; }

        public GenericRepository(SocialConnectDBContext db)
        {
            Db = db;
        }

        public List<TEntity> selectall()
        {
            return Db.Set<TEntity>().ToList();
        }

        public TEntity selectbyid(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void add(TEntity entity)
        {
            Db.Set<TEntity>().Add(entity);
        }

        public void edit(TEntity entity)
        {
            Db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void delete(int id)
        {
            TEntity entity = selectbyid(id);
            Db.Set<TEntity>().Remove(entity);
        }

    }
}
