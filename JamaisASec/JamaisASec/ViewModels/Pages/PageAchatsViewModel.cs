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
                if (param is Commande commande)
                {
                    Navigate("AchatView", commande);
                }
                else if (param is string tab)
                {
                    Navigate(tab);
                }
            });
            Navigate("AchatsGrid");
        }

        private void Navigate(string tab, Commande? commande = null)
        {
            switch (tab)
            {
                case "AchatsGrid":
                    CurrentContent = new AchatsGrid();
                    break;
                case "AchatView":
                    if (commande != null)
                    {
                        var commandeViewModel = new AchatViewModel(commande);
                        var commandeView = new AchatView
                        {
                            DataContext = commandeViewModel
                        };
                        CurrentContent = commandeView;
                    }
                    break;
            }
        }
    }
}
