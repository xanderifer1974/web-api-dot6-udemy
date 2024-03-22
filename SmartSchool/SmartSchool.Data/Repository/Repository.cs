using SmartSchool.API.Data;
using SmartSchool.Data.Repository.Interface;

namespace SmartSchool.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
           _context.Update(entity);
        }
        public bool SaveChanges()
        {
           return(_context.SaveChanges() > 0);
        }


    }
}
