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
    class RegisterViewModel: DependencyObject, INotifyPropertyChanged
    {
        public RegisterBindingHelper RegisterHelper
        {
            get { return (RegisterBindingHelper)GetValue(RegisterHelperProperty); }
            set { SetValue(RegisterHelperProperty, value); }
        }

        public static readonly DependencyProperty RegisterHelperProperty =
            DependencyProperty.Register("RegisterHelperProperty", typeof(RegisterBindingHelper), typeof(RegisterViewModel), new PropertyMetadata(new RegisterBindingHelper()));

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetPassword(string pwd)
        {
            RegisterHelper.Model.Password = pwd;
        }

        public void SetConfirmPassword(string pwd)
        {
            RegisterHelper.Model.ConfirmPassword = pwd;
        }

        public void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Property_Changed(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public void Register()
        {

        }

        public RegisterViewModel()
        {
            RegisterHelper.PropertyChanged += Property_Changed;
        }

        public bool Validate()
        {
            return RegisterHelper.Validate();
        }
    }
}
