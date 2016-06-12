using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;
using System.Windows;

namespace SchemaPrompt.Launcher.Schema
{
    public class StringSchemaViewModel : BaseSchemaViewModel
    {
        private string value = "Value Text";

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
    }
}
