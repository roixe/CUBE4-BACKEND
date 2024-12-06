using System.Windows;
using System.Windows.Controls;

namespace JamaisASec
{
    public partial class AjouterProduitForm : Window
    {
        public Produit ProduitAjoute { get; private set; }

        public AjouterProduitForm()
        {
            InitializeComponent();
        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier les erreurs de validation
            if (Validation.GetHasError(StockTextBox) || Validation.GetHasError(PrixTextBox))
            {
                MessageBox.Show("Veuillez corriger les erreurs dans les champs Stock et Prix.");
                return;
            }

            // Récupérer les valeurs des champs de texte
            string nom = NomTextBox.Text;
            string description = DescriptionTextBox.Text;
            int stock = int.Parse(StockTextBox.Text);
            string familles = FamillesTextBox.Text;
            int prix = int.Parse(PrixTextBox.Text);

            // Créer un nouveau produit
            ProduitAjoute = new Produit
            {
                Nom = nom,
                Description = description,
                Stock = stock,
                Familles = familles,
                Prix = prix
            };

            // Ajouter le produit (vous pouvez ajouter votre logique ici)
            MessageBox.Show("Produit ajouté avec succès!");

            // Fermer la fenêtre
            this.Close();
        }
    }
}
