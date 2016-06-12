using System.Windows.Controls;
using System.Collections.Generic;
using SchemaPrompt.Launcher.Controls;

namespace SchemaPrompt.Launcher.ViewModels
{
    internal class SelectionSchemaViewModel : BaseSchemaViewModel
    {
        private object value = "";
        private IEnumerable<object> items = new object[] { "First", "Second", "Third" };

        public object Value
        {
            get { return value; }
            set
            {
                if (Equals(value, this.value)) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<object> Items
        {
            get { return items; }
            set
            {
                if (Equals(value, items)) return;
                items = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new ComboBoxControl
            {
                DataContext = this
            };
        }
    }
}
