using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.db_client
{
    interface IHasher 
    {
        string Hash(string source);
    }
}
