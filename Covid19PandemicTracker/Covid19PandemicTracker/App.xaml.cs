using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Covid19PandemicTracker.Services;
using Covid19PandemicTracker.Views;
using System.Collections.Generic;

namespace Covid19PandemicTracker
{
    public partial class App : Application
    {
        public static List<string> AllCountries = new List<string>();
        public static string CompleteCoronaJsonData = string.Empty;
        public App()
        {
            InitializeComponent();
            MainPage = new CoronaStatsGlobalPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
