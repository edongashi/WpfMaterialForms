using System.Security;
using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class PasswordSchema : SchemaBase
    {
        private SecureString password;

        public SecureString Password
        {
            get { return password; }
            set
            {
                if (Equals(value, password)) return;
                password = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new PasswordTextControl { DataContext = this };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => PasswordHelpers.ConvertToUnsecureString(Password);
    }
}
