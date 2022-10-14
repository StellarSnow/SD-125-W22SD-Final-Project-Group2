using Microsoft.EntityFrameworkCore;
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
            _db.Add(entity);
        }

        public Ticket Get(int id)
        {
            return _db.Tickets.Include(p => p.Project).First(t => t.Id == id);
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _db.Tickets
                .Include(t => t.Project)
                .Include(t => t.TicketWatchers)
                .ThenInclude(tw => tw.Watcher)
                .Include(u => u.Owner)
                .Include(t => t.Comments)
                .ThenInclude(c => c.CreatedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
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

        public Ticket Get(Func<Ticket, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ICollection<Ticket> GetAll()
        {
            return _db.Tickets.Include(t => t.Project).Include(t => t.Owner).ToList();
        }

        public ICollection<Ticket> GetList(Func<Ticket, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public ApplicationDbContext Get_db()
        {
            return _db;
        }

        public Ticket Update(Ticket entity)
        {
            _db.Update(entity);
            return entity;
        }

        public Ticket Get(string id)
        {
            throw new NotImplementedException();
        }

        public Ticket GetEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
