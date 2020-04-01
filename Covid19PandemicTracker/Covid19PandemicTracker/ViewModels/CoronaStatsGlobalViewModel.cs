using Covid19PandemicTracker.Models;
using Covid19PandemicTracker.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Covid19PandemicTracker.ViewModels
{
    public class CoronaStatsGlobalViewModel:BaseViewModel
    {
        DataStore dataStore;
        private List<string> _countries;
        public List<string> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        private string _confirmed = "0";
        public string Confirmed
        {
            get => _confirmed;
            set => SetProperty(ref _confirmed, value);
        }
    
        private string _deaths = "0";
        public string Deaths
        {
            get => _deaths;
            set => SetProperty(ref _deaths, value);
        }

        private string _recovered = "0";
        public string Recovered
        {
            get => _recovered;
            set => SetProperty(ref _recovered, value);
        }

        private string _date;
        public string Date
        {
            get
            {
                if (string.IsNullOrEmpty(_date))
                {
                    var date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    var temp = date.Split('-');
                    int outparam;
                    _ = int.TryParse(temp[1], out outparam);
                    _date = $"Results as on: {temp[2]}-{outparam}-{temp[0]}";
                }
                return _date;
            }
        }
        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }
        public CoronaStatsGlobalViewModel()
        {
            dataStore = new DataStore();
        }

        public async Task GetCoronaFullJson()
        {
            await dataStore.GetCoronaFullJson();
        }

        public void FetchAllcountriesFromJson()
        {
            dataStore.FetchAllcountriesFromJson();
        }

        public async Task OnAppearingAsync()
        {
            try
            {
                IsBusy = true;
                //for invoking getter property of date
                var a = Date;
                await dataStore.GetCoronaFullJson();
                //getting the full country list from Json
                if (App.AllCountries.Count == 0)
                {
                    dataStore.FetchAllcountriesFromJson();
                }
                Countries = App.AllCountries;
                var index = Countries.FindIndex(x=> x=="India");
                SelectedIndex = index;
                IsBusy = false;
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Something went wrong while fetching result. Please try again later", "Ok");
                IsBusy = false;
            }            
        }

        public void PickerIndexChanged(string selectedCountry)
        {
            try
            {
                IsBusy = true;
                var date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                var temp = date.Split('-');
                int outparam;
                _ = int.TryParse(temp[1], out outparam);
                string newdate = $"{temp[0]}-{outparam}-{temp[2]}";
                var covidData = dataStore.GetCoronaDataCountryWise(selectedCountry, newdate);
                if (covidData != null)
                {
                    Confirmed = covidData.confirmed.ToString();
                    Deaths = covidData.deaths.ToString();
                    Recovered = covidData.recovered.ToString();
                }
            }
            catch (Exception)
            {
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
            
        }
    }
}
