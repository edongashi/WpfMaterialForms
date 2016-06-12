using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;
using System.Windows;

namespace SchemaPrompt.Launcher.Schema
{
    public class DateSchemaViewModel : BaseSchemaViewModel
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
    }
}
