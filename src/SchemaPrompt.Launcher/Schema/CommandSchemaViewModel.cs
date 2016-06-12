using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;
using System.Windows;

namespace SchemaPrompt.Launcher.Schema
{
    public class CommandSchemaViewModel : BaseSchemaViewModel
    {
        public string CommandHint { get; set; } = "Action";
    }
}
