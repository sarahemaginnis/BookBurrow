using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface ISeriesRepository
    {
        public List<Series> GetAllOrderedByName();
        public Series GetById(int id);
        public void Add(Series series);
        public void Update(Series series);
        public void Delete(int id);
    }
}
