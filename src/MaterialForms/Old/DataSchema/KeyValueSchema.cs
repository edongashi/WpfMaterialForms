using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class KeyValueSchema : SchemaBase
    {
        private string value;

        public string Value
        {
            get => value;
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public override bool HoldsValue => true;

        public override UserControl CreateView()
        {
            return new KeyValueControl
            {
                DataContext = this
            };
        }

        public override object GetValue()
        {
            return Value;
        }

        public override void SetValue(object obj)
        {
            Value = obj?.ToString();
        }
    }
}