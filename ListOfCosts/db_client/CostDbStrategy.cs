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
                string cmdString = "INSERT INTO Cost VALUES(@ti, @cw, @ty, @id)";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    cmd.Parameters.AddWithValue("@ti", source.Title);
                    cmd.Parameters.AddWithValue("@ty", source.CostsType.Id);
                    cmd.Parameters.AddWithValue("@cw", source.CurrentWaste);
                    cmd.Parameters.AddWithValue("@id", DbContext.Identity.Id);

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
                using (SqlCommand cmd = new SqlCommand(@"SELECT A.Name as CostName, B.Name as TypeName, A.CurrWastes, A.Id, B.Id as TypeId
                                                         FROM Cost A 
                                                         INNER JOIN CostsType B
                                                         ON A.TypeId = B.Id
                                                         Where A.OwnerId=@id", con))
                {
                    cmd.Parameters.AddWithValue("@id", DbContext.Identity.Id);
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<Cost> toReturn = new List<Cost>();

                        while (rdr.Read())
                        {
                            toReturn.Add(new Cost()
                            {
                                CurrentWaste = double.Parse(rdr["CurrWastes"].ToString()),
                                Id = int.Parse(rdr["Id"].ToString()),
                                Title = rdr["CostName"].ToString(),
                                CostsType = new Category()
                                {
                                    Id = int.Parse(rdr["TypeId"].ToString()),
                                    Name = rdr["TypeName"].ToString()
                                }
                            });
                        }

                        return toReturn;
                    }
                }
            }
        }
    }
}
