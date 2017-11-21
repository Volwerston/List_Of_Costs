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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ListOfCosts.ViewModels;

namespace ListOfCosts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            Register r = new Register();
            r.Show();
            this.Close();
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
                        ListWindow w = new ListWindow();
                        w.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password and/or login are invalid");
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
