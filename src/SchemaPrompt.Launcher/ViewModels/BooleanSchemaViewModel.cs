using System.Windows.Controls;
using SchemaPrompt.Launcher.Controls;
using SchemaPrompt.Launcher.ViewModels;

namespace SchemaPrompt.Launcher.ViewModels
{
    internal class BooleanSchemaViewModel : BaseSchemaViewModel
    {
        private bool value;
        private bool isCheckBox;

        public bool Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public bool IsCheckBox
        {
            get { return isCheckBox; }
            set
            {
                if (value == isCheckBox) return;
                isCheckBox = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            if (isCheckBox)
            {
                return new CheckBoxControl
                {
                    DataContext = this
                };
            }
            else
            {
                return new SwitchControl()
                {
                    DataContext = this
                };
            }
        }
    }
}
