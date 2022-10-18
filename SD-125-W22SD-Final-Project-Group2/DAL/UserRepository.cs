using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public class UserRepository : IUserRepository<ApplicationUser>
    {
        private ApplicationDbContext _db { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }

        public UserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public ApplicationUser GetByPredicate(Func<ApplicationUser, bool> predicate)
        {
            return _db.Users.First(predicate);
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetListAsync(Func<ApplicationUser, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public async Task<ICollection<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task AddUserToRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task RemoveUserFromRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string role)
        {
            IList<ApplicationUser> users = await _userManager.GetUsersInRoleAsync(role);

            return users;
        }
    }
}
