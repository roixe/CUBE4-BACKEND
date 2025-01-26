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
                if (param is Commande commande)
                {
                    Navigate("CommandeView", commande);
                }
                else if (param is string tab)
                {
                    Navigate(tab);
                }
            });
            Navigate("CommandesGrid");
        }

        private void Navigate(string tab, Commande? commande = null)
        {
            switch (tab)
            {
                case "CommandesGrid":
                    CurrentContent = new CommandesGrid();
                    break;
                case "CommandeView":
                    if (commande != null)
                    {
                        var commandeViewModel = new CommandeViewModel(commande);
                        var commandeView = new CommandeView
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
