using SD_340_W22SD_Final_Project_Group6.BLL;
using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;
using SD_340_W22SD_Final_Project_Group6.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace TestProject
{
    [TestClass]
    public class DaneTests
    {
        private TicketBusinessLogic ticketBLL;
        private UserManager<ApplicationUser> userManager;
        private ProjectBusinessLogicLayer projectBLL;

        public DaneTests()
        {
            CreateMockTickets();
            CreateMockProjects();
            //var userData = new List<ApplicationUser>
            //{
            //    new ApplicationUser {Id = "one", UserName = "Superman"},
            //    new ApplicationUser {Id = "two", UserName = "Batman"},
            //    new ApplicationUser {Id = "three", UserName = "Green Lantern"},
            //    new ApplicationUser {Id = "one", UserName = "The Flash"}
            //}.AsQueryable();

            //var mockUserDbSet = new Mock<DbSet<ApplicationUser>>();

            //mockUserDbSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(userData.Provider);
            //mockUserDbSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(userData.Expression);
            //mockUserDbSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            //mockUserDbSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator);

            //var mockContext = new Mock<ApplicationDbContext>();
            //mockContext.Setup(u => u.Users).Returns(mockUserDbSet.Object);






            
        }

        public void CreateMockTickets()
        {
            var data = new List<Ticket>
            {
                new Ticket {Id = 1, Title = "Add Olives", Body = "Add Olives to the pizza", RequiredHours = 5, Completed = false },
                new Ticket {Id = 2, Title = "Add Pineapples", Body = "Add Pineapples to the pizza", RequiredHours = 4, Completed = true },
                new Ticket {Id = 3, Title = "Add Red Peppers", Body = "Add Red Peppers to the pizza", RequiredHours = 3, Completed = false },
                new Ticket {Id = 4, Title = "Add Ham", Body = "Add Ham to the pizza", RequiredHours = 2, Completed = false }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Ticket>>();

            mockDbSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(t => t.Tickets).Returns(mockDbSet.Object);

            ticketBLL = new TicketBusinessLogic(new TicketRepository(mockContext.Object));
        }

        public void CreateMockProjects()
        {
            var data = new List<Project>
            {
                new Project {Id = 1, ProjectName = "Make Pizza"},
                new Project {Id = 2, ProjectName = "The Batman Project"}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Project>>();

            mockDbSet.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Projects).Returns(mockDbSet.Object);

            projectBLL = new ProjectBusinessLogicLayer(new ProjectRepository(mockContext.Object));
        }

        [TestMethod]
        [DataRow(1)]
        public void GetTicket_ValidInputs_ReturnsATicket(int id)
        {
            Ticket ticket = ticketBLL.GetTicket(id);

            Assert.AreEqual(id, ticket.Id);
        }

        [TestMethod]
        [DataRow(77)]
        public void GetTicket_InvalidInputs_DoesNotReturnATicket(int id)
        {
            Ticket ticket;

            try
            { 
               ticket = ticketBLL.GetTicket(id);
            }
            catch (Exception ex)
            {
                ticket = null;
            }
            
            Assert.IsNull(ticket);
        }

        [TestMethod]
        [DataRow(2)]
        public void UpdateTicket_ValidInputs_UpdatesATicket(int id)
        {
            Ticket ticket = ticketBLL.GetTicket(id);
            string originalBody = ticket.Body;  
            ticket.Body = "Some people don't like pineapples";

            ticketBLL.UpdateTicket(ticket);

            Ticket ticket2 = ticketBLL.GetTicket(id);

            Assert.AreNotEqual(originalBody, ticket2.Body);
        }

        [TestMethod]
        [DataRow(2)]
        public void UpdateTicket_TitleTooShort_DoesNotUpdateATicket(int id)
        {
            Ticket ticket = ticketBLL.GetTicket(id);
            ticket.Title = "One";

            bool errorThrown = false;

            try
            {
                ticketBLL.UpdateTicket(ticket);
            }
            catch (Exception ex)
            {
                errorThrown = true;
            }

            Assert.AreEqual(true, errorThrown);
        }
    }
}
