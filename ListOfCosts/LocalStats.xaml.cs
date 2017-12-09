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
    /// Interaction logic for LocalStats.xaml
    /// </summary>
    public partial class LocalStats : Window
    {
        public LocalStats(int costId)
        {
            InitializeComponent();
            DataContext = new LocalStatsViewModel(costId);
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            LocalStatsViewModel vm = DataContext as LocalStatsViewModel;
            vm.Search();
        }
    }
}
