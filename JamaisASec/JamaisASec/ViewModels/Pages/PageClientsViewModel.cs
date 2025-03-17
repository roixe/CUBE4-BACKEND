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
        private ClientsGrid? _gridCache;
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
                switch (param)
                {
                    case (Client client, bool isEditMode):
                        Navigate(isEditMode ? "ClientEditView" : "ClientView", client);
                        break;
                    default:
                        Navigate("ClientsGrid");
                        break;
                }
            });
            Navigate("ClientsGrid");
        }

        private void Navigate(string tab, Client? client = null)
        {
            switch (tab)
            {
                case "ClientsGrid":
                    if (_gridCache == null)
                    {
                        _gridCache = new ClientsGrid();
                    }
                    CurrentContent = _gridCache;
                    break;
                case "ClientView":
                    if(client != null)
                    {
                        var clientView = new ClientView
                        {
                            DataContext = new ClientViewModel(client, NavigateCommand)
                        };
                        CurrentContent = clientView;
                    }
                    break;
                case "ClientEditView":
                    if (client != null)
                    {
                        var clientEditView = new ClientEditView
                        {
                            DataContext = new ClientViewModel(client, NavigateCommand, isEditMode: true)
                        };
                        CurrentContent = clientEditView;
                    }
                    break;
            }
        }

    }
}
