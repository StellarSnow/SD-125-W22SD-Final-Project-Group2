using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SD_340_W22SD_Final_Project_Group6.BLL;
using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Data;
using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.Models.ViewModel;

namespace SD_340_W22SD_Final_Project_Group6.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AdministrativeBusinessLogic BLL { get; set; }

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> users)
        {
            BLL = new AdministrativeBusinessLogic(new UserRepository(context, users));
        }

        public async Task<IActionResult> Index()
        {
            ProjectManagersAndDevelopersViewModels vm = await BLL.CreateIndexViewModelAsync();
            return View(vm);
        }

        public async Task<IActionResult> ReassignRoleAsync()
        {
            List<ApplicationUser> allUsers = (List<ApplicationUser>)(await BLL.GetAllUsers())[0];
            ViewBag.Users = (List<SelectListItem>)(await BLL.GetAllUsers())[1];

            return View(allUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReassignRole(string role, string userId)
        {
            await BLL.ReassignRole(role, userId);

            return RedirectToAction("Index", "Admin", new { area = "" });
        }
    }
}

