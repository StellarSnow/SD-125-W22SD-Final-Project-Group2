using Moq;
using SD_340_W22SD_Final_Project_Group6.BLL;
using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace UnitTesting
{
    [TestClass]
    public class ProjectBllUnitTesting
    {
        private ProjectBusinessLogicLayer projectBL;
        private List<Project> allProjects;
        private ApplicationUser user;

        [TestInitialize]
        public void Initialize()
        {
            Project mockProject1 = new Project() { Id = 1, ProjectName = "Project", };
            Project mockProject2 = new Project() { Id = 2, ProjectName = "Project", };
            Project mockProject3 = new Project() { Id = 3, ProjectName = "Project3", };
            UserProject newUser = new UserProject();
            mockProject1.AssignedTo.Add(newUser);
            mockProject3.AssignedTo.Add(newUser);
            newUser.Project = mockProject1;
            newUser.Project = mockProject3;

            allProjects = new List<Project>() { mockProject1, mockProject2, mockProject3, };

            Mock<IRepository<Project>> mockRepo = new Mock<IRepository<Project>>();

            mockRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns<int>((num) => allProjects.First(project => project.Id == num));

            mockRepo.Setup(repo => repo.GetAll()).Returns(allProjects);
            mockRepo.Setup(repo => repo.GetList(It.IsAny<Func<Project, bool>>())).Returns<Func<Project, bool>>((func) => allProjects.Where(func).ToList());

            mockRepo.Setup(repo => repo.Delete(It.IsAny<Project>())).Callback<Project>((proj) => allProjects.Remove(proj));
            mockRepo.Setup(repo => repo.Add(It.IsAny<Project>())).Callback<Project>((proj) => allProjects.Add(proj));

            projectBL = new ProjectBusinessLogicLayer(mockRepo.Object);
        }

        [TestMethod]
        public void GetAllProjectsTest()
        {
            var expectedList = allProjects;
            var actualList = projectBL.GetAll();

            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void GetProjectTest()
        {
            Assert.AreSame(allProjects.First(p => p.Id == 1), projectBL.GetProject(1));
            Assert.AreSame(allProjects.First(p => p.Id == 3), projectBL.GetProject(3));
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
            var projectToDelete = projectBL.GetProject(1);
            projectBL.Delete(projectToDelete);

            CollectionAssert.DoesNotContain(allProjects, projectToDelete);
        }

        [TestMethod]
        public void CreateProjectTest()
        {
            var testAdd = new Project();
            projectBL.Create(testAdd);
            CollectionAssert.Contains(allProjects, testAdd);
        }
    }
}
