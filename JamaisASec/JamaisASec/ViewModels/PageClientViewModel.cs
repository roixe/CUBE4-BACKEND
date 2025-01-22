﻿using System.Collections.ObjectModel;
using JamaisASec.Models;
using System.Windows.Input;
using JamaisASec.Services;

namespace JamaisASec.ViewModels
{

    class PageClientViewModel : BaseViewModel
    {
        public ObservableCollection<Client> Clients { get; set; }
        public ICommand LoadDataCommand { get; }

        public PageClientViewModel() { 
            Clients = new ObservableCollection<Client>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());

            LoadDataCommand.Execute(null);
        }

        public async Task LoadData()
        {
            var clients = await _apiService.GetClientsAsync();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }
    }
}
