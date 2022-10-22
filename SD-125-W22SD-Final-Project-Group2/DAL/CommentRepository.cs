using SD_340_W22SD_Final_Project_Group6.Data;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.DAL
{
    public class CommentRepository : IRepository<Comment>
    {
        public ApplicationDbContext DbContext { get; set; }

        public CommentRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Add(Comment entity)
        {
            DbContext.Comments.Add(entity);
        }

        public void Delete(Comment entity)
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

        public Comment Get(int id)
        {
            throw new NotImplementedException();
        }

        public Comment Get(string id)
        {
            throw new NotImplementedException();
        }

        public Comment Get(Func<Comment, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public ICollection<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Comment> GetList(Func<Comment, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public Comment Update(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Comment GetEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
