using PublicPoo.ViewModels;
using PublicToilet.Common;
using Xamarin.Forms;

namespace PublicPoo.Views
{
    public partial class ToiletsPage : ContentPage
    {
        public ToiletsViewModel vm => BindingContext as ToiletsViewModel;
        public ToiletsPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedToilet = e.SelectedItem as Toilet;
            if (vm == null || selectedToilet == null) return;
            vm.NavigateToToiletDetailPage(selectedToilet);
        }
    }
}