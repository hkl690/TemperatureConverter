using System;
using System.ComponentModel;
using System.Windows.Input;

namespace TemperatureConverter.ViewModels
{
    public class TemperatureViewModel : INotifyPropertyChanged
    {
        private double celsius;
        private double fahrenheit;
        private double kelvin;
        private bool isUpdating;
        private string modifiedProperty;

        public TemperatureViewModel()
        {
            ConvertCommand = new RelayCommand(ConvertTemperatures);
            ClearCommand = new RelayCommand(ClearFields);
        }

        public double Celsius
        {
            get { return celsius; }
            set
            {
                if (celsius != value && !isUpdating)
                {
                    isUpdating = true;
                    celsius = value;
                    OnPropertyChanged(nameof(Celsius));
                    UpdateFromCelsius();
                    isUpdating = false;
                    modifiedProperty = nameof(Celsius);
                }
            }
        }

        public double Fahrenheit
        {
            get { return fahrenheit; }
            set
            {
                if (fahrenheit != value && !isUpdating)
                {
                    isUpdating = true;
                    fahrenheit = value;
                    OnPropertyChanged(nameof(Fahrenheit));
                    UpdateFromFahrenheit();
                    isUpdating = false;
                    modifiedProperty = nameof(Fahrenheit);
                }
            }
        }

        public double Kelvin
        {
            get { return kelvin; }
            set
            {
                if (kelvin != value && !isUpdating)
                {
                    isUpdating = true;
                    kelvin = value;
                    OnPropertyChanged(nameof(Kelvin));
                    UpdateFromKelvin();
                    isUpdating = false;
                    modifiedProperty = nameof(Kelvin);
                }
            }
        }

        public ICommand ConvertCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        private void ConvertTemperatures(object obj)
        {
            switch (modifiedProperty)
            {
                case nameof(Celsius):
                    UpdateFromCelsius();
                    break;
                case nameof(Fahrenheit): 
                    UpdateFromFahrenheit(); 
                    break;
                case nameof(Kelvin):
                    UpdateFromKelvin();
                    break;
            }
            modifiedProperty = null;   
        }

        private void ClearFields(object obj)
        {
            Celsius = 0;
            Fahrenheit = 0;
            Kelvin = 0;
            modifiedProperty = null;
        }

        private void UpdateFromCelsius()
        {
            Fahrenheit = Math.Round((Celsius * 9.0 / 5.0) + 32, 2);
            Kelvin = Celsius + 273.15;
        }

        private void UpdateFromFahrenheit()
        {
            Celsius = Math.Round((Fahrenheit - 32) * 5.0 / 9.0, 2);
            Kelvin = Celsius + 273.15;
        }

        private void UpdateFromKelvin()
        {
            Celsius = Kelvin - 273.15;
            Fahrenheit = Math.Round((Celsius * 9.0 / 5.0) + 32, 2);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;

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
