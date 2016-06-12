using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;
using System.Windows;
using System.Collections.Generic;

namespace SchemaPrompt.Launcher.Schema
{
    public class SelectionSchemaViewModel : BaseSchemaViewModel
    {
        private object value = "";
        private IEnumerable<object> items = new object[] { "First", "Second", "Third" };

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

        public IEnumerable<object> Items
        {
            get { return items; }
            set
            {
                if (Equals(value, items)) return;
                items = value;
                OnPropertyChanged();
            }
        }
    }
}
