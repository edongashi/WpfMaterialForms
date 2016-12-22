using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialForms.Controls
{
    /// <summary>
    /// Interaction logic for SingleLineTextControl.xaml
    /// </summary>
    public partial class SingleLineTextControl : UserControl
    {
        public SingleLineTextControl()
        {
            InitializeComponent();
        }

        public SingleLineTextControl(IValueConverter converter)
        {
            InitializeComponent();
            var binding = new Binding("Value") { Converter = converter };
            ValueHolderControl.SetBinding(TextBox.TextProperty, binding);
        }

        private void SingleLineTextControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            ValueHolderControl.SelectAll();
        }
    }
}
