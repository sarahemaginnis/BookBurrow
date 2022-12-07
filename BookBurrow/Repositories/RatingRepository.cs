using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class RatingRepository : BaseRepository, IRatingRepository
    {
        public RatingRepository(IConfiguration configuration) : base(configuration) { }
        public List<Rating> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, displayValue
                            FROM dbo.Rating
                        ORDER BY displayValue
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var ratings = new List<Rating>();
                        while (reader.Read())
                        {
                            ratings.Add(new Rating()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                DisplayValue = reader.GetDecimal(reader.GetOrdinal("displayValue")),
                            });
                        }
                        return ratings;
                    }
                }
            }
        }

        public Rating GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT displayValue
                            FROM dbo.Rating
                        WHERE Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Rating rating = null;
                        if (reader.Read())
                        {
                            rating = new Rating()
                            {
                                Id = id,
                                DisplayValue = reader.GetDecimal(reader.GetOrdinal("displayValue")),
                            };
                        }
                        return rating;
                    }
                }
            }
        }

        public void Add(Rating rating)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.Rating (displayValue)
                        OUTPUT INSERTED.id
                        VALUES (@displayValue)
                    ";

                    cmd.Parameters.AddWithValue("@displayValue", rating.DisplayValue);

                    rating.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Rating rating)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.Rating
                            SET displayValue = @displayValue
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@displayValue", rating.DisplayValue);
                    DbUtils.AddParameter(cmd, "@id", rating.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM dbo.Rating WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
