using System.Security;
using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class PasswordSchema : SchemaBase
    {
        public SecureString Password { get; set; }

        public override UserControl CreateView()
        {
            return new PasswordTextControl { DataContext = this };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => PasswordHelpers.ConvertToUnsecureString(Password);
    }
}
