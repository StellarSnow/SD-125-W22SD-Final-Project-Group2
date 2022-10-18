using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public interface IUserRepository<T> where T : class
    {
        Task<ApplicationUser> GetAsync(string id);

        ApplicationUser GetByPredicate(Func<ApplicationUser, bool> predicate);

        Task<List<ApplicationUser>> GetAllAsync();

        Task<List<ApplicationUser>> GetListAsync(Func<ApplicationUser, bool> predicate);

        Task<ICollection<string>> GetRolesAsync(ApplicationUser user);

        Task<IList<ApplicationUser>> GetUsersInRoleAsync(string role);

        Task AddUserToRoleAsync(ApplicationUser user, string role);

        Task RemoveUserFromRoleAsync(ApplicationUser user, string role);
    }
}
