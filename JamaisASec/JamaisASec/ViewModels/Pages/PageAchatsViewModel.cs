using System.Windows.Controls;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;
using JamaisASec.ViewModels.Contents;
using JamaisASec.Views.Contents;

namespace JamaisASec.ViewModels.Pages
{
    class PageAchatsViewModel : BaseViewModel
    {
        private AchatsGrid? _gridCache;
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
        public PageAchatsViewModel()
        {
            NavigateCommand = new RelayCommand<object>(param =>
            {
                switch(param)
                {
                    case Commande achat:
                        Navigate("AchatView", achat);
                        break;
                    case (Commande achat, bool isEditMode):
                        Navigate(isEditMode? "AchatEditView" : "AchatView", achat);
                        break;
                    default:
                        Navigate("AchatsGrid");
                        break;
                }
            });
            Navigate("AchatsGrid");
        }

        private void Navigate(string tab, Commande? commande = null)
        {
            switch (tab)
            {
                case "AchatsGrid":
                    if (_gridCache == null)
                    {
                        _gridCache = new AchatsGrid
                        {
                            DataContext = new AchatsGridViewModel(NavigateCommand)
                        };
                    }
                    CurrentContent = _gridCache;
                    break;
                case "AchatView":
                    if (commande != null)
                    {
                        var commandeView = new AchatView
                        {
                            DataContext = new AchatViewModel(commande, NavigateCommand)
                        };
                        CurrentContent = commandeView;
                    }
                    break;
                case "AchatEditView":
                    if (commande != null)
                    {
                        var commandeView = new AchatEditView
                        {
                            DataContext = new AchatViewModel(commande, NavigateCommand, true)
                        };
                        CurrentContent = commandeView;
                    }
                    break;
            }
        }
    }
}
