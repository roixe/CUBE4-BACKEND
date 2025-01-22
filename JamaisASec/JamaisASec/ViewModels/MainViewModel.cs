using JamaisASec.Views;
using JamaisASec.Services;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace JamaisASec.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentPage;

        private readonly Dictionary<string, Page> _pagesCache = new();

        // Propriétés de navigation
        public object CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }

        // Commande de navigation
        public ICommand NavigateCommand { get; }

        // Propriétés de chaque page
        private bool _isDashboardActive;
        public bool IsDashboardActive
        {
            get => _isDashboardActive;
            set
            {
                if (_isDashboardActive != value)
                {
                    _isDashboardActive = value;
                    OnPropertyChanged(nameof(IsDashboardActive));
                }
            }
        }

        private bool _isArticlesActive;
        public bool IsArticlesActive
        {
            get => _isArticlesActive;
            set
            {
                if (_isArticlesActive != value)
                {
                    _isArticlesActive = value;
                    OnPropertyChanged(nameof(IsArticlesActive));
                }
            }
        }

        private bool _isClientsActive;
        public bool IsClientsActive
        {
            get => _isClientsActive;
            set
            {
                if (_isClientsActive != value)
                {
                    _isClientsActive = value;
                    OnPropertyChanged(nameof(IsClientsActive));
                }
            }
        }

        private bool _isCommandesActive;
        public bool IsCommandesActive
        {
            get => _isCommandesActive;
            set
            {
                if (_isCommandesActive != value)
                {
                    _isCommandesActive = value;
                    OnPropertyChanged(nameof(IsCommandesActive));
                }
            }
        }

        private bool _isFournisseursActive;
        public bool IsFournisseursActive
        {
            get => _isFournisseursActive;
            set
            {
                if (_isFournisseursActive != value)
                {
                    _isFournisseursActive = value;
                    OnPropertyChanged(nameof(IsFournisseursActive));
                }
            }
        }

        private bool _isAchatsActive;
        public bool IsAchatsActive
        {
            get => _isAchatsActive;
            set
            {
                if (_isAchatsActive != value)
                {
                    _isAchatsActive = value;
                    OnPropertyChanged(nameof(IsAchatsActive));
                }
            }
        }

        private bool _isStocksActive;
        public bool IsStocksActive
        {
            get => _isStocksActive;
            set
            {
                if (_isStocksActive != value)
                {
                    _isStocksActive = value;
                    OnPropertyChanged(nameof(IsStocksActive));
                }
            }
        }

        public MainViewModel()
        {
            NavigateCommand = new RelayCommand<string>(Navigate);

            Navigate("Dashboard");
        }

        private void Navigate(string pageTag)
        {
            IsDashboardActive = false;
            IsArticlesActive = false;
            IsClientsActive = false;
            IsCommandesActive = false;
            IsFournisseursActive = false;
            IsAchatsActive = false;
            IsStocksActive = false;
            // Vérifier si la page est déjà dans le cache
            if (!_pagesCache.ContainsKey(pageTag))
            {
                // Si la page n'est pas dans le cache, la créer et l'ajouter
                switch (pageTag)
                {
                    case "Dashboard":
                        _pagesCache[pageTag] = new PageAccueil();
                        IsDashboardActive = true;
                        break;
                    case "Articles":
                        IsArticlesActive = true;
                        break;
                    case "Clients":
                        IsClientsActive = true;
                        break;
                    case "Commandes":
                        _pagesCache[pageTag] = new PageCommandes();
                        IsCommandesActive = true;
                        break;
                    case "Fournisseurs":
                        IsFournisseursActive = true;
                        break;
                    case "Achats":
                        _pagesCache[pageTag] = new PageAchats();
                        IsAchatsActive = true;
                        break;
                    case "Stocks":
                        IsStocksActive = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // La page existe déjà dans le cache, définir son état
                switch (pageTag)
                {
                    case "Dashboard": IsDashboardActive = true; break;
                    case "Articles": IsArticlesActive = true; break;
                    case "Clients": IsClientsActive = true; break;
                    case "Commandes": IsCommandesActive = true; break;
                    case "Fournisseurs": IsFournisseursActive = true; break;
                    case "Achats": IsAchatsActive = true; break;
                    case "Stocks": IsStocksActive = true; break;
                }
            }

            // Naviguer vers la page existante ou nouvellement créée
            CurrentPage = _pagesCache[pageTag];
        }
    }
}
