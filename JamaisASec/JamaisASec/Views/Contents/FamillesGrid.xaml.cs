using System.Windows.Controls;
using JamaisASec.Models;
using JamaisASec.ViewModels.Contents;

namespace JamaisASec.Views.Contents
{
    /// <summary>
    /// Logique d'interaction pour FamillesGrid.xaml
    /// </summary>
    public partial class FamillesGrid : UserControl
    {
        public FamillesGrid()
        {
            InitializeComponent();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Row.DataContext is Famille famille)
            {
                if (DataContext is FamillesGridViewModel vm)
                {
                    vm.EditCommand.Execute(famille);
                }
            }
        }
    }
}
