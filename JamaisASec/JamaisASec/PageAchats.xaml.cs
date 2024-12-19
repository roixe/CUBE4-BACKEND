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

namespace JamaisASec
{
    /// <summary>
    /// Logique d'interaction pour PageAchats.xaml
    /// </summary>
    public partial class PageAchats : Page
    {
        private List<Commande> Achats;
        public PageAchats(List<Commande> achats)
        {
            InitializeComponent();
            Achats = achats;
            AchatsGrid.ItemsSource = Achats;
        }
        private void HeaderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var achat in Achats)
                {
                    achat.IsSelected = true;
                }
                AchatsGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }

        private void HeaderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox headerCheckBox)
            {
                foreach (var achat in Achats)
                {
                    achat.IsSelected = false;
                }
                AchatsGrid.Items.Refresh(); // Rafraîchir la grille pour refléter les modifications
            }
        }
    }
}
