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
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                string cmdString = "INSERT INTO Resource VALUES(@ti, @am, @ty, @id)";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    cmd.Parameters.AddWithValue("@ti", source.Title);
                    cmd.Parameters.AddWithValue("@ty", source.ResourceType.Id);
                    cmd.Parameters.AddWithValue("@am", source.Amount);
                    cmd.Parameters.AddWithValue("@id", DbContext.Identity.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return source;
                }
            }
        }

        public TResult Read<TParam, TResult>(TParam param) where TResult : class
        {
            dynamic resouceId = param;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT A.Id as ResourceId, A.Name as ResourceName, A.Amount, B.Name as CategoryName, B.Id as TypeId
                                                         FROM Resource A 
                                                         Inner Join ResourceType B
                                                         ON A.TypeId =  B.Id
                                                         Where A.OwnerId=@oid AND A.Id=@id", con))
                {
                    cmd.Parameters.AddWithValue("@oid", DbContext.Identity.Id);
                    cmd.Parameters.AddWithValue("@id", resouceId);

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        Resource toReturn = null;


                        if (rdr.Read())
                        {
                            toReturn = new Resource()
                            {
                                Amount = double.Parse(rdr["Amount"].ToString()),
                                Id = int.Parse(rdr["ResourceId"].ToString()),
                                Title = rdr["ResourceName"].ToString(),
                                ResourceType = new Category()
                                {
                                    Id = int.Parse(rdr["TypeId"].ToString()),
                                    Name = rdr["categoryName"].ToString()
                                }
                            };
                        }

                        return toReturn as TResult;
                    }
                }
            }
        }
    

        public Resource Update(Resource source)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                string cmdString = @"UPDATE Resource
                                     Set Name=@n, Amount=@am, TypeId=@tid
                                     Where (OwnerId=@oid) AND (Id=@id)
                                    ";

                using(SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    cmd.Parameters.AddWithValue("@n", source.Title);
                    cmd.Parameters.AddWithValue("@am", source.Amount);
                    cmd.Parameters.AddWithValue("@tid", source.ResourceType.Id);

                    cmd.Parameters.AddWithValue("@oid", DbContext.Identity.Id);
                    cmd.Parameters.AddWithValue("@id", source.Id);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return source;
                }
            }
        }

        public IEnumerable<Resource> ReadAll()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(@"SELECT A.Id as ResourceId, A.Name as ResourceName, A.Amount, B.Name as CategoryName, B.Id as TypeId
                                                         FROM Resource A 
                                                         Inner Join ResourceType B
                                                         ON A.TypeId =  B.Id
                                                         Where A.OwnerId=@id
                                                         Order By A.Id desc", con))
                {
                    cmd.Parameters.AddWithValue("@id", DbContext.Identity.Id);

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<Resource> toReturn = new List<Resource>();

                        while (rdr.Read())
                        {
                            toReturn.Add(new Resource() {
                                Amount = double.Parse(rdr["Amount"].ToString()),
                                Id = int.Parse(rdr["ResourceId"].ToString()),
                                Title = rdr["ResourceName"].ToString(),
                                ResourceType = new Category()
                                {
                                    Id = int.Parse(rdr["TypeId"].ToString()),
                                    Name = rdr["categoryName"].ToString()
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
