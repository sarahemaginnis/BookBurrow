using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IUserRoleRepository
    {
        public List<UserRole> GetAllOrderedByCreatedAt();
        public UserRole GetById(int id);
        public void Add(UserRole userRole);
        public void Update(UserRole userRole);
        public void Delete(int id);
    }
}
