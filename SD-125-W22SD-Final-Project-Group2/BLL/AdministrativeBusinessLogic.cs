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
        private IUserRepository<ApplicationUser> _userRepositry;
        
        public AdministrativeBusinessLogic(
            IUserRepository<ApplicationUser> userRepository
        ) {
            _userRepositry = userRepository;
        }

        public async Task<ProjectManagersAndDevelopersViewModels> CreateIndexViewModelAsync()
        {
            ProjectManagersAndDevelopersViewModels viewModel = new ProjectManagersAndDevelopersViewModels();

            List<ApplicationUser> pmUsers = (List<ApplicationUser>)await _userRepositry.GetUsersInRoleAsync("ProjectManager");

            List<ApplicationUser> devUsers = (List<ApplicationUser>)await _userRepositry.GetUsersInRoleAsync("Developer");

            List<ApplicationUser> allUsers = await _userRepositry.GetAllAsync();

            viewModel.pms = pmUsers;
            viewModel.devs = devUsers;
            viewModel.allUsers = allUsers;

            return viewModel;
        }

        public async Task<object[]> GetAllUsers()
        {
            List<ApplicationUser> allUsers = (await _userRepositry.GetAllAsync()).ToList();

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
            ApplicationUser user = await _userRepositry.GetAsync(userId);

            ICollection<string> roleUser = await _userRepositry.GetRolesAsync(user);
            
            if (roleUser.Count == 0)
            {
                await _userRepositry.AddUserToRoleAsync(user, role);
            }
            else
            {
                await _userRepositry.RemoveUserFromRoleAsync(user, roleUser.First());
                await _userRepositry.AddUserToRoleAsync(user, role);
            }
        }
    }
}
