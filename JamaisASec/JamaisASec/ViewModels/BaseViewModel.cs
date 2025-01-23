using System.ComponentModel;
using JamaisASec.Services;

namespace JamaisASec.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected readonly ApiService _apiService;
        protected readonly CommandeService _commandeService;
        protected readonly DataService _dataService;

        public BaseViewModel()
        {
            _apiService = new ApiService();
            _commandeService = new CommandeService(_apiService);
            _dataService = new DataService(_apiService);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
}
