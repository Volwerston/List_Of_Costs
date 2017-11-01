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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var dc = DataContext as RegisterViewModel;
            dc.SetPassword((sender as PasswordBox).Password);
        }

        private void confirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var dc = DataContext as RegisterViewModel;
            dc.SetConfirmPassword((sender as PasswordBox).Password);
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dc = DataContext as RegisterViewModel;

                if (dc.Validate())
                {
                    dc.Register();
                    MessageBox.Show("New user has been successfully registered");
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }
    }
}
