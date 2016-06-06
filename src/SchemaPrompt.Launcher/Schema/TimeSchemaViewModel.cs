using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;
using System.Windows;

namespace SchemaPrompt.Launcher.Schema
{
    public class TimeSchemaViewModel : BaseSchemaViewModel
    {
        public string Value { get; set; }

        public bool Is24Hours { get; set; } = true;
    }
}
