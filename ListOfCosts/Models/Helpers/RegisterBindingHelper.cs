using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models.Helpers
{
    public class RegisterBindingHelper : INotifyPropertyChanged
    {
        public RegisterBindingModel Model { get; set; }

        public RegisterBindingHelper()
            : this(new RegisterBindingModel())
        {
        }

        public RegisterBindingHelper(RegisterBindingModel _m)
        {
            Model = _m;

            LoginValidation = "";
            PasswordValidation = "";
            ConfirmPasswordValidation = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Validate()
        {
            return ValidateLogin() & ValidatePassword() & ValidateConfirmPassword();
        }


        #region Validation Properties

        private string loginValidation;

        private string passwordValidation;

        private string confirmPasswordValidation;

        public string LoginValidation
        {
            get
            {
                return loginValidation;
            }
            set
            {
                if(loginValidation != value)
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

        public string ConfirmPasswordValidation
        {
            get
            {
                return confirmPasswordValidation;
            }
            set
            {
                if (confirmPasswordValidation != value)
                {
                    confirmPasswordValidation = value;
                    OnPropertyChanged("ConfirmPasswordValidation");
                }
            }
        }

        #endregion

        #region Validation Methods

        private bool ValidateLogin()
        {
            if (string.IsNullOrWhiteSpace(Model.Login))
            {
                LoginValidation = "Login cannot be empty";
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
                PasswordValidation = "Password cannot be empty";
                return false;
            }
            else
            {
                PasswordValidation = "";
                return true;
            }
        }

        private bool ValidateConfirmPassword()
        {
            if (string.IsNullOrWhiteSpace(Model.ConfirmPassword))
            {
                ConfirmPasswordValidation = "Password cannot be empty";
                return false;
            }
            else if(Model.ConfirmPassword != Model.Password)
            {
                ConfirmPasswordValidation = "Passwords do not match";
                return false;
            }
            else
            {
                ConfirmPasswordValidation = "";
                return true;
            }
        }

        #endregion

    }
}
