using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class SingleFileSchema : SchemaBase
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
            return new FileLoaderControl()
            {
                DataContext = this
            };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => Value;

        public override void SetValue(object obj)
        {
            Value = obj?.ToString();
        }
    }
}
