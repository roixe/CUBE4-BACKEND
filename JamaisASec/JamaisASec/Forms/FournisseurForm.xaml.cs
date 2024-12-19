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
    /// Logique d'interaction pour FournisseurForm.xaml
    /// </summary>
    public partial class FournisseurForm : Window
    {
        private List<Fournisseur> Fournisseurs { get; set; }
        public bool IsEditMode { get; private set; }
        private Fournisseur? FournisseurEnCours { get; set; }
        public FournisseurForm(List<Fournisseur> fournisseurs, Fournisseur? fournisseurAModifier = null)
        {
            InitializeComponent();
            Fournisseurs = fournisseurs ?? new List<Fournisseur>();

            IsEditMode = fournisseurAModifier != null;
            FournisseurEnCours = fournisseurAModifier;
            this.Title = IsEditMode ? "Modifier un fournisseur" : "Ajouter un fournisseur";

            if (FournisseurEnCours != null)
            {
                fournisseurName.Text = FournisseurEnCours.nom;
                fournisseurAddress.Text = FournisseurEnCours.adresse;
                fournisseurMail.Text = FournisseurEnCours.mail;
                fournisseurPhoneNumber.Text = FournisseurEnCours.telephone;
                fournisseurSIRET.Text = FournisseurEnCours.siret;
            }
        }

        private void FormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) { return; }

            string nom = fournisseurName.Text;
            string adresse = fournisseurAddress.Text;
            string mail = fournisseurMail.Text;
            string telephone = fournisseurPhoneNumber.Text;
            string siret = fournisseurSIRET.Text;

            if (FournisseurEnCours != null)
            {
                FournisseurEnCours.nom = nom;
                FournisseurEnCours.adresse = adresse;
                FournisseurEnCours.mail = mail;
                FournisseurEnCours.telephone = telephone;
                FournisseurEnCours.siret = siret;
            }
            /*else
            {
                Fournisseurs.Add(new Fournisseur(nom, adresse, mail, telephone, siret));
            }*/
            this.Close();
        }

        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(fournisseurName.Text))
            {
                fournisseurName.ErrorMessage = "Veuillez entrer un nom.";
                isValid = false;
            }
            else
            {
                fournisseurName.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(fournisseurAddress.Text))
            {
                fournisseurAddress.ErrorMessage = "Veuillez entrer une adresse.";
                isValid = false;
            }
            else
            {
                fournisseurAddress.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(fournisseurMail.Text))
            {
                fournisseurMail.ErrorMessage = "Veuillez entrer une adresse e-mail.";
                isValid = false;
            }
            else
            {
                fournisseurMail.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(fournisseurPhoneNumber.Text))
            {
                fournisseurPhoneNumber.ErrorMessage = "Veuillez entrer un numéro de téléphone.";
                isValid = false;
            }
            else
            {
                fournisseurPhoneNumber.ErrorMessage = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(fournisseurSIRET.Text))
            {
                fournisseurSIRET.ErrorMessage = "Veuillez entrer un numéro de SIRET.";
                isValid = false;
            }
            else
            {
                fournisseurSIRET.ErrorMessage = string.Empty;
            }


            return isValid;
        }
    }
}
