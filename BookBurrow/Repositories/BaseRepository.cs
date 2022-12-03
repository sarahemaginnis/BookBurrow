using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BookBurrow.Repositories
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection Connection => new SqlConnection(_connectionString);
    }
}
