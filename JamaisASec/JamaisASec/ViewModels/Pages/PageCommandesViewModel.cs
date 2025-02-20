using System.Collections.ObjectModel;
using JamaisASec.Models;
using System.Windows.Input;
using JamaisASec.Services;
using JamaisASec.ViewModels.Contents;
using JamaisASec.Views.Contents;
using System.Windows.Controls;

namespace JamaisASec.ViewModels.Pages
{

    class PageCommandesViewModel : BaseViewModel
    {
        private CommandesGrid? _gridCache;
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
        public PageCommandesViewModel()
        {
            NavigateCommand = new RelayCommand<object>(param =>
            {
                switch(param)
                {
                    case Commande commande:
                        Navigate("CommandeView", commande);
                        break;
                    case (Commande commande, bool isEditMode):
                        Navigate(isEditMode ? "CommandeEditView" : "CommandeView", commande);
                        break;
                    default:
                        Navigate("CommandesGrid");
                        break;
                }
            });
            Navigate("CommandesGrid");
        }

        private void Navigate(string tab, Commande? commande = null)
        {
            switch (tab)
            {
                case "CommandesGrid":
                    if (_gridCache == null)
                    {
                        _gridCache = new CommandesGrid
                        {
                            DataContext = new CommandesGridViewModel(NavigateCommand)
                        };
                    }
                    CurrentContent = _gridCache;
                    break;
                case "CommandeView":
                    if (commande != null)
                    {
                        var commandeView = new CommandeView
                        {
                            DataContext = new CommandeViewModel(commande, NavigateCommand)
                        };
                        CurrentContent = commandeView;
                    }
                    break;
                case "CommandeEditView":
                    if (commande != null)
                    {
                        var commandeEditView = new CommandeEditView
                        {
                            DataContext = new CommandeViewModel(commande, NavigateCommand, true)
                        };
                        CurrentContent = commandeEditView;
                    }
                    break;
            }
        }
    }
}
