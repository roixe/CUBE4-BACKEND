﻿using System.Collections.ObjectModel;
using JamaisASec.Models;
using System.Windows.Input;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Pages
{

    class PageClientsViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Client> _allClients;
        public ObservableCollection<Client> Clients { get; set; }
        public ICommand LoadDataCommand { get; }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value, nameof(SearchText)))
                {
                    Filter();
                }
            }
        }
        private bool _isHeaderCheckBoxChecked;
        public bool IsHeaderCheckBoxChecked
        {
            get => _isHeaderCheckBoxChecked;
            set
            {
                if (SetProperty(ref _isHeaderCheckBoxChecked, value, nameof(IsHeaderCheckBoxChecked)))
                {
                    foreach (var client in Clients)
                    {
                        client.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }

        public PageClientsViewModel() 
        {
            _allClients = new ObservableCollection<Client>();
            Clients = new ObservableCollection<Client>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var clients = await _dataService.GetClientsAsync();
            _allClients.Clear();
            foreach (var client in clients)
            {
                _allClients.Add(client);
            }
            Filter();
        }
        private void Filter()
        {
            var filtered = _allClients
                .Where(m => m.nom.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase)).ToList();
            Clients.Clear();
            foreach (var client in filtered)
            {
                Clients.Add(client);
            }
        }
    }
}
