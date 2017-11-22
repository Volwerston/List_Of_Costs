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
    public class TransactionDbStrategy : IDbStrategy<Transaction>
    {
        public Transaction Create(Transaction source)
        {
            using(SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                string cmdString = "INSERT INTO Transactions VALUES(@ri, @ci, @a, @d, @c);";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    cmd.Parameters.AddWithValue("@ri", source.Resource.Id);
                    cmd.Parameters.AddWithValue("@ci", source.Cost.Id);
                    cmd.Parameters.AddWithValue("@a", source.Amount);
                    cmd.Parameters.AddWithValue("@d", source.Date);
                    cmd.Parameters.AddWithValue("@c", source.Comment);

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

        public IEnumerable<Transaction> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Transaction Update(Transaction source)
        {
            throw new NotImplementedException();
        }
    }
}
