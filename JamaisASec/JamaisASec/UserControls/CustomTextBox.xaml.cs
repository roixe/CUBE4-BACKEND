using System.Windows;
using System.Windows.Controls;

namespace JamaisASec.UserControls
{
    /// <summary>
    /// Logique d'interaction pour Input_Form.xaml
    /// </summary>
    public partial class CustomTextBox : UserControl
    {
        public CustomTextBox()
        {
            InitializeComponent();
        }

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(CustomTextBox), new PropertyMetadata(""));
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(CustomTextBox), new PropertyMetadata(""));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CustomTextBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        // Propriété pour le message d'erreur
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(CustomTextBox),
                new PropertyMetadata(string.Empty, OnErrorMessageChanged));

        // Méthode appelée quand ErrorMessage change
        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (CustomTextBox)d;
            control.ErrorMessageVisibility = string.IsNullOrEmpty(control.ErrorMessage) ? Visibility.Collapsed : Visibility.Visible;
        }

        // Propriété pour la visibilité de l'erreur
        public static readonly DependencyProperty ErrorMessageVisibilityProperty =
            DependencyProperty.Register("ErrorMessageVisibility", typeof(Visibility), typeof(CustomTextBox),
                new PropertyMetadata(Visibility.Collapsed));

        public Visibility ErrorMessageVisibility
        {
            get => (Visibility)GetValue(ErrorMessageVisibilityProperty);
            set => SetValue(ErrorMessageVisibilityProperty, value);
        }

    }

}
