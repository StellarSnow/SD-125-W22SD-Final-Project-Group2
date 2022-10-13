using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.BLL
{
    public class ProjectBusinessLogicLayer
    {
        private IRepository<Project> _projectRepository;

        public ProjectBusinessLogicLayer(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Create(Project? project)
        {
            if (project is null)
            {
                throw new ArgumentNullException();
            }

            _projectRepository.Add(project);
            _projectRepository.Save();
        }

        public void Edit(Project? project)
        {
            if (project is null)
            {
                throw new ArgumentNullException();
            }

            _projectRepository.Update(project);
            _projectRepository.Save();
        }

        public List<Project> GetAll()
        {
            return _projectRepository.GetAll().ToList();
        }

        public List<Project> GetOrderedByPriority()
        {
            return _projectRepository.GetAll().OrderByDescending(p => p.Tickets.OrderByDescending(t => t.TicketPriority))
                .ToList();
        }

        public List<Project> GetOrderedByPriorityAsc()
        {
            return _projectRepository.GetAll().OrderBy(p => p.Tickets.OrderBy(t => t.TicketPriority))
                .ToList();
        }

        public List<Project> GetOrderedByRequiredHours()
        {
            return _projectRepository.GetAll().OrderByDescending(p => p.Tickets.OrderByDescending(t => t.RequiredHours))
                .ToList();
        }

        public List<Project> GetOrderedByRequiredHoursAsc()
        {
            return _projectRepository.GetAll().OrderBy(p => p.Tickets.OrderBy(t => t.RequiredHours))
                .ToList();
        }

        public List<Project> GetCompletedProjects()
        {
            return _projectRepository.GetAll()
                .ToList();
        }

        public Project? Get(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException();
            }

            return _projectRepository.Get(p => p.Id == id);
        }

        public void Delete(Project entity)
        {
            _projectRepository.Delete(entity);
            _projectRepository.Save();
        }
    }
}