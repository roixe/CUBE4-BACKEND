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
using JamaisASec.ViewModels.Contents;
using JamaisASec.Models;

namespace JamaisASec.Views.Contents
{
    /// <summary>
    /// Logique d'interaction pour AchatsGrid.xaml
    /// </summary>
    public partial class AchatsGrid : UserControl
    {
        public AchatsGrid()
        {
            InitializeComponent();
        }
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row && row.DataContext is Commande selectedAchat)
            {
                var viewModel = DataContext as AchatsGridViewModel;
                viewModel?.RowDoubleClickCommand.Execute(selectedAchat);
            }
        }
    }
}
