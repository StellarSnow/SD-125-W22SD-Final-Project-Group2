using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.BLL;

public class ProjectBusinessLogicLayer
{
    private readonly IRepository<Project> _projectRepository;

    public ProjectBusinessLogicLayer(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public List<Project> GetAll()
    {
        return _projectRepository.GetAll().ToList();
    }
}
