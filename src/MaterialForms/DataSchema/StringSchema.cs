using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class StringSchema : SchemaBase
    {
        private string value;
        private bool isMultiLine;
        private bool isReadOnly;

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

        public bool IsMultiLine
        {
            get { return isMultiLine; }
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
            get { return isReadOnly; }
            set
            {
                if (value == isReadOnly) return;
                isReadOnly = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            if (IsMultiLine)
            {
                return new MultiLineTextControl
                {
                    DataContext = this
                };
            }
            else
            {
                return new SingleLineTextControl()
                {
                    DataContext = this
                };
            }
        }
    }
}
