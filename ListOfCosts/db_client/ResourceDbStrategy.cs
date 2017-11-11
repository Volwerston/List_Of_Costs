using ListOfCosts.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ListOfCosts.db_client
{
    public class ResourceDbStrategy : IDbStrategy<Resource>
    {
        public Resource Create(Resource source)
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                string cmdString = "INSERT INTO Resources VALUES(@ti, @ty, @am)";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    cmd.Parameters.AddWithValue("@ti", source.Title);
                    cmd.Parameters.AddWithValue("@ty", source.Type);
                    cmd.Parameters.AddWithValue("@am", source.Amount);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return source;
                }
            }
        }

        public TResult Read<TParam, TResult>(TParam param) where TResult : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Resource> ReadAll()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Resources", con))
                {

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<Resource> toReturn = new List<Resource>();

                        while (rdr.Read())
                        {
                            toReturn.Add(new Resource() {
                                Amount = double.Parse(rdr["Amount"].ToString()),
                                Id = int.Parse(rdr["Id"].ToString()),
                                Title = rdr["Title"].ToString(),
                                Type = rdr["Type"].ToString()
                            });
                        }

                        return toReturn;
                    }
                }
            }
        }
    }
}
