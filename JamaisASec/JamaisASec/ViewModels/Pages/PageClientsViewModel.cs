using System.Collections.ObjectModel;
using JamaisASec.Models;
using System.Windows.Input;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{

    class PageClientsViewModel : BaseViewModel
    {
        public ObservableCollection<Client> Clients { get; set; }
        public ICommand LoadDataCommand { get; }
        private bool _isHeaderCheckBoxChecked;
        public bool IsHeaderCheckBoxChecked
        {
            get => _isHeaderCheckBoxChecked;
            set
            {
                if (_isHeaderCheckBoxChecked != value)
                {
                    _isHeaderCheckBoxChecked = value;
                    OnPropertyChanged(nameof(IsHeaderCheckBoxChecked));
                    foreach (var client in Clients)
                    {
                        client.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }


        public PageClientsViewModel() { 
            Clients = new ObservableCollection<Client>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var clients = await _dataService.GetClientsAsync();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }
    }
}
