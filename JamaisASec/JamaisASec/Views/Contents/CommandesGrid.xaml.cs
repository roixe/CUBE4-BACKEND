using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JamaisASec.Models;
using JamaisASec.ViewModels.Contents;

namespace JamaisASec.Views.Contents
{
    /// <summary>
    /// Logique d'interaction pour CommandesGrid.xaml
    /// </summary>
    public partial class CommandesGrid : UserControl
    {
        public CommandesGrid()
        {
            InitializeComponent();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row && row.DataContext is Commande selectedCommande)
            {
                var viewModel = DataContext as CommandesGridViewModel;
                viewModel?.RowDoubleClickCommand.Execute(selectedCommande);
            }
        }
    }
}
