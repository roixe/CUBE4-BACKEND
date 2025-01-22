using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;
using JamaisASec.Views.UserControls;

namespace JamaisASec.ViewModels
{
    class PageArticlesViewModel : BaseViewModel
    {
        private readonly Dictionary<string, UserControl> _tabCache = new();
        private UserControl _currentContent;
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
        private string _activeTab;
        public string ActiveTab
        {
            get => _activeTab;
            set
            {
                if (_activeTab != value)
                {
                    _activeTab = value;
                    OnPropertyChanged(nameof(ActiveTab));
                }
            }
        }

        public ICommand LoadCommand { get; }
        public ICommand NavigateCommand { get; }

        public PageArticlesViewModel()
        {
            // Commandes pour changer l'onglet actif
            NavigateCommand = new RelayCommand<string>(Navigate);
            Navigate("Articles");
        }


        private void Navigate(string tab)
        {
            if (!_tabCache.ContainsKey(tab))
            {
                switch (tab)
                {
                    case "Articles":
                        _tabCache[tab] = new ArticlesTab();
                        break;
                    case "Familles":
                        _tabCache[tab] = new FamillesTab();
                        break;
                    case "Maisons":
                        _tabCache[tab] = new MaisonsTab();
                        break;
                }
            }
            CurrentContent = _tabCache[tab];
            ActiveTab = tab;

        }
    }
}
