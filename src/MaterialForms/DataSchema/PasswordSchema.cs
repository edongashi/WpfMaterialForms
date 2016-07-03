using System.Security;
using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class PasswordSchema : SchemaBase
    {
        private SecureString value;

        public SecureString Value
        {
            get { return value; }
            set
            {
                if (Equals(value, this.value)) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new PasswordTextControl { DataContext = this };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => PasswordHelpers.ConvertToUnsecureString(Value);
    }
}
