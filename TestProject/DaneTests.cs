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
    public class DaneTestsTicketBusinessLogic
    {
        private TicketBusinessLogic ticketBLL;
        private ProjectBusinessLogicLayer projectBLL;

        public DaneTestsTicketBusinessLogic()
        {
            CreateMockTickets();
            CreateMockProjects();
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
            ticket.Title = "O";

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

        [TestMethod]
        [DataRow(1)]
        public void Add_ValidTicket_AddATicket(int projectId)
        {
            // The idea for this method is taken from
            // https://learn.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN
            // I needed to set up a context and test it directly to see if
            // it had Verified the Add
            var mockDbSet = new Mock<DbSet<Ticket>>();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Tickets).Returns(mockDbSet.Object);

            Project project = projectBLL.Get(projectId);
            Ticket ticket = new Ticket();

            ticket.Id = 5;
            ticket.Title = "Add Shrimp";
            ticket.Body = "Add Shrimp to pizza";
            ticket.RequiredHours = 2;
            ticket.TicketPriority = Ticket.Priority.High;
            ticket.Completed = false;
            ticket.Project = project;

            TicketBusinessLogic myTicketBLL = new TicketBusinessLogic(new TicketRepository(mockContext.Object));

            myTicketBLL.Add(ticket);

            mockDbSet.Verify(m => m.Add(It.IsAny<Ticket>()), Times.Once());
        }

        [TestMethod]
        [DataRow(1)]
        public void SaveTicket_ValidTicket_SaveATicket(int projectId)
        {
            // The idea for this method is taken from
            // https://learn.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN
            // I needed to set up a context and test it directly 
            var mockDbSet = new Mock<DbSet<Ticket>>();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Tickets).Returns(mockDbSet.Object);

            Project project = projectBLL.Get(projectId);
            Ticket ticket = new Ticket();

            ticket.Id = 1;
            ticket.Title = "Add Shrimp";
            ticket.Body = "Add Shrimp to pizza";
            ticket.RequiredHours = 2;
            ticket.TicketPriority = Ticket.Priority.High;
            ticket.Completed = false;
            ticket.Project = project;

            TicketBusinessLogic myTicketBLL = new TicketBusinessLogic(new TicketRepository(mockContext.Object));

            myTicketBLL.Add(ticket);
            myTicketBLL.SaveTicket();

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void GetAllTickets_ValidInputs_ReturnAllTickets()
        {
            List<Ticket> tickets = ticketBLL.GetAllTickets();

            Assert.AreEqual(4, tickets.Count);
        }

        [TestMethod]
        public void DeleteTicket_ValidInputs_DeleteATicket()
        {
            // The idea for this method is taken from
            // https://learn.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking?redirectedfrom=MSDN
            // I needed to set up a context and test it directly 
            var mockDbSet = new Mock<DbSet<Ticket>>();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Tickets).Returns(mockDbSet.Object);

            Project project = projectBLL.Get(1);
            Ticket ticket = new Ticket();

            ticket.Id = 1;
            ticket.Title = "Add Shrimp";
            ticket.Body = "Add Shrimp to pizza";
            ticket.RequiredHours = 2;
            ticket.TicketPriority = Ticket.Priority.High;
            ticket.Completed = false;
            ticket.Project = project;

            TicketBusinessLogic myTicketBLL = new TicketBusinessLogic(new TicketRepository(mockContext.Object));

            myTicketBLL.DeleteTicket(ticket);

            mockDbSet.Verify(m => m.Remove(It.IsAny<Ticket>()), Times.Once());
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void DoesTicketExist_ValidTicket_ReturnsTrue(int id)
        {
            bool ticketExists = ticketBLL.DoesTicketExist(id);

            Assert.IsTrue(ticketExists);
        }

        [TestMethod]
        [DataRow(7)]
        [DataRow(8)]
        public void DoesTicketExist_InvalidTicket_ReturnsFalse(int id)
        {
            bool ticketExists = ticketBLL.DoesTicketExist(id);

            Assert.IsFalse(ticketExists);
        }
    }

    [TestClass]
    public class DaneTestsTicketWatcherBusinessLogic
    {
        private TicketBusinessLogic ticketBLL;
        private ProjectBusinessLogicLayer projectBLL;

        public DaneTestsTicketWatcherBusinessLogic()
        {
            CreateMockTickets();
            CreateMockProjects();
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
    }
}
