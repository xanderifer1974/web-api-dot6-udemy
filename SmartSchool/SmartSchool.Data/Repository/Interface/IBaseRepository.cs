namespace SmartSchool.Data.Repository.Interface
{
    public interface IBaseRepository
    {
        public void Add<T>(T entity) where T : class;
        public void Update<T>(T entity) where T : class;
        public void Delete<T>(T entity) where T : class;
        public bool SaveChanges();


    }
}
