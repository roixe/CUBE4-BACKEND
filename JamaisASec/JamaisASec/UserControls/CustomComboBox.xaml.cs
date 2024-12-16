using System.Windows;
using System.Windows.Controls;

namespace JamaisASec.UserControls
{
    /// <summary>
    /// Logique d'interaction pour CustomComboBox.xaml
    /// </summary>
    public partial class CustomComboBox : UserControl
    {
        public CustomComboBox()
        {
            InitializeComponent();
        }

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(CustomComboBox), new PropertyMetadata(""));
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(CustomComboBox), new PropertyMetadata(""));

        // ErrorMessage property
        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(CustomComboBox), 
                new PropertyMetadata(string.Empty, OnErrorMessageChanged));

        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (CustomComboBox)d;
            control.ErrorMessageVisibility = string.IsNullOrEmpty(control.ErrorMessage) ? Visibility.Collapsed : Visibility.Visible;
        }
        // ErrorMessageVisibility property
        public Visibility ErrorMessageVisibility
        {
            get { return (Visibility)GetValue(ErrorMessageVisibilityProperty); }
            set { SetValue(ErrorMessageVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageVisibilityProperty =
            DependencyProperty.Register("ErrorMessageVisibility", typeof(Visibility), typeof(CustomComboBox), new PropertyMetadata(Visibility.Collapsed));

        // ItemsSource property
        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(CustomComboBox), new PropertyMetadata(null));

        // SelectedItem property
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(CustomComboBox), new PropertyMetadata(null));

    }
}

