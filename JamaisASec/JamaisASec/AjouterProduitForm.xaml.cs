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
using System.Windows.Shapes;

namespace JamaisASec
{
    /// <summary>
    /// Interaction logic for AjouterProduitForm.xaml
    /// </summary>
    public partial class AjouterProduitForm : Window
    {
        public AjouterProduitForm()
        {
            InitializeComponent();
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les valeurs des champs de texte
            string nom = NomTextBox.Text;
            string description = DescriptionTextBox.Text;
            string stock = StockTextBox.Text;
            string familles = FamillesTextBox.Text;
            string prix = PrixTextBox.Text;

            // Ajouter le produit (vous pouvez ajouter votre logique ici)
            MessageBox.Show("Produit ajouté avec succès!");

            // Fermer la fenêtre
            this.Close();
        }
    }
}
