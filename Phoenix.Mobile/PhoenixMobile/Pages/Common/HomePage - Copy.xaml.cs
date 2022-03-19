using CongDongBau.PageModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CongDongBau.Pages.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void OnScrolled(object sender, ScrolledEventArgs e)
        {
            ScrollView scrollView = sender as ScrollView;
            var viewModel = BindingContext as HomePageModel;
            double scrollingSpace = scrollView.ContentSize.Height - scrollView.Height;

            if ((long)scrollingSpace-1 <= (long)e.ScrollY) // Touched bottom
                                             // Do the things you want to do
            {
                if (viewModel.ShowMoreCommand.CanExecute(null))
                    viewModel.ShowMoreCommand.Execute(null);
            }    
        }
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var lv = (ListView)sender;
            if (lv == null) return;
            lv.SelectedItem = null;
        }

        private void ViewCell_Appearing(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            UpdateListViewHeight(viewCell);
        }
        private void UpdateListViewHeight(ViewCell viewCell)
        {
            try
            {
                double height = 0;
                foreach (var cell in ListView16.ItemsSource)
                {
                    height +=
                    viewCell.View.Bounds.Height +
                    viewCell.View.Margin.Top +
                    viewCell.View.Margin.Bottom;
                }
                ListView16.HeightRequest = height;
            }
            catch { }
        }
    }
}