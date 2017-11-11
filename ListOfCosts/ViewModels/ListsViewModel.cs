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
    public class ListsViewModel: DependencyObject
    {
        public ObservableCollection<Resource> Resources
        {
            get { return (ObservableCollection<Resource>)GetValue(ResourcesProperty); }
            set { SetValue(ResourcesProperty, value); }
        }

        public static readonly DependencyProperty ResourcesProperty =
            DependencyProperty.Register("ResourcesProperty", typeof(ObservableCollection<Resource>), typeof(ListsViewModel), new PropertyMetadata(new ObservableCollection<Resource>()));



        public ObservableCollection<Cost> Costs
        {
            get { return (ObservableCollection<Cost>)GetValue(CostsProperty); }
            set { SetValue(CostsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Costs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CostsProperty =
            DependencyProperty.Register("CostsProperty", typeof(ObservableCollection<Cost>), typeof(ListsViewModel), new PropertyMetadata(new ObservableCollection<Cost>()));


        public ListsViewModel()
        {
            Resources = new ObservableCollection<Resource>();

            ResourceDbStrategy strategy = (ResourceDbStrategy)DbContext.GetStartegy<Resource>();

            foreach(var resource in strategy.ReadAll())
            {
                Resources.Add(resource);
            }


            Costs = new ObservableCollection<Cost>();

            CostDbStrategy cDbStrategy = (CostDbStrategy)DbContext.GetStartegy<Cost>();

            foreach(var cost in cDbStrategy.ReadAll())
            {
                Costs.Add(cost);
            }
        }
    }
}
