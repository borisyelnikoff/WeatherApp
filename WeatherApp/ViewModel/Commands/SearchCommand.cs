using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    public class SearchCommand(WeatherViewModel weatherViewModel) : ICommand
    {
        public WeatherViewModel WeatherViewModel { get; set; } = weatherViewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => !string.IsNullOrWhiteSpace(parameter as string);

        public async void Execute(object parameter)
        {
            await WeatherViewModel.QueryForCities();
        }
    }
}
