using ListOfCosts.Models;
using ListOfCosts.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.db_client
{
    public static class DbContext
    {
        public static UserIdentity Identity { get; set; }

        public static object GetStartegy<T>()
        {
            if(typeof(T) == typeof(RegisterBindingModel))
            {
                return new UserDbStrategy();
            }

            return null;
        }
    }
}
