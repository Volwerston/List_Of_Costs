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
            Closing += AddCost_Closing;
            InitializeComponent();
            DataContext = new AddCostViewModel();
        }

        public AddCost(int costId)
        {
            Closing += AddCost_Closing;
            InitializeComponent();
            DataContext = new AddCostViewModel(costId);
        }

        private void AddCost_Closing(object sender, EventArgs e)
        {
            AddCostViewModel vm = DataContext as AddCostViewModel;
            vm.CostCategories.Clear();
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