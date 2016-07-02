using System.Windows.Controls;
using System.Collections.ObjectModel;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class SelectionSchema : SchemaBase
    {
        private object value;
        private ObservableCollection<object> items;
        private bool isEditable;

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

        public bool IsEditable
        {
            get { return isEditable; }
            set
            {
                if (value == isEditable) return;
                isEditable = value;
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

        public override bool HoldsValue => true;

        public override object GetValue() => Value;
    }
}
