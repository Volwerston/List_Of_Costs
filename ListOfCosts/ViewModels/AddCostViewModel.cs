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
        private readonly int _id = 0;

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

        public double CurrentWastes
        {
            get { return (double)GetValue(CurrentWastesProperty); }
            set { SetValue(CurrentWastesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentWastes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentWastesProperty =
            DependencyProperty.Register("CurrentWastes", typeof(double), typeof(AddCostViewModel), new PropertyMetadata(0.00));


        public AddCostViewModel()
        {
            CostCategoryDbStrategy s = new CostCategoryDbStrategy();

            foreach (var c in s.ReadAll())
            {
                CostCategories.Add(c);
            }
        }

        public AddCostViewModel(int id)
        {
            _id = id;

            CostCategoryDbStrategy s = new CostCategoryDbStrategy();

            foreach (var c in s.ReadAll())
            {
                CostCategories.Add(c);
            }

            Cost toEdit = new CostDbStrategy().Read<int, Cost>(id);

            Title = toEdit.Title;
            SelectedCategory = toEdit.CostsType.Id;
            CurrentWastes = toEdit.CurrentWaste;
        }

        public void Add()
        {
            CostDbStrategy s = new CostDbStrategy();
            Cost c = new Cost()
            {
                Id = _id,
                CurrentWaste = CurrentWastes,
                CostsType = new Category()
                {
                    Id = SelectedCategory,
                    Name = CostCategories.Where(x => x.Id == SelectedCategory).First().Name
                },
                Title = Title
            };

            if (c.Id == 0)
            {
                s.Create(c);
            }
            else
            {
                s.Update(c);
            }
        }
    }
}
