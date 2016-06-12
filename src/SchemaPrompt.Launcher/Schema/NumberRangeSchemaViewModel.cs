using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;
using System.Windows;

namespace SchemaPrompt.Launcher.Schema
{
    public class NumberRangeSchemaViewModel : BaseSchemaViewModel
    {
        private int value;

        public int Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }
    }
}
