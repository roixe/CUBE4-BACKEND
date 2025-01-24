using System.Windows;
using System.Windows.Controls;

namespace JamaisASec.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour CustomTextBox.xaml
    /// </summary>
    public partial class CustomTextBox : UserControl
    {
        // Dependency Properties
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CustomTextBox), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(CustomTextBox), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(CustomTextBox), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(CustomTextBox), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ErrorMessageVisibilityProperty =
            DependencyProperty.Register("ErrorMessageVisibility", typeof(Visibility), typeof(CustomTextBox), new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty TextWrappingProperty =
            DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(CustomTextBox), new PropertyMetadata(TextWrapping.NoWrap));

        public static readonly DependencyProperty DynamicHeightProperty =
            DependencyProperty.Register("DynamicHeight", typeof(double), typeof(CustomTextBox), new PropertyMetadata(30.0));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        public string Hint
        {
            get => (string)GetValue(HintProperty);
            set => SetValue(HintProperty, value);
        }

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public string ErrorMessageVisibility
        {
            get { return (string)GetValue(ErrorMessageVisibilityProperty); }
            set { SetValue(ErrorMessageVisibilityProperty, value); }
        }

        public TextWrapping TextWrapping
        {
            get => (TextWrapping)GetValue(TextWrappingProperty);
            set => SetValue(TextWrappingProperty, value);
        }

        public double DynamicHeight
        {
            get => (double)GetValue(DynamicHeightProperty);
            set => SetValue(DynamicHeightProperty, value);
        }

        public CustomTextBox()
        {
            InitializeComponent();
        }
    }
}