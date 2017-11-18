using ListOfCosts.Models;
using ListOfCosts.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.db_client
{
    public class UserDbStrategy : IDbStrategy<RegisterBindingModel>
    {
        private static readonly IHasher _hasher = new MD5Hasher();

        public RegisterBindingModel Create(RegisterBindingModel source)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                string cmdString = "INSERT INTO Users VALUES(@n, @ph)";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    cmd.Parameters.AddWithValue("@n", source.Login);
                    cmd.Parameters.AddWithValue("@ph", _hasher.Hash(source.Password));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return source;
                }
            }
        }

        public RegisterBindingModel Update(RegisterBindingModel source)
        {
            throw new NotImplementedException();
        }

        public TResult Read<TParam, TResult>(TParam model) where TResult : class
        {
            UserIdentity identity =default(UserIdentity);
            LoginBindingModel m = model as LoginBindingModel;

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                string cmdString = "SELECT Id,Login FROM Users WHERE Login=@l AND PasswordHash=@ph";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {

                    cmd.Parameters.AddWithValue("@l", m.Name);
                    cmd.Parameters.AddWithValue("@ph", _hasher.Hash(m.Password));

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            identity = new UserIdentity()
                            {
                                Login = rdr["Login"].ToString(),
                                Id = int.Parse(rdr["Id"].ToString())
                            };
                        }
                    }
                }
            }

            return identity as TResult;
        }

        public IEnumerable<RegisterBindingModel> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
