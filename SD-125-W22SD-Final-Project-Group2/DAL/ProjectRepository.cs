using Microsoft.EntityFrameworkCore;
using SD_340_W22SD_Final_Project_Group6.Data;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public class ProjectRepository : IRepository<Project>
    {

        private ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Project entity)
        {
            _context.Add(entity);
        }

        public Project Get(int id)
        {
            var project = _context
                .Projects
                .Include(p => p.Tickets)
                .ThenInclude(t => t.Comments)
                .Include(p => p.AssignedTo)
                .ThenInclude(at => at.ApplicationUser)
                .Include(p => p.CreatedBy)
                .Single(p => p.Id == id);
            return project;
        }

        public Project Get(Func<Project, bool> predicate)
        {
            var project = _context
                .Projects
                .Include(p => p.Tickets)
                .ThenInclude(t => t.Comments)
                .Include(p => p.AssignedTo)
                .ThenInclude(at => at.ApplicationUser)
                .Include(p => p.CreatedBy).Single(predicate);
            return project;
        }

        public ICollection<Project> GetAll()
        {
            var projects = _context
                .Projects
                .Include(p => p.Tickets)
                .ThenInclude(t => t.Comments)
                .Include(p => p.AssignedTo)
                .ThenInclude(at => at.ApplicationUser)
                .Include(p => p.CreatedBy)
                .ToList();
            return projects;
        }

        public ICollection<Project> GetList(Func<Project, bool> predicate)
        {
            var projects = _context
                .Projects
                .Include(p => p.Tickets)
                .ThenInclude(t => t.Comments)
                .Include(p => p.AssignedTo)
                .ThenInclude(at => at.ApplicationUser)
                .Include(p => p.CreatedBy)
                .Where(predicate)
                .ToList();
            return projects;
        }

        public Project Update(Project entity)
        {
            _context.Update(entity);
            var project = _context
                .Projects
                .Include(p => p.Tickets)
                .ThenInclude(t => t.Comments)
                .Include(p => p.AssignedTo)
                .ThenInclude(at => at.ApplicationUser)
                .Include(p => p.CreatedBy)
                .Single(p => p.Id == entity.Id);
            return project;
        }

        public void Delete(Project entity)
        {
            _context.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Projects.Any(p => p.Id == id);
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Project Get(string id)
        {
            throw new NotImplementedException();
        }

        public Project GetEntity(int id)
        {
            return _context.Projects.Find(id);
        }
    }
}
