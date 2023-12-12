using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BaseUrl = "http://dataservice.accuweather.com/";
        public const string AutocompleteEndpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CurrentConditionsEndpoint = "currentconditions/v1/{0}?apikey={1}";
        public const string ApiKey = "uGznbB939HG692MdWILJgKTZXcRrC8Km";

        public static async Task<IEnumerable<City>> GetCities(string query)
        {
            string url = $"{BaseUrl}{string.Format(AutocompleteEndpoint, ApiKey, query)}";
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Failed to load cities by query: {query}", null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var cities = JsonConvert.DeserializeObject<List<City>>(jsonResponse);

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            string url = $"{BaseUrl}{string.Format(CurrentConditionsEndpoint, cityKey, ApiKey)}";
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Failed to load conditions for city key: {cityKey}", null, response.StatusCode);
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var conditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(jsonResponse);

            return conditions.FirstOrDefault();
        }
    }
}
