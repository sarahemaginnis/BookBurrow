using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IPostCommentRepository
    {
        public List<PostComment> GetAllOrderedByCommentDate();
        public PostComment GetById(int id);
        public void Add(PostComment postComment);
        public void Update(PostComment postComment);
        public void Delete(int id);
    }
}
