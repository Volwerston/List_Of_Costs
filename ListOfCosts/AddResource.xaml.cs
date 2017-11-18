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
    public partial class AddResource : Window
    {
        public AddResource()
        {
            Closing += AddResource_Closing;
            InitializeComponent();
            DataContext = new AddResourceViewModel();
        }

        public AddResource(int resourceId)
        {
            Closing += AddResource_Closing;
            InitializeComponent();
            DataContext = new AddResourceViewModel(resourceId);
        }

        private void AddResource_Closing(object sender, EventArgs e)
        {
            AddResourceViewModel vm = DataContext as AddResourceViewModel;
            vm.Categories.Clear();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddResourceViewModel vm = DataContext as AddResourceViewModel;
                vm.Add();
                this.Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
