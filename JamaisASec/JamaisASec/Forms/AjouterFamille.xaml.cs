using System.Windows;


namespace JamaisASec.Forms
{
    /// <summary>
    /// Logique d'interaction pour AjouterFamille.xaml
    /// </summary>
    public partial class AjouterFamille : Window
    {
        private List<Famille> Famille { get; set; }
        public AjouterFamille(List<Famille> famille)
        {
            InitializeComponent();
            Famille = famille ?? new List<Famille>();

        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) { return; }

            string nom = familleName.Text;

            Famille.Add(new Famille(nom));

            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(familleName.Text))
            {
                familleName.ErrorMessage = "Veuillez entrer un nom.";
                isValid = false;
            }
            else
            {
                familleName.ErrorMessage = string.Empty;
            }

            return isValid;
        }
    }
}
