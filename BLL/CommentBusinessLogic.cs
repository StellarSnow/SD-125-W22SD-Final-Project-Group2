using SD_340_W22SD_Final_Project_Group6.DAL;
using SD_340_W22SD_Final_Project_Group6.Models;

namespace SD_340_W22SD_Final_Project_Group6.BLL
{
    public class CommentBusinessLogic
    {
        public IRepository<Comment> _repo { get; set; }

        public CommentBusinessLogic(IRepository<Comment> repo)
        {
            _repo = repo;
        }

        public void SaveComment()
        {
            _repo.Save();
        }

        public void AddComment(Comment entity)
        {
            _repo.Add(entity);
        }
    }
}
