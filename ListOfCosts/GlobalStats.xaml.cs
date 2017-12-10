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
    /// Interaction logic for GlobalStats.xaml
    /// </summary>
    public partial class GlobalStats : Window
    {
        public GlobalStats()
        {
            InitializeComponent();
            DataContext = new GlobalStatsViewModel();
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            GlobalStatsViewModel vm = DataContext as GlobalStatsViewModel;
            vm.Search();
        }
    }
}
