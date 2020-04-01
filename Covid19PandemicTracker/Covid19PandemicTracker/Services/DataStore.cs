using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Covid19PandemicTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Covid19PandemicTracker.Services
{
    public class DataStore
    {
        HttpClient httpClient;
        public DataStore()
        {
            httpClient = new HttpClient();
        }

        public Country GetCoronaDataCountryWise(string country, string date)
        {
            //var json = await httpClient.GetStringAsync("https://pomber.github.io/covid19/timeseries.json");
            JObject jsonObject = JObject.Parse(App.CompleteCoronaJsonData);
            string jsonString = jsonObject[country].ToString();
            return JsonConvert.DeserializeObject<List<Country>>(jsonString).FirstOrDefault(x => x.date == date);
        }

        public async Task GetCoronaFullJson()
        {
            try
            {
                if (string.IsNullOrEmpty(App.CompleteCoronaJsonData))
                {
                    App.CompleteCoronaJsonData = await httpClient.GetStringAsync("https://pomber.github.io/covid19/timeseries.json");
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("something went wrong while fetching Json data");
            }
        }

        public void FetchAllcountriesFromJson()
        {
            try
            {
                if (!string.IsNullOrEmpty(App.CompleteCoronaJsonData))
                {
                    JObject rss = JObject.Parse(App.CompleteCoronaJsonData);
                    foreach (var item in rss)
                    {
                        App.AllCountries.Add(item.Key.ToString());
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("something went wrong while fetching countries from Json");
            }
            

        }
    }
}