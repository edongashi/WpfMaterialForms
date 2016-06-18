using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using MaterialForms.Annotations;
using MaterialForms.Validation;

namespace MaterialForms
{
    /// <summary>
    /// Base class for all schemas. Custom data types and controls can be implemented by extending this class.
    /// </summary>
    public abstract class SchemaBase : IViewProvider
    {
        private string key;
        private string description;
        private string name;
        private PackIconKind packIconKind;
        private Visibility iconVisibility = Visibility.Collapsed;

        /// <summary>
        /// Gets or sets the key that identifies the value in the form. Does not appear in the UI.
        /// </summary>
        public string Key
        {
            get { return key; }
            set
            {
                if (value == key) return;
                key = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the display name of the schema, usually appearing as a control hint or label.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the description of the schema, usually appearing as a tooltip on mouse over.
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                if (value == description) return;
                description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the icon type shown in the control. Some controls do not display icons.
        /// </summary>
        public IconKind IconKind
        {
            get { return (IconKind)packIconKind; }
            set
            {
                PackIconKind = (PackIconKind)value;
                IconVisibility = Visibility.Visible;
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

        public PackIconKind PackIconKind
        {
            get { return packIconKind; }
            set
            {
                if (value == packIconKind) return;
                packIconKind = value;
                OnPropertyChanged();
            }
        }

        public ValidationRule ValidationRule { get; set; } = new NotEmptyValidationRule();

        public UserControl View => CreateView();

        public abstract UserControl CreateView();

        public abstract bool HoldsValue { get; }

        public abstract object GetValue();

        public virtual void AssignValue(Action<string, object> assignFunction)
        {
            if (string.IsNullOrEmpty(Key))
            {
                return;
            }

            if (HoldsValue)
            {
                assignFunction(Key, GetValue());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
