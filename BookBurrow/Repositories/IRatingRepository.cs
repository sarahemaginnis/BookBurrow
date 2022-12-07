using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IRatingRepository
    {
        public List<Rating> GetAll();
        public Rating GetById(int id);
        public void Add(Rating rating);
        public void Update(Rating rating);
        public void Delete(int id);
    }
}
