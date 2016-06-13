using System.Windows.Controls;
using System.Collections.ObjectModel;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class SelectionSchema : SchemaBase
    {
        private object value;
        private ObservableCollection<object> items;

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

        public ObservableCollection<object> Items
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
