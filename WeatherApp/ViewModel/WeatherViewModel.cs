using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string _query;
        private CurrentConditions _currentConditions;
        private City _selectedCity;

        public WeatherViewModel()
        {
            SelectedCity = new City()
            {
                LocalizedName = "New York"
            };
            CurrentConditions = new CurrentConditions()
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
                OnPropertyChanged(nameof(SelectedCity));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
