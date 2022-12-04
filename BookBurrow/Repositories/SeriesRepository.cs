using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class SeriesRepository : BaseRepository, ISeriesRepository
    {
        public SeriesRepository(IConfiguration configuration) : base(configuration) { }

        public List<Series> GetAllOrderedByName()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, name
                        FROM dbo.Series
                        ORDER BY name
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var series = new List<Series>();
                        while (reader.Read())
                        {
                            series.Add(new Series()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                Name = DbUtils.GetString(reader, "name"),
                            });
                        }

                        return series;
                    }
                }
            }
        }

        public Series GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, name
                        FROM dbo.Series
                        WHERE Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Series series = null;
                        if (reader.Read())
                        {
                            series = new Series()
                            {
                                Id = id,
                                Name = DbUtils.GetString(reader, "name"),
                            };
                        }
                        return series;
                    }
                }
            }
        }

        public void Add(Series series)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.Series (name)
                        OUTPUT INSERTED.id
                        VALUES (@name)
                    ";

                    DbUtils.AddParameter(cmd, "@name", series.Name);
                    series.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Series series)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.Series
                            SET name = @name
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@name", series.Name);
                    DbUtils.AddParameter(cmd, "@id", series.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.Series WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
