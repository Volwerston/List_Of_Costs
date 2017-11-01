using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models
{
    public class RegisterBindingModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterBindingModel(string _l, string _p, string _cp)
        {
            Login = _l;
            Password = _p;
            ConfirmPassword = _cp;
        }

        public RegisterBindingModel() 
            : this("", "", "")
        {
        }
    }
}
