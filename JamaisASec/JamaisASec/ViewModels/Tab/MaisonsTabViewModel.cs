using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JamaisASec.Models;
using JamaisASec.Services;

namespace JamaisASec.ViewModels.Tab
{
    class MaisonsTabViewModel : BaseViewModel
    {
        public ObservableCollection<Maison> Maisons { get; set; }
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
                    foreach (var maison in Maisons)
                    {
                        maison.IsSelected = _isHeaderCheckBoxChecked;
                    }
                }
            }
        }


        public MaisonsTabViewModel()
        {
            Maisons = new ObservableCollection<Maison>();
            LoadDataCommand = new RelayCommandAsync(async () => await LoadData());
            LoadDataCommand.Execute(null);
        }

        private async Task LoadData()
        {
            var maisons = await _dataService.GetMaisonsAsync();
            Maisons.Clear();
            foreach (var maison in maisons)
            {
                Maisons.Add(maison);
            }
        }
    }
}
