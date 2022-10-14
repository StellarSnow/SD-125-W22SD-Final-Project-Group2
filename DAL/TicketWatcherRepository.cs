using SD_340_W22SD_Final_Project_Group6.Data;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public class TicketWatcherRepository : IRepository<TicketWatcher>
    {
        public ApplicationDbContext DbContext { get; set; }

        public TicketWatcherRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Add(TicketWatcher entity)
        {
            DbContext.TicketWatchers.Add(entity);
        }

        public void Delete(TicketWatcher entity)
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

        public TicketWatcher Get(int id)
        {
            throw new NotImplementedException();
        }

        public TicketWatcher Get(string id)
        {
            throw new NotImplementedException();
        }

        public TicketWatcher Get(Func<TicketWatcher, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ICollection<TicketWatcher> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TicketWatcher> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<TicketWatcher> GetList(Func<TicketWatcher, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public TicketWatcher Update(TicketWatcher entity)
        {
            throw new NotImplementedException();
        }
    }
}
