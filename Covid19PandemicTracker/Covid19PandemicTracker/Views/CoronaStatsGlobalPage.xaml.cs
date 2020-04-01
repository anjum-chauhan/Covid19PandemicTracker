using Covid19PandemicTracker.Models;
using Covid19PandemicTracker.Services;
using Covid19PandemicTracker.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19PandemicTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoronaStatsGlobalPage : ContentPage
    {
        CoronaStatsGlobalViewModel _viewModel;
        public CoronaStatsGlobalPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CoronaStatsGlobalViewModel();           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearingAsync();
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCountry = (string)((Picker)sender).SelectedItem;
            _viewModel.PickerIndexChanged(selectedCountry);
        }
    }
}