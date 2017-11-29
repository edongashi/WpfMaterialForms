using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class BooleanSchema : SchemaBase
    {
        private bool isCheckBox;
        private bool value;

        public bool Value
        {
            get => value;
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckBox
        {
            get => isCheckBox;
            set
            {
                if (value == isCheckBox) return;
                isCheckBox = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(View));
            }
        }

        public override bool HoldsValue => true;

        public override UserControl CreateView()
        {
            if (isCheckBox)
                return new CheckBoxControl
                {
                    DataContext = this
                };

            return new SwitchControl
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
            Value = obj as bool? ?? false;
        }
    }
}