using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string _query;
        private CurrentConditions _currentConditions;
        private City _selectedCity;

        public ObservableCollection<City> Cities { get; set; }

        public WeatherViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = CityExample();
                CurrentConditions = ConditionsExample();
            }

            SearchCommand = new(this);
            Cities = new();
        }

        public SearchCommand SearchCommand { get; set; }

        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        public CurrentConditions CurrentConditions
        {
            get => _currentConditions;
            set
            {
                _currentConditions = value;
                OnPropertyChanged(nameof(CurrentConditions));
            }
        }

        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                if (_selectedCity != null)
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(async () => await GetCurrentConditions());
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public async Task QueryForCities()
        {
            try
            {
                var cities = await AccuWeatherHelper.GetCities(Query);
                Cities.Clear();
                foreach (var city in cities)
                {
                    Cities.Add(city);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load city list for {Query}\nException message: {ex.Message}", 
                    "Data error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetCurrentConditions()
        {
            try
            {
                Query = string.Empty;
                CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
                OnPropertyChanged(nameof(SelectedCity));
                Cities.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load current conditions for {SelectedCity.LocalizedName}\nException message: {ex.Message}", 
                    "Data error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static City CityExample() => new()
        {
            LocalizedName = "New York"
        };

        private static CurrentConditions ConditionsExample() => new()
        {
            WeatherText = "Mostly cloudy",
            Temperature = new Temperature()
            {
                Metric = new Units()
                {
                    Value = 21
                }
            }
        };
    }
}
