using System.ComponentModel;

namespace InfiniteScrollingMauiVersion
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }

        private int _remainingItemsThresholdReachedCounter;

        private void CollectionView_RemainingItemsThresholdReached(object sender, System.EventArgs e)
        {
            RemainingThresholdCountLabel.Text = $"RemainingItemsThresholdReached {++_remainingItemsThresholdReachedCounter} time(s)";
        }
    }
}
