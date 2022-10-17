using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.Data;
using Microsoft.AspNetCore.Identity;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        private ApplicationDbContext _db { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }

        public UserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(string id)
        {
            return _userManager.Users.First(u => u.Id == id);
        }

        public Task<ApplicationUser> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(Func<ApplicationUser, bool> predicate)
        {
            return _db.Users.First(predicate);
        }

        public ICollection<ApplicationUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public ICollection<ApplicationUser> GetList(Func<ApplicationUser, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public async Task<ICollection<string>> GetRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
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

        public ApplicationUser GetEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
