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

        public string Filter { get; set; }

        public ValidationCallback<string> Validation { get; set; }

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

        protected override bool OnValidation()
        {
            var callback = Validation;
            if (callback == null)
            {
                return true;
            }

            Error = callback(value);
            return HasNoError;
        }
    }
}
