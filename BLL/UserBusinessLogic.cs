using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.BLL
{
    public class UserBusinessLogic
    {
        private IRepository<ApplicationUser> _userRepository { get; set; }

        public UserBusinessLogic(IRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public ApplicationUser GetUser(string id)
        {
            return _userRepository.Get(id);
        }
    }
}
