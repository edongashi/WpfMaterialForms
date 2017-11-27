using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialForms.Controls
{
    /// <summary>
    ///     Interaction logic for SingleLineTextControl.xaml
    /// </summary>
    public partial class PasswordTextControl : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password",
                typeof(SecureString),
                typeof(PasswordBox),
                new PropertyMetadata(default(SecureString)));

        public PasswordTextControl()
        {
            InitializeComponent();
            ValueHolderControl.PasswordChanged += (sender, args) =>
            {
                Password = ((PasswordBox) sender).SecurePassword;
            };

            var binding = new Binding("Value")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            ValueHolderControl.SetBinding(PasswordProperty, binding);
        }

        public SecureString Password
        {
            get => (SecureString) ValueHolderControl.GetValue(PasswordProperty);
            set => ValueHolderControl.SetValue(PasswordProperty, value);
        }
    }
}