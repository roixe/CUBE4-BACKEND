using System.Collections.ObjectModel;
using JamaisASec.Models;
using JamaisASec.Services;
using System.Windows.Input;

namespace JamaisASec.ViewModels.Contents
{
    class AchatsGridViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Commande> _allAchats;
        public ObservableCollection<Commande> Achats { get; set; }
        
        private string? _searchText;
        public string SearchText
        {
            get => _searchText ?? String.Empty;
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
                if (_isHeaderCheckBoxChecked != value)
                {
                    _isHeaderCheckBoxChecked = value;
                    OnPropertyChanged(nameof(IsHeaderCheckBoxChecked));
                    foreach (var commande in Achats)
                    {
                        commande.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }
        public ICommand LoadDataCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand RowDoubleClickCommand { get; }
        public AchatsGridViewModel(ICommand navigateCommand)
        {
            _allAchats = new ObservableCollection<Commande>();
            Achats = new ObservableCollection<Commande>();

            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);

            _dataService.CommandesUpdated += OnAchatsUpdated;

            NavigateCommand = navigateCommand;
            
            RowDoubleClickCommand = new RelayCommand<Commande>(achat =>
            {
                if (achat != null)
                {
                    NavigateCommand?.Execute(achat);
                }
            });
        }

        private void OnAchatsUpdated(object? sender, EventArgs e)
        {
            _ = LoadData();
        }

        private async Task LoadData()
        {
            var (_, achats) = await _dataService.GetCommandesAndAchatsAsync();
            _allAchats.Clear();
            foreach (var achat in achats)
            {
                _allAchats.Add(achat);
            }
            Filter();
        }

        private void Filter()
        {
            var filtered = _allAchats
                .Where(m => (m.reference?.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (m.fournisseur?.nom?.Contains(SearchText ?? string.Empty, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();
            Achats.Clear();
            foreach (var achat in filtered)
            {
                Achats.Add(achat);
            }
        }
    }
}
