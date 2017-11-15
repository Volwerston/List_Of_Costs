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
    class ResourceCategoryDbStrategy : IDbStrategy<Category>
    {
        public Category Create(Category source)
        {
            throw new NotImplementedException();
        }

        public TResult Read<TParam, TResult>(TParam param) where TResult : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> ReadAll()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("SELECT  * FROM ResourceType", con))
                {
                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        List<Category> toReturn = new List<Category>();

                        while (rdr.Read())
                        {
                            toReturn.Add(new Category()
                            {
                                Id = int.Parse(rdr["Id"].ToString()),
                                Name = rdr["Name"].ToString()
                            });
                        }

                        return toReturn;
                    }
                }
            }
        }
    }
}
