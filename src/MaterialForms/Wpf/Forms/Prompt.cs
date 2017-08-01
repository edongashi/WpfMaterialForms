using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Forms.Base;

namespace MaterialForms.Wpf.Forms
{
    [Form(Mode = DefaultFields.None)]

    [Title("{Binding Title}", IsVisible = "{Binding Title|IsNotEmpty}")]
    [Text("{Binding Message}", IsVisible = "{Binding Message|IsNotEmpty}")]
    [Break(Height = 16d, InsertAfter = true)]
    [Action("negative", "{Binding NegativeAction}", IsVisible = "{Binding NegativeAction|IsNotEmpty}")]
    [Action("positive", "{Binding PositiveAction}", IsVisible = "{Binding PositiveAction|IsNotEmpty}")]
    public sealed class Prompt<T> : DialogBase
    {
        private T value;
        private string fieldName;
        private string toolTip;
        private PackIconKind? icon;

        [Field(Name = "{Binding Name}", ToolTip = "{Binding ToolTip}")]
        public T Value
        {
            get => value;
            set
            {
                if (Equals(value, this.value)) return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        public string FieldName
        {
            get => fieldName;
            set
            {
                if (value == fieldName) return;
                fieldName = value;
                OnPropertyChanged();
            }
        }

        public string ToolTip
        {
            get => toolTip;
            set
            {
                if (value == toolTip) return;
                toolTip = value;
                OnPropertyChanged();
            }
        }
    }
}
