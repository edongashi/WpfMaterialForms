using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;
using System.Windows;

namespace SchemaPrompt.Launcher.Schema
{
    public class StringSchemaViewModel
    {
        private static readonly Random Random = new Random();

        public Visibility IconVisibility { get; set; } = Visibility.Visible;

        public PackIconKind IconKind { get; set; } = (PackIconKind)Random.Next((600));

        public string Value { get; set; } = "";

        public string Hint { get; set; } = "Floating hint";

        public string ToolTip { get; set; } = "Enter text here. It can not be empty.";

        public ValidationRule ValidationRule { get; set; } = new NotEmptyValidationRule();
    }
}
