using ListOfCosts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ListOfCosts
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginViewModel model = DataContext as LoginViewModel;
            model.SetPassword((sender as PasswordBox).Password);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginViewModel model = DataContext as LoginViewModel;

                if (model.Validate())
                {
                    if (model.AuthenticateUser())
                    {
                        MessageBox.Show("User authenticated");
                    }
                    else
                    {
                        MessageBox.Show("Password and/or login are invalid");
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
    }
}
