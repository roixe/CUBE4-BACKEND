using System.Collections.ObjectModel;
using JamaisASec.Models;
using System.Windows.Input;
using JamaisASec.Services;
using System.Windows.Controls;
using JamaisASec.Views.Contents;
using JamaisASec.ViewModels.Contents;

namespace JamaisASec.ViewModels.Pages
{

    class PageClientsViewModel : BaseViewModel
    {
        private UserControl _currentContent = new();
        public UserControl CurrentContent
        {
            get => _currentContent;
            set
            {
                if (_currentContent != value)
                {
                    _currentContent = value;
                    OnPropertyChanged(nameof(CurrentContent));
                }
            }
        }
        public ICommand NavigateCommand { get; }

        public PageClientsViewModel()
        {
            NavigateCommand = new RelayCommand<object>(param =>
            {
                if (param is Client client)
                {
                    Navigate("ClientView", client);
                }
                else if (param is string tab)
                {
                    Navigate(tab);
                }
            });
            Navigate("ClientsGrid");
        }

        private void Navigate(string tab, Client? client = null)
        {
            switch (tab)
            {
                case "ClientsGrid":
                    CurrentContent = new ClientsGrid();
                    break;
                case "ClientView":
                    if(client != null)
                    {
                        var clientViewModel = new ClientViewModel(client);
                        var clientView = new ClientView
                        {
                            DataContext = clientViewModel
                        };
                        CurrentContent = clientView;
                    }
                    break;
            }
        }

    }
}
