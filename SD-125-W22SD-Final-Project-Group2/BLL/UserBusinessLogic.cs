using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.BLL
{
    public class UserBusinessLogic
    {
        private IUserRepository<ApplicationUser> _userRepository { get; set; }

        public UserBusinessLogic(IUserRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            var abc = await _userRepository.GetAsync(id);

            return abc;
        }

        public async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            return _userRepository.GetByPredicate(u => u.UserName == userName);
        }

        public async Task<List<ApplicationUser>> GetUsersWhoAreNotTheTicketOwnerAsync(ApplicationUser owner)
        {
            List<ApplicationUser> usersWhoAreNotOwners = await _userRepository.GetListAsync(u => u != owner);

            return usersWhoAreNotOwners;
        }
    }
}
