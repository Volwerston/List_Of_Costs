using ListOfCosts.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.db_client
{
    public class CostDbStrategy : IDbStrategy<Cost>
    {
        public Cost Create(Cost source)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                string cmdString = "INSERT INTO Costs VALUES(@ti, @ty, @cw)";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    cmd.Parameters.AddWithValue("@ti", source.Title);
                    cmd.Parameters.AddWithValue("@ty", source.Type);
                    cmd.Parameters.AddWithValue("@cw", source.CurrentWaste);

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

        public IEnumerable<Cost> ReadAll()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Costs", con))
                {

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<Cost> toReturn = new List<Cost>();

                        while (rdr.Read())
                        {
                            toReturn.Add(new Cost()
                            {
                                CurrentWaste = double.Parse(rdr["CurrentWaste"].ToString()),
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
