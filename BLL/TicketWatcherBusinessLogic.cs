using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.DAL;

namespace SD_340_W22SD_Final_Project_Group6.BLL
{
    public class TicketWatcherBusinessLogic
    {
        private IRepository<TicketWatcher> _repo { get; set; }

        public TicketWatcherBusinessLogic(IRepository<TicketWatcher> repo)
        {
            _repo = repo;
        }

        public void AddTicketWatcher(TicketWatcher entity)
        {
            _repo.Add(entity);
        }

        public void SaveTicketWatcher()
        {
            _repo.Save();
        }
    }
}
