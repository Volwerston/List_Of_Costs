using ListOfCosts;
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
    public partial class ListWindow : Window
    {
        public ListWindow()
        {
            InitializeComponent();
            DataContext = new ListsViewModel();
        }

        protected virtual void Add_Resource(object sender, EventArgs e)
        {
            AddResource ar = new AddResource();
            ar.Closing += (DataContext as ListsViewModel).Refresh;
            ar.Show();
        }

        private void Add_Cost(object sender, RoutedEventArgs e)
        {
            AddCost ac = new AddCost();
            ac.Closing += (DataContext as ListsViewModel).Refresh;
            ac.Show();
        }

        private void Edit_Cost(object sender, MouseButtonEventArgs e)
        {
            int costId = (int)((Border)sender).Tag;

            AddCost ac = new AddCost(costId, true);
            ac.Closing += (DataContext as ListsViewModel).Refresh;

            ac.Show();
        }

        private void Edit_Resource(object sender, MouseButtonEventArgs e)
        {
            int resourceId = (int)((Border)sender).Tag;

            AddResource ac = new AddResource(resourceId);
            ac.Closing += (DataContext as ListsViewModel).Refresh;

            ac.Show();
        }

        private void StackPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("Object", this);
                data.SetData(DataFormats.StringFormat, sender);



                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void StackPanel_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                Mouse.SetCursor(Cursors.Pen);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            int resourceId = (int)((StackPanel)e.Data.GetData(DataFormats.StringFormat)).Tag;
            int wasteId = (int)((StackPanel)sender).Tag;

            MakeTransaction mt = new MakeTransaction(resourceId, wasteId);
            mt.Closing += (DataContext as ListsViewModel).Refresh;
            mt.Show();
        }

        private void menuOpen(object sender, RoutedEventArgs e)
        {
        
            if (menuBackground.Visibility == Visibility.Hidden)
            {
                menuBackground.Visibility = Visibility.Visible;
                menuBtn1.Visibility = Visibility.Hidden;
                menuBtn2.Visibility = Visibility.Visible;
                helpBtn.Visibility = Visibility.Visible;
                contactBtn.Visibility = Visibility.Visible;
                logoutBtn.Visibility = Visibility.Visible;
                statisticsBtn.Visibility = Visibility.Visible;
            }
            else if(menuBackground.Visibility == Visibility.Visible)
            {
                menuBtn1.Visibility = Visibility.Visible;
                menuBtn2.Visibility = Visibility.Hidden;
                menuBackground.Visibility = Visibility.Hidden;
                helpBtn.Visibility = Visibility.Hidden;
                contactBtn.Visibility = Visibility.Hidden;
                logoutBtn.Visibility = Visibility.Hidden;
                statisticsBtn.Visibility = Visibility.Hidden;

            }
        }

        private void openHelp(object sender, RoutedEventArgs e)
        {
            addBackground.Visibility = Visibility.Visible;
            helpLabel.Visibility = Visibility.Visible;
            closeBtn.Visibility = Visibility.Visible;
            contactLabel.Visibility = Visibility.Hidden;
        }

        private void closeAddMenu(object sender, RoutedEventArgs e)
        {
            addBackground.Visibility = Visibility.Hidden;
            helpLabel.Visibility = Visibility.Hidden;
            contactLabel.Visibility = Visibility.Hidden;
            closeBtn.Visibility = Visibility.Hidden;
        }

        private void openContact(object sender, RoutedEventArgs e)
        {
            addBackground.Visibility = Visibility.Visible;
            contactLabel.Visibility = Visibility.Visible;
            closeBtn.Visibility = Visibility.Visible;
            helpLabel.Visibility = Visibility.Hidden;
        }

        private void openStatistic(object sender, RoutedEventArgs e)
        {
            GlobalStats gs = new GlobalStats();
            gs.Show();
        }

        private void logOut(object sender, RoutedEventArgs e)
        {       
                MainWindow w = new MainWindow();
                w.Show();
                this.Close();
        }
    }
}
