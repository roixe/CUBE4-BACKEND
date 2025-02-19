using System.Windows.Controls;
using JamaisASec.Models;
using JamaisASec.ViewModels.Contents;

namespace JamaisASec.Views.Contents
{
    /// <summary>
    /// Logique d'interaction pour MaisonsControl.xaml
    /// </summary>
    public partial class MaisonsGrid : UserControl
    {
        public MaisonsGrid()
        {
            InitializeComponent();
            
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Row.DataContext is Maison maison)
            {
                if (DataContext is MaisonsGridViewModel vm)
                {
                    vm.EditCommand.Execute(maison);
                }
            }
        }
    }
}
