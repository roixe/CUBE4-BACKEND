using System.Windows;


namespace JamaisASec.Forms
{
    /// <summary>
    /// Logique d'interaction pour AjouterFamille.xaml
    /// </summary>
    public partial class FamilleForm : Window
    {
        private List<Famille> Famille { get; set; }
        public bool IsEditMode { get; private set; }
        private Famille? FamilleEnCours { get; set; }
        public FamilleForm(List<Famille> famille, Famille? familleAModifier = null)
        {
            InitializeComponent();
            Famille = famille ?? new List<Famille>();

            IsEditMode = familleAModifier != null;
            FamilleEnCours = familleAModifier;
            this.Title = IsEditMode ? "Modifier une famille" : "Ajouter une famille";

            if (FamilleEnCours != null)
            {
                familleName.Text = FamilleEnCours.Nom;
            }

        }

        private void FormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) { return; }

            string nom = familleName.Text;

            if (FamilleEnCours != null)
            {
                FamilleEnCours.Nom = nom;
            }
            else
            {
                Famille.Add(new Famille(nom));
            }
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
