using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.Models.ViewModel;

namespace SD_340_W22SD_Final_Project_Group6.BLL
{
    public class AdministrativeBusinessLogic
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        
        public AdministrativeBusinessLogic(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
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
    }
}
