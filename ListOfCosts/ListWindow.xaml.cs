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
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        public ListWindow()
        {
            InitializeComponent();
            DataContext = new ListsViewModel();
        }

        protected virtual void Add_Resource(object sender, EventArgs e)
        {
            AddResource ar = new AddResource();
            ar.Closing += (DataContext as ListsViewModel).Refresh;
            ar.Show();
        }

        private void Add_Cost(object sender, RoutedEventArgs e)
        {
            AddCost ac = new AddCost();
            ac.Closing += (DataContext as ListsViewModel).Refresh;
            ac.Show();
        }

        private void Edit_Cost(object sender, MouseButtonEventArgs e)
        {
            int costId = (int)((Border)sender).Tag;

            AddCost ac = new AddCost(costId);
            ac.Closing += (DataContext as ListsViewModel).Refresh;

            ac.Show();
        }

        private void Edit_Resource(object sender, MouseButtonEventArgs e)
        {
            int resourceId = (int)((Border)sender).Tag;

            AddResource ac = new AddResource(resourceId);
            ac.Closing += (DataContext as ListsViewModel).Refresh;

            ac.Show();
        }
    }
}
