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
        public object Value { get; set; } = "";

        public IEnumerable<object> Items { get; set; } = new object[] { "First", "Second", "Third" };
    }
}
