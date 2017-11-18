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
    /// Interaction logic for AddCost.xaml
    /// </summary>
    public partial class AddCost : Window
    {
        public AddCost()
        {
            InitializeComponent();
            DataContext = new AddCostViewModel();
            //Closing += AddCost_Closing;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddCostViewModel vm = DataContext as AddCostViewModel;
                vm.Add();
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}