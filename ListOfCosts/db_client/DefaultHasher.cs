using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.db_client
{
    class DefaultHasher : IHasher
    {
        public string Hash(string source)
        {
            return source.GetHashCode().ToString();
        }
    }
}
