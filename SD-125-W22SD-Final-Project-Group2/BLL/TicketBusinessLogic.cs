using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.BLL
{
    public class TicketBusinessLogic
    {
        private IRepository<Ticket> _repo { get; set; }

        public TicketBusinessLogic(IRepository<Ticket> repo)
        {
            _repo = repo;
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _repo.GetAsync(id);
        }

        public Ticket GetTicket(int id)
        {
            return _repo.Get(id);
        }

        public Ticket UpdateTicket(Ticket ticket)
        {
            return _repo.Update(ticket);
        }

        public void SaveTicket()
        {
            _repo.Save();
        }

        public void Add(Ticket entity)
        {
            _repo.Add(entity);
        }

        public List<Ticket> GetAllTickets()
        {
            return _repo.GetAll().ToList();
        }

        public void DeleteTicket(Ticket ticket)
        {
            _repo.Delete(ticket);
        }

        public bool DoesTicketExist(int id)
        {
            return _repo.Exists(id);
        }
    }
}
