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
                string cmdString = "INSERT INTO Cost VALUES(@ti, @ty, @cw, @id)";

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
            dynamic costId = param;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT A.Name as CostName, B.Name as TypeName, A.CurrentWaste, A.Id, B.Id as TypeId
                                                         FROM Cost A 
                                                         INNER JOIN CostsType B
                                                         ON A.TypeId = B.Id
                                                         Where (A.OwnerId=@id) AND (A.Id = @cid)", con))
                {
                    cmd.Parameters.AddWithValue("@id", DbContext.Identity.Id);
                    cmd.Parameters.AddWithValue("@cid", costId);

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        Cost toReturn = null;

                        if (rdr.Read())
                        {
                            toReturn = new Cost()
                            {
                                CurrentWaste = double.Parse(rdr["CurrentWaste"].ToString()),
                                Id = int.Parse(rdr["Id"].ToString()),
                                Title = rdr["CostName"].ToString(),
                                CostsType = new Category()
                                {
                                    Id = int.Parse(rdr["TypeId"].ToString()),
                                    Name = rdr["TypeName"].ToString()
                                }
                            };
                        }


                        return toReturn as TResult;
                    }
                }
            }
        } 

        public Cost Update(Cost source)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                string cmdString = @"UPDATE Cost 
                                    Set Name=@name, TypeId=@tid, CurrentWaste=@cw
                                    Where (OwnerId=@oid) and (Id=@id)";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    cmd.Parameters.AddWithValue("@id", source.Id);
                    cmd.Parameters.AddWithValue("@oid", DbContext.Identity.Id);

                    cmd.Parameters.AddWithValue("@name", source.Title);
                    cmd.Parameters.AddWithValue("@tid", source.CostsType.Id);
                    cmd.Parameters.AddWithValue("@cw", source.CurrentWaste);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return source;
                }
            }
        }

        public IEnumerable<Cost> ReadAll()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT A.Name as CostName, B.Name as TypeName, A.CurrentWaste, A.Id, B.Id as TypeId
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
                                CurrentWaste = double.Parse(rdr["CurrentWaste"].ToString()),
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
