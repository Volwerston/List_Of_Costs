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
    public class AddCostViewModel: DependencyObject
    {
        public ObservableCollection<Category> CostCategories
        {
            get { return (ObservableCollection<Category>)GetValue(CostCategoriesProperty); }
            set { SetValue(CostCategoriesProperty, value); }
        }

        public static readonly DependencyProperty CostCategoriesProperty =
            DependencyProperty.Register("Categories", typeof(ObservableCollection<Category>), typeof(AddCostViewModel), new PropertyMetadata(new ObservableCollection<Category>()));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(AddCostViewModel), new PropertyMetadata(""));

        public int SelectedCategory
        {
            get { return (int)GetValue(SelectedCategoryProperty); }
            set { SetValue(SelectedCategoryProperty, value); }
        }

        public static readonly DependencyProperty SelectedCategoryProperty =
            DependencyProperty.Register("SelectedCategory", typeof(int), typeof(AddCostViewModel), new PropertyMetadata(0));

        public AddCostViewModel()
        {
            CostCategoryDbStrategy s = new CostCategoryDbStrategy();

            foreach (var c in s.ReadAll())
            {
                CostCategories.Add(c);
            }
        }

        public void Add()
        {
            CostDbStrategy s = new CostDbStrategy();
            s.Create(new Cost()
            {
                CurrentWaste = 0,
                CostsType = new Category()
                {
                    Id = SelectedCategory,
                    Name = CostCategories.Where(x => x.Id == SelectedCategory).First().Name
                },
                Title = Title
            });
        }
    }
}
