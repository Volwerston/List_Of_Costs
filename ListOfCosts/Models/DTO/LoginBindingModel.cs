using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models.DTO
{
    public class LoginBindingModel
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public LoginBindingModel(string _n, string _p)
        {
            Name = _n;
            Password = _p;
        }

        public LoginBindingModel()
            : this("", "")
        {
        }
    }
}
