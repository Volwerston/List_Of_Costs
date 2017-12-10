using ListOfCosts.db_client;
using ListOfCosts.Models.DTO;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ListOfCosts.ViewModels
{
    public class GlobalStatsViewModel: DependencyObject
    {
        public ObservableCollection<int> Year
        {
            get { return (ObservableCollection<int>)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }


        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(ObservableCollection<int>), typeof(GlobalStatsViewModel), new PropertyMetadata(new ObservableCollection<int>()));


        public ObservableCollection<string> Month
        {
            get { return (ObservableCollection<string>)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }

        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(ObservableCollection<string>), typeof(GlobalStatsViewModel), new PropertyMetadata(new ObservableCollection<string>()));

        public int SelectedYear
        {
            get { return (int)GetValue(SelectedYearProperty); }
            set { SetValue(SelectedYearProperty, value); }
        }

        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.Register("SelectedYear", typeof(int), typeof(GlobalStatsViewModel), new PropertyMetadata(1900));
        public string SelectedMonth
        {
            get { return (string)GetValue(SelectedMonthProperty); }
            set { SetValue(SelectedMonthProperty, value); }
        }

        public static readonly DependencyProperty SelectedMonthProperty =
            DependencyProperty.Register("SelectedMonth", typeof(string), typeof(GlobalStatsViewModel), new PropertyMetadata("Січень"));

        public ObservableCollection<Transaction> FoundTransactions
        {
            get { return (ObservableCollection<Transaction>)GetValue(FoundTransactionsProperty); }
            set { SetValue(FoundTransactionsProperty, value); }
        }

        public static readonly DependencyProperty FoundTransactionsProperty =
            DependencyProperty.Register("FoundTransactions", typeof(ObservableCollection<Transaction>), typeof(GlobalStatsViewModel), new PropertyMetadata(new ObservableCollection<Transaction>()));


        public string  GraphTitle
        {
            get { return (string)GetValue(GraphTitleProperty); }
            set { SetValue(GraphTitleProperty, value); }
        }

        public static readonly DependencyProperty GraphTitleProperty =
            DependencyProperty.Register("GraphTitle", typeof(string), typeof(GlobalStatsViewModel), new PropertyMetadata("Wastes Graph"));

        public ObservableCollection<DataPoint> Points
        {
            get { return (ObservableCollection<DataPoint>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }


        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(ObservableCollection<DataPoint>), typeof(GlobalStatsViewModel), new PropertyMetadata(new ObservableCollection<DataPoint>()));


        public GlobalStatsViewModel()
        {
            foreach (var month in this.GetMonths())
            {
                Month.Add(month);
            }

            foreach (var year in this.GetYears())
            {
                Year.Add(year);
            }
        }


        public void Search()
        {
            FoundTransactions.Clear();
            Points.Clear();

            int month = this.GetMonthNumber(SelectedMonth);

            foreach (var transaction in this.SearchTransactions(SelectedYear, month, 0, 0))
            {
                FoundTransactions.Add(transaction);
            }

            foreach(var point in this.GenerateGraphPoints())
            {
                Points.Add(point);
            }
        }



        #region Helper Methods

        private List<DataPoint> GenerateGraphPoints()
        {
            List<DataPoint> toReturn = new List<DataPoint>();

            foreach(var date in this.GetDates())
            {
                double _x = date;
                double _y = 0;

                var temp = FoundTransactions
                            .Where(x => x.Date.Day == date);

                if (temp.Count() > 0)
                { 
                    _y =    temp
                            .Select(x => x.Amount)
                            .Aggregate((x, y) => x + y);
                }

                toReturn.Add(new DataPoint(_x, _y));
            }

            return toReturn;
        }

        private List<int> GetYears()
        {
            List<int> toReturn = new List<int>();

            for (int i = 1900; i <= DateTime.Now.Year; ++i)
            {
                toReturn.Add(i);
            }

            return toReturn;
        }

        private List<int> GetDates()
        {
            List<int> toReturn = new List<int>();

            for (int i = 1; i <= 31; ++i)
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
