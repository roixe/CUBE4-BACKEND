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

namespace JamaisASec.Forms
{
    /// <summary>
    /// Logique d'interaction pour MaisonForm.xaml
    /// </summary>
    public partial class MaisonForm : Window
    {
        private List<Maison> Maison { get; set; }
        public bool IsEditMode { get; private set; }
        private Maison? MaisonEnCours { get; set; }
        public MaisonForm(List<Maison> maison, Maison? maisonAModifier = null)
        {
            InitializeComponent();
            Maison = maison ?? new List<Maison>();

            IsEditMode = maisonAModifier != null;
            MaisonEnCours = maisonAModifier;
            this.Title = IsEditMode ? "Modifier une maison" : "Ajouter une maison";

            if (MaisonEnCours != null)
            {
                maisonName.Text = MaisonEnCours.nom;
            }

        }

        private void FormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) { return; }

            string nom = maisonName.Text;

            if (MaisonEnCours != null)
            {
                MaisonEnCours.nom = nom;
            }
            else
            {
                Maison.Add(new Maison(nom));
            }
            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(maisonName.Text))
            {
                maisonName.ErrorMessage = "Veuillez entrer un nom.";
                isValid = false;
            }
            else
            {
                maisonName.ErrorMessage = string.Empty;
            }

            return isValid;
        }
    }
}
