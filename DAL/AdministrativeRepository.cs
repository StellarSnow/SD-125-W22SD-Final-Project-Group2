using Microsoft.AspNetCore.Identity;
using SD_340_W22SD_Final_Project_Group6.Data;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public class AdministrativeRepository : IRepository<ApplicationUser>
    {
        public ApplicationDbContext DbContext { get; set; }
        public AdministrativeRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
        public ApplicationUser Get(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ApplicationUser> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(Func<ApplicationUser, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ICollection<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICollection<ApplicationUser> GetList(Func<ApplicationUser, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Udate(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
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

    }
}
