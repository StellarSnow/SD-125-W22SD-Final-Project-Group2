using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.Data;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private ApplicationDbContext _db { get; set; }

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(Func<ApplicationUser, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ICollection<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICollection<ApplicationUser> GetList(Func<ApplicationUser, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public ApplicationUser Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(string id)
        {
            return _db.Users.Find(id);
        }

        public ApplicationUser GetEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
