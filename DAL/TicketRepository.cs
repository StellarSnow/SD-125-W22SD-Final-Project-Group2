using SD_340_W22SD_Final_Project_Group6.Data;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public class TicketRepository : IRepository<Ticket>
    {
        private ApplicationDbContext _db { get; set; }

        public TicketRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public Ticket Get(int id)
        {
            throw new NotImplementedException();
        }

        public Ticket Get(Func<Ticket, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ICollection<Ticket> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICollection<Ticket> GetList(Func<Ticket, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Ticket Udate(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
