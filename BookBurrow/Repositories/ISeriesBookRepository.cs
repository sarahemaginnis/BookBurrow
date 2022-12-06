using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface ISeriesBookRepository
    {
        public List<SeriesBook> GetAllOrderedBySeriesPosition();
        public SeriesBook GetById(int id);
        public void Add(SeriesBook seriesBook);
        public void Update(SeriesBook seriesBook);
        public void Delete(int id);
    }
}
