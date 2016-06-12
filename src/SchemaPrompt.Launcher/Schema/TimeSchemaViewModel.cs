using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;
using System.Windows;

namespace SchemaPrompt.Launcher.Schema
{
    public class TimeSchemaViewModel : BaseSchemaViewModel
    {
        private string value;
        private bool is24Hours = true;

        public string Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public bool Is24Hours
        {
            get { return is24Hours; }
            set
            {
                if (value == is24Hours) return;
                is24Hours = value;
                OnPropertyChanged();
            }
        }
    }
}
