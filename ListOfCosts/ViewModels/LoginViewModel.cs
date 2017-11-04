using ListOfCosts.db_client;
using ListOfCosts.Models;
using ListOfCosts.Models.DTO;
using ListOfCosts.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ListOfCosts.ViewModels
{
    class LoginViewModel: DependencyObject, INotifyPropertyChanged
    {
        public LoginBindingHelper LoginHelper
        {
            get { return (LoginBindingHelper)GetValue(LoginHelperProperty); }
            set { SetValue(LoginHelperProperty, value); }
        }

        public static readonly DependencyProperty LoginHelperProperty =
            DependencyProperty.Register("LoginHelperProperty", typeof(LoginBindingHelper), typeof(LoginViewModel), new PropertyMetadata(new LoginBindingHelper()));

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            LoginHelper.PropertyChanged += Property_Changed;
        }

        public void SetPassword(string pwd)
        {
            LoginHelper.Model.Password = pwd;
        }

        public bool AuthenticateUser()
        {

            UserDbStrategy strategy = DbContext.GetStartegy<RegisterBindingModel>() as UserDbStrategy;

            UserIdentity identity = strategy.Read<LoginBindingModel, UserIdentity>(LoginHelper.Model);

            if (identity != null)
            {
                DbContext.Identity = identity;
                return true;
            }
            else
            {
                DbContext.Identity = null;
                return false;
            }
        }

        public bool Validate()
        {
            return LoginHelper.Validate();
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Property_Changed(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}
