using System.ComponentModel;
using JamaisASec.Services;

namespace JamaisASec.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected readonly IApiService _apiService;
        protected readonly CommandeService _commandeService;
        protected readonly DataService _dataService;

        public BaseViewModel()
        {
            IApiService apiService = ApiService.Instance;
            _apiService = apiService;
            _commandeService = new CommandeService(apiService);
            DataService.Initialize(apiService);
            _dataService = DataService.Instance;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, string propertyName)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        // Propriété SearchText
        private string? _searchText;
        public string SearchText
        {
            get => _searchText ?? string.Empty;
            set
            {
                if (SetProperty(ref _searchText, value, nameof(SearchText)))
                {
                    OnSearchTextChanged?.Invoke(value);
                }
            }
        }

        // Callback pour SearchText (à définir dans les ViewModels)
        public Action<string>? OnSearchTextChanged { get; set; }

        // Propriété IsHeaderCheckBoxChecked
        private bool _isHeaderCheckBoxChecked;
        public bool IsHeaderCheckBoxChecked
        {
            get => _isHeaderCheckBoxChecked;
            set
            {
                if (SetProperty(ref _isHeaderCheckBoxChecked, value, nameof(IsHeaderCheckBoxChecked)))
                {
                    OnHeaderCheckBoxChanged?.Invoke(value);
                }
            }
        }

        // Callback pour IsHeaderCheckBoxChecked (à définir dans les ViewModels)
        public Action<bool>? OnHeaderCheckBoxChanged { get; set; }
    }
}
