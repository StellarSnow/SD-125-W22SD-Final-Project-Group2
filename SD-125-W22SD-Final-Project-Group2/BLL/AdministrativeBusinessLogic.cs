using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.Models.ViewModel;

namespace SD_340_W22SD_Final_Project_Group6.BLL
{
    public class AdministrativeBusinessLogic
    {
        private IRepository<ApplicationUser> _userRepositry;
        public UserManager<ApplicationUser> UserManager { get; set; }
        
        public AdministrativeBusinessLogic(
            UserManager<ApplicationUser> userManager,
            IRepository<ApplicationUser> userRepository
        ) {
            UserManager = userManager;
            _userRepositry = userRepository;
        }
        // did not use repositry because userManager is already abstracting the data Layer. because we dont see user Manager calling db it does internally.
        public async Task<ProjectManagersAndDevelopersViewModels> CreateIndexViewModelAsync()
        {
            ProjectManagersAndDevelopersViewModels viewModel = new ProjectManagersAndDevelopersViewModels();

            List<ApplicationUser> pmUsers = (List<ApplicationUser>)await UserManager.GetUsersInRoleAsync("ProjectManager");
            List<ApplicationUser> devUsers = (List<ApplicationUser>)await UserManager.GetUsersInRoleAsync("Developer");
            List<ApplicationUser> allUsers = await UserManager.Users.ToListAsync();

            viewModel.pms = pmUsers;
            viewModel.devs = devUsers;
            viewModel.allUsers = allUsers;

            return viewModel;
        }

        public async Task<object[]> GetAllUsers()
        {
            List<ApplicationUser> allUsers = _userRepositry.GetAll().ToList();

            List<SelectListItem> users = new List<SelectListItem>();
            
            allUsers.ForEach(u =>
            {
                users.Add(new SelectListItem(u.UserName, u.Id.ToString()));
            });

            return new object[]
            {
                allUsers, users
            };
        }

        public async Task ReassignRole(string role, string userId)
        {
            ApplicationUser user = _userRepositry.Get(userId);

            ICollection<string> roleUser = await UserManager.GetRolesAsync(user);
            
            if (roleUser.Count == 0)
            {
                await UserManager.AddToRoleAsync(user, role);
            }
            else
            {
                await UserManager.RemoveFromRoleAsync(user, roleUser.First());
                await UserManager.AddToRoleAsync(user, role);
            }
        }
    }
}
