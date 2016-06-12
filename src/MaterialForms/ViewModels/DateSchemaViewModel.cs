using System;
using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms.ViewModels
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
