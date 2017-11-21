using ListOfCosts.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models.Helpers
{
    class LoginBindingHelper : INotifyPropertyChanged
    {
        public LoginBindingModel Model { get; set; }

        public LoginBindingHelper(LoginBindingModel model)
        {
            Model = model;

            LoginValidation = "";
            PasswordValidation = "";
        }

        public LoginBindingHelper()
            : this(new LoginBindingModel())
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Validate()
        {
            return ValidateLogin() & ValidatePassword();
        }

        #region Validation Properties

        private string loginValidation;

        private string passwordValidation;

        public string LoginValidation
        {
            get
            {
                return loginValidation;
            }
            set
            {
                if (loginValidation != value)
                {
                    loginValidation = value;
                    OnPropertyChanged("LoginValidation");
                }

            }
        }

        public string PasswordValidation
        {
            get
            {
                return passwordValidation;
            }
            set
            {
                if (passwordValidation != value)
                {
                    passwordValidation = value;
                    OnPropertyChanged("PasswordValidation");
                }
            }
        }

        #endregion

        #region Validation Methods

        private bool ValidateLogin()
        {
            if (string.IsNullOrWhiteSpace(Model.Name))
            {
                LoginValidation = "* Поле Login не може бути порожнім";
                return false;
            }
            else
            {
                LoginValidation = "";
                return true;
            }
        }

        private bool ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Model.Password))
            {
                PasswordValidation = "* Поле Password не може бути порожнім";
                return false;
            }
            else
            {
                PasswordValidation = "";
                return true;
            }
        }

        #endregion
    }
}
