using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JamaisASec.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false; // Pas de changement, pas de notification
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Propriété IsSelected commune à tous les modèles
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
