using ListOfCosts.db_client;
using ListOfCosts.Models.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ListOfCosts.ViewModels
{
    public class LocalStatsViewModel: DependencyObject
    {
        private readonly int costId = 0;

        public ObservableCollection<int> Year
        {
            get { return (ObservableCollection<int>)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(ObservableCollection<int>), typeof(LocalStatsViewModel), new PropertyMetadata(new ObservableCollection<int>()));


        public ObservableCollection<string> Month
        {
            get { return (ObservableCollection<string>)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }

        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(ObservableCollection<string>), typeof(LocalStatsViewModel), new PropertyMetadata(new ObservableCollection<string>()));

        public ObservableCollection<int> Date
        {
            get { return (ObservableCollection<int>)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }


        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(ObservableCollection<int>), typeof(LocalStatsViewModel), new PropertyMetadata(new ObservableCollection<int>()));

        public int SelectedYear
        {
            get { return (int)GetValue(SelectedYearProperty); }
            set { SetValue(SelectedYearProperty, value); }
        }

        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.Register("SelectedYear", typeof(int), typeof(LocalStatsViewModel), new PropertyMetadata(2010));

        public int SelectedDate
        {
            get { return (int)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(int), typeof(LocalStatsViewModel), new PropertyMetadata(1));

        public string SelectedMonth
        {
            get { return (string)GetValue(SelectedMonthProperty); }
            set { SetValue(SelectedMonthProperty, value); }
        }

        public static readonly DependencyProperty SelectedMonthProperty =
            DependencyProperty.Register("SelectedMonth", typeof(string), typeof(LocalStatsViewModel), new PropertyMetadata("Січень"));

        public ObservableCollection<Transaction> FoundTransactions
        {
            get { return (ObservableCollection<Transaction>)GetValue(FoundTransactionsProperty); }
            set { SetValue(FoundTransactionsProperty, value); }
        }

        public static readonly DependencyProperty FoundTransactionsProperty =
            DependencyProperty.Register("FoundTransactions", typeof(ObservableCollection<Transaction>), typeof(LocalStatsViewModel), new PropertyMetadata(new ObservableCollection<Transaction>()));

        public LocalStatsViewModel(int _costId)
        {
            Month.Clear();
            Year.Clear();
            Date.Clear();

            foreach(var month in this.GetMonths())
            {
                Month.Add(month);
            }

            foreach(var year in this.GetYears())
            {
                Year.Add(year);
            }

            foreach(var date in this.GetDates())
            {
                Date.Add(date);
            }

            costId = _costId;
        }


        public void Search()
        {
            FoundTransactions.Clear();

            int month = this.GetMonthNumber(SelectedMonth);

            foreach(var transaction in this.SearchTransactions(SelectedYear, month, SelectedDate, costId))
            {
                FoundTransactions.Add(transaction);
            }
        }


        #region Helper Methods

        private List<int> GetYears()
        {
            List<int> toReturn = new List<int>();

            for(int i = 2010; i <= DateTime.Now.Year; ++i)
            {
                toReturn.Add(i);
            }

            return toReturn;
        }

        private List<string> GetMonths()
        {
            List<string> toReturn = new List<string>()
            {
                "Січень",
                "Лютий",
                "Березень",
                "Квітень",
                "Травень",
                "Червень",
                "Липень",
                "Серпень",
                "Вересень",
                "Жовтень",
                "Листопад",
                "Грудень"
            };

            return toReturn;
        }

        private List<int> GetDates()
        {
            List<int> toReturn = new List<int>();

            for(int i = 1; i <= 31; ++i)
            {
                toReturn.Add(i);
            }

            return toReturn;
        }

        private List<Transaction> SearchTransactions(int year, int month, int date, int costId)
        {
            Tuple<int, int, int, int> criteria = new Tuple<int, int, int, int>(year, month, date, costId);

            TransactionDbStrategy dbs = new TransactionDbStrategy();

            List<Transaction> transactions = dbs.Read<Tuple<int, int, int, int>, List<Transaction>>(criteria);

            return transactions;
        }

        private int GetMonthNumber(string month)
        {
            switch (month)
            {
                case "Січень":
                    return 1;
                case "Лютий":
                    return 2;
                case "Березень":
                    return 3;
                case "Квітень":
                    return 4;
                case "Травень":
                    return 5;
                case "Червень":
                    return 6;
                case "Липень":
                    return 7;
                case "Серпень":
                    return 8;
                case "Вересень":
                    return 9;
                case "Жовтень":
                    return 10;
                case "Листопад":
                    return 11;
                case "Грудень":
                    return 12;
                default:
                    throw new Exception("Wrong month");
            }
        }

        
        #endregion

    }
}
