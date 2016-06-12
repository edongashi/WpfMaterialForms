using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms.ViewModels
{
    internal class StringSchemaViewModel : BaseSchemaViewModel
    {
        private string value = "Value Text";
        private bool isMultiLine;

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
