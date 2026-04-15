using lab1.Data;
using lab1.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace lab1.Repositories
{
    public class Repository<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList(); ;
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
