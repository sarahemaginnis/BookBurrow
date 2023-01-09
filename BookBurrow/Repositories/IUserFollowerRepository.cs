using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IUserFollowerRepository
    {
        public List<UserFollower> GetAllFollowerProfilesOrderedByFollowDate();
        public UserFollower GetFollowerProfileById(int id);
        public UserFollower VerifyFollowerStatus(int loggedInUserId, int profileUserId);
        public void Add(UserFollower userFollower);
        public void Update(UserFollower userFollower);
        public void Delete(int id);
    }
}
