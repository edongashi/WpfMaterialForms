using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialForms.Controls
{
    /// <summary>
    /// Interaction logic for SingleLineTextControl.xaml
    /// </summary>
    public partial class PasswordTextControl : UserControl
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password",
                typeof(SecureString), typeof(PasswordTextControl),
                new PropertyMetadata(default(SecureString)));

        public SecureString Password
        {
            get { return (SecureString)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public PasswordTextControl()
        {
            InitializeComponent();
            ValueHolderControl.PasswordChanged += (sender, args) =>
            {
                Password = ((PasswordBox)sender).SecurePassword;
            };

            var binding = new Binding("Password")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.LostFocus
            };

            SetBinding(PasswordProperty, binding);
        }
    }
}
