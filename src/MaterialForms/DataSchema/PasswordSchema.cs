using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class PasswordSchema : SchemaBase
    {
        private string value;

        public string Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new PasswordTextControl { DataContext = this };
        }
    }
}
