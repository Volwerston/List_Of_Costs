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
    /// Interaction logic for MakeTransaction.xaml
    /// </summary>
    public partial class MakeTransaction : Window
    {
        public MakeTransaction(int resourceId, int costId)
        {
            InitializeComponent();
            DataContext = new MakeTransactionViewModel(resourceId, costId);
        }

        private void TB_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            TB.Text = TB.Text.ToUpper();

        }

        private void TB1_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            TB1.Text = TB1.Text.ToUpper();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MakeTransactionViewModel vm = DataContext as MakeTransactionViewModel;

                if (vm.Validate())
                {
                    vm.ConductTransaction();
                    this.Close();
                }

            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
