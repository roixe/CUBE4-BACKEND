using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
