namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    // Copied mostly from Zacherie Montreuil's code for GenericRepositoryMVCApp
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        T Get(int id);
        T Get(string id);

        Task<T> GetAsync(int id);

        T Get(Func<T, bool> predicate);

        ICollection<T> GetAll();

        ICollection<T> GetList(Func<T, bool> predicate);

        T Update(T entity);

        void Delete(T entity);

        void Save();

        bool Exists(int id);

        bool Exists();
    }
}
