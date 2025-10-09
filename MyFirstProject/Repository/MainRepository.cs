using Microsoft.EntityFrameworkCore;
using MyFirstProject.Data;
using MyFirstProject.Interfaces;


namespace MyFirstProject.Repository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        private readonly HrDbContext _context;
        public MainRepository(HrDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }

        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();

        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);

        }

        public T GetByUid(string Uid)
        {
            return _context.Set<T>().FirstOrDefault(e => EF.Property<string>(e, "Uid") == Uid);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();

        }
    }
}
