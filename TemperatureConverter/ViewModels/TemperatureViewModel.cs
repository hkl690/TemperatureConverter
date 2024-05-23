using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TemperatureConverter.ViewModels
{
    public class TemperatureViewModel : INotifyPropertyChanged
    {
        private double celsius;
        private double fahrenheit;
        private double kelvin;

        public TemperatureViewModel()
        {
            ConvertCommand = new RelayCommand(ConvertTemperatures);
            ClearCommand = new RelayCommand(ClearFields);
        }

        public double Celsius
        {
            get { return celsius; }
            set { celsius = value; OnPropertyChanged("Celsius"); }
        }

        public double Fahrenheit
        {
            get { return fahrenheit; }
            set { fahrenheit = value; OnPropertyChanged("Fahrenheit"); }
        }

        public double Kelvin
        {
            get { return kelvin; }
            set { kelvin = value; OnPropertyChanged("Kelvin"); }
        }

        public ICommand ConvertCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        private void ConvertTemperatures(object obj)
        {
            Fahrenheit = (Celsius * 9.0 / 5.0) + 32;
            Kelvin = Celsius + 273.15;
        }

        private void ClearFields(object obj)
        {
            Celsius = 0;
            Fahrenheit = 0;
            Kelvin = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private Action<object> execute;

        public RelayCommand(Action<object> executeAction)
        {
            execute = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
