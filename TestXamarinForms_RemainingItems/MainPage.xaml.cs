using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestXamarinForms_RemainingItems
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private static Lazy<MainPageViewModel> lazy = new Lazy<MainPageViewModel>(() => new MainPageViewModel());

        public MainPageViewModel Instance => lazy.Value;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = Instance;
        }

        private int _remainingItemsThresholdReachedCounter;

        private async void CollectionView_RemainingItemsThresholdReached(object sender, System.EventArgs e)
        {
            RemainingThresholdCount.Text = $"RemainingItemsThresholdReached {++_remainingItemsThresholdReachedCounter} time(s)";
        }
    }
}
