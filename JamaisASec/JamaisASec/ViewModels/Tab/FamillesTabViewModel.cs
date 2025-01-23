using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Tab
{
    class FamillesTabViewModel : BaseViewModel
    {
        public ObservableCollection<Famille> Familles { get; }
        public ICommand LoadDataCommand { get; }
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
                    foreach (var famille in Familles)
                    {
                        famille.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }


        public FamillesTabViewModel()
        {
            Familles = new ObservableCollection<Famille>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var familles = await _dataService.GetFamillesAsync();
            Familles.Clear();
            foreach (var famille in familles)
            {
                Familles.Add(famille);
            }
        }
    }
}
