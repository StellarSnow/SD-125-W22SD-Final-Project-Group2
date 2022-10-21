using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SD_340_W22SD_Final_Project_Group6.BLL;
using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Data;
using SD_340_W22SD_Final_Project_Group6.Models;
using TestProject.Mock.DAL;

namespace TestProject
{
    [TestClass]
    public class UserTests
    {
        public static List<string> _userIds { get; set; }
        public static List<string> _userNames { get; set; }
        public static UserManager<ApplicationUser> _userManager { get; set; }
        public static UserBusinessLogic _userBLL { get; set; }
        public static ApplicationDbContext _dbContext { get; set; }

        [ClassInitialize]
        public static void InitializeUserTests(TestContext testContext)
        {
            _userIds = new List<string>()
            {
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            };

            _userNames = new List<string>()
            {
                "johnsmith@johnsmith.com",
                "janedoe@janedoe.com",
                "alicebob@alicebob.com"
            };

            ApplicationUser user1 = new ApplicationUser()
            {
                Id = _userIds[0],
                Email = _userNames[0],
                UserName = _userNames[0]
            };

            ApplicationUser user2 = new ApplicationUser()
            {
                Id = _userIds[1],
                Email = _userNames[1],
                UserName = _userNames[1]
            };

            ApplicationUser user3 = new ApplicationUser()
            {
                Id = _userIds[2],
                Email = _userNames[2],
                UserName = _userNames[2]
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(databaseName: "FakeDatabase")
                                .Options;

            _dbContext = new ApplicationDbContext(options);

            _dbContext.Users.Add(user1);
            _dbContext.Users.Add(user2);
            _dbContext.Users.Add(user3);

            _dbContext.SaveChanges();

            var userStore = new UserStore<ApplicationUser>(_dbContext);

            _userManager = new FakeUserManager(userStore);

            _userBLL = new UserBusinessLogic(new UserRepository(_dbContext, _userManager));
        }

        [TestMethod]
        public async Task TestGetUserAsyncSuccess()
        {
            ApplicationUser user1 = await _userBLL.GetUserAsync(_userIds[0]);

            Assert.IsNotNull(user1);

            Assert.AreEqual(user1.Id, _userIds[0]);

            ApplicationUser user2 = await _userBLL.GetUserAsync(_userIds[1]);

            Assert.IsNotNull(user2);

            Assert.AreEqual(user2.Id, _userIds[1]);
            
            ApplicationUser user3 = await _userBLL.GetUserAsync(_userIds[2]);

            Assert.IsNotNull(user3);

            Assert.AreEqual(user3.Id, _userIds[2]);
        }

        [DataRow("abcde")]
        [TestMethod]
        public async Task TestGetUserAsyncFailure(string id)
        {
            ApplicationUser user = await _userBLL.GetUserAsync(id);

            Assert.IsNull(user);
        }

        [DataRow("abcde")]
        [TestMethod]
        public async Task TestGetUserByUserNameFailure(string userName)
        {
            ApplicationUser user = await _userBLL.GetUserByUserName(userName);

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task TestGetUserByUserNameSuccess()
        {
            ApplicationUser user1 = await _userBLL.GetUserByUserName(_userNames[0]);

            Assert.IsNotNull(user1);

            Assert.AreEqual(user1.UserName, _userNames[0]);

            ApplicationUser user2 = await _userBLL.GetUserByUserName(_userNames[1]);

            Assert.IsNotNull(user2);

            Assert.AreEqual(user2.UserName, _userNames[1]);

            ApplicationUser user3 = await _userBLL.GetUserByUserName(_userNames[2]);

            Assert.IsNotNull(user3);

            Assert.AreEqual(user3.UserName, _userNames[2]);
        }

        [TestMethod]
        public async Task TestGetUsersWhoAreNotTheTicketOwnerAsyncSuccess()
        {
            ApplicationUser owner = await _userBLL.GetUserAsync(_userIds[0]);

            List<ApplicationUser> users = await _userBLL.GetUsersWhoAreNotTheTicketOwnerAsync(owner);

            bool isOwnerReturned = users.Exists(u => u.Id == owner.Id);

            Assert.IsFalse(isOwnerReturned);
        }
    }
}
