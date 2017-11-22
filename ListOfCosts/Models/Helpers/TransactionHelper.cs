using ListOfCosts.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models.Helpers
{
    public class TransactionHelper : INotifyPropertyChanged
    {
        public Transaction Transaction { get; set; }
        
        public TransactionHelper(Transaction _t)
        {
            Transaction = _t;
        }

        public TransactionHelper()
            : this(new Transaction())
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Validation Helpers

        private string wasteValidation;

        public string WasteValidation
        {
            get
            {
                return wasteValidation;
            }
            set
            {
                if(wasteValidation != value)
                {
                    wasteValidation = value;
                    OnPropertyChanged("WasteValidation");
                }
            }
        }

        #endregion

        #region Validation Methods
        public bool ValidateWaste()
        {
            if(Transaction.Amount > Transaction.Resource.Amount)
            {
                WasteValidation = "Not enough money";
                return false;
            }
            else if(Transaction.Amount <= 0)
            {
                WasteValidation = "Wrong amount";
                return false;
            }

            return true;
        }

        #endregion
    }
}
