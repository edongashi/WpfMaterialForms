using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SchemaPrompt.Launcher.Annotations;
using SchemaPrompt.Launcher.Validation;

namespace SchemaPrompt.Launcher.ViewModels
{
    internal abstract class BaseSchemaViewModel : INotifyPropertyChanged
    {
        private static readonly Random Random = new Random();
        private string toolTip = "Control Tooltip";
        private string hint = "Control Hint";
        private PackIconKind iconKind = (PackIconKind)Random.Next(600);
        private Visibility iconVisibility = Visibility.Visible;

        public string Hint
        {
            get { return hint; }
            set
            {
                if (value == hint) return;
                hint = value;
                OnPropertyChanged();
            }
        }

        public string ToolTip
        {
            get { return toolTip; }
            set
            {
                if (value == toolTip) return;
                toolTip = value;
                OnPropertyChanged();
            }
        }

        public PackIconKind IconKind
        {
            get { return iconKind; }
            set
            {
                if (value == iconKind) return;
                iconKind = value;
                OnPropertyChanged();
            }
        }

        public Visibility IconVisibility
        {
            get { return iconVisibility; }
            set
            {
                if (value == iconVisibility) return;
                iconVisibility = value;
                OnPropertyChanged();
            }
        }

        public ValidationRule ValidationRule { get; set; } = new NotEmptyValidationRule();

        public abstract UserControl CreateView();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
