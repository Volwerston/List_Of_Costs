using ListOfCosts.db_client;
using ListOfCosts.Models.DTO;
using ListOfCosts.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ListOfCosts.ViewModels
{
    public class MakeTransactionViewModel : DependencyObject, INotifyPropertyChanged
    {
        public TransactionHelper TransactionHelper
        {
            get { return (TransactionHelper)GetValue(TransactionHelperProperty); }
            set { SetValue(TransactionHelperProperty, value); }
        }

        public static readonly DependencyProperty TransactionHelperProperty =
            DependencyProperty.Register("TransactionHelper", typeof(TransactionHelper), typeof(MakeTransactionViewModel), new PropertyMetadata(new TransactionHelper()));

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public void ConductTransaction()
        {
            TransactionDbStrategy dbs = new TransactionDbStrategy();
            if(dbs.Create(TransactionHelper.Transaction) != null)
            {
                ResourceDbStrategy ds = new ResourceDbStrategy();

                Resource toUpdate = TransactionHelper.Transaction.Resource;
                toUpdate.Amount -= TransactionHelper.Transaction.Amount;

                ds.Update(toUpdate);

                CostDbStrategy cds = new CostDbStrategy();

                Cost ctu = TransactionHelper.Transaction.Cost;
                ctu.CurrentWaste += TransactionHelper.Transaction.Amount;

                cds.Update(ctu);
            }
        }

        public MakeTransactionViewModel(int resourceId, int costId)
        {
   
            ResourceDbStrategy rds = new ResourceDbStrategy();
            Resource r = rds.Read<int, Resource>(resourceId);

            CostDbStrategy cds = new CostDbStrategy();
            Cost c = cds.Read<int, Cost>(costId);

            TransactionHelper = new TransactionHelper()
            {
                Transaction = new Transaction()
                {
                    Cost = c,
                    Resource = r
                }
            };

            TransactionHelper.PropertyChanged += TransactionHelper_PropertyChanged;
        }

        private void TransactionHelper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(sender, e);
        }

        public bool Validate()
        {
            return TransactionHelper.ValidateWaste();
        }
    }
}
