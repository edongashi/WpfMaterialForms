using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class SelectionSchema : SchemaBase
    {
        private bool isEditable;
        private ObservableCollection<object> items;
        private object value;

        public object Value
        {
            get => value;
            set
            {
                if (Equals(value, this.value)) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<object> Items
        {
            get => items;
            set
            {
                if (Equals(value, items)) return;
                items = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditable
        {
            get => isEditable;
            set
            {
                if (value == isEditable) return;
                isEditable = value;
                OnPropertyChanged();
            }
        }

        public override bool HoldsValue => true;

        public override UserControl CreateView()
        {
            return new ComboBoxControl
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
            if (obj == null)
            {
                Value = null;
                return;
            }

            Value = Items.FirstOrDefault(item => Equals(item, obj));
        }
    }
}