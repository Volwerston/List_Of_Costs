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
            Tuple<int, int, int, int> toSearch = param as Tuple<int, int, int, int>;
            List<Transaction> toReturn = new List<Transaction>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("exec spSearchTransactions @y, @m, @d, @id, @oid", con))
                {

                    cmd.Parameters.AddWithValue("@y", toSearch.Item1);
                    cmd.Parameters.AddWithValue("@m", toSearch.Item2);
                    cmd.Parameters.AddWithValue("@d", toSearch.Item3);
                    cmd.Parameters.AddWithValue("@id", toSearch.Item4);
                    cmd.Parameters.AddWithValue("@oid", DbContext.Identity.Id);

                    con.Open();

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            toReturn.Add(new Transaction()
                            {
                                Amount = int.Parse(rdr["Amount"].ToString()),
                                Comment = rdr["Comment"].ToString(),
                                Date = DateTime.Parse(rdr["DateTime"].ToString()),
                                Id = int.Parse(rdr["Id"].ToString())
                            });
                        }
                    } 
                }
            }

                return toReturn as TResult;
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
