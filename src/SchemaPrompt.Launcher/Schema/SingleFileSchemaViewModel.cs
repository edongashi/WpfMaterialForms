using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Validation;

namespace SchemaPrompt.Launcher.Schema
{
    public class SingleFileSchemaViewModel : BaseSchemaViewModel
    {
        private string path;

        public string Path
        {
            get { return path; }
            set
            {
                if (value == path) return;
                path = value;
                OnPropertyChanged();
            }
        }
    }
}
