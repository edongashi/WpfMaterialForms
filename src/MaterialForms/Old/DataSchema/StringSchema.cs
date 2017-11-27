using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class StringSchema : SchemaBase
    {
        private bool isMultiLine;
        private bool isReadOnly;
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

        public bool IsMultiLine
        {
            get => isMultiLine;
            set
            {
                if (value == isMultiLine) return;
                isMultiLine = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(View));
            }
        }

        public bool IsReadOnly
        {
            get => isReadOnly;
            set
            {
                if (value == isReadOnly) return;
                isReadOnly = value;
                OnPropertyChanged();
            }
        }

        public override bool HoldsValue => true;

        public ValidationCallback<string> Validation { get; set; }

        public override UserControl CreateView()
        {
            if (IsMultiLine)
                return new MultiLineTextControl
                {
                    DataContext = this
                };

            return new SingleLineTextControl
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

        protected override bool OnValidation()
        {
            var callback = Validation;
            if (callback == null)
                return true;

            Error = callback(value);
            return HasNoError;
        }
    }
}