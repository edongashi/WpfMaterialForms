using System;
using System.Windows.Controls;
using SchemaPrompt.Launcher.Controls;
using SchemaPrompt.Launcher.ViewModels;

namespace SchemaPrompt.Launcher.ViewModels
{
    internal class DateSchemaViewModel : BaseSchemaViewModel
    {
        private DateTime? value;

        public DateTime? Value
        {
            get { return value; }
            set
            {
                if (value.Equals(this.value)) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new DatePickerControl
            {
                DataContext = this
            };
        }
    }
}
