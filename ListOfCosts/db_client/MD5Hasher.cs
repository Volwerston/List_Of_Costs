using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.db_client
{
    public class MD5Hasher : IHasher
    {
        public string Hash(string source)
        {
            MD5 hasher = MD5.Create();

            byte[] data =hasher.ComputeHash(Encoding.UTF8.GetBytes(source));


            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
