using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Forms.Base;

namespace MaterialForms.Wpf.Forms
{
    [Form(Mode = DefaultFields.None)]

    [Title("{Binding Title}", IsVisible = "{Binding Title|IsNotEmpty}")]
    [Text("{Binding Message}", IsVisible = "{Binding Message|IsNotEmpty}")]
    [Action("negative", "{Binding NegativeAction}", IsVisible = "{Binding NegativeAction|IsNotEmpty}")]
    [Action("positive", "{Binding PositiveAction}", IsVisible = "{Binding PositiveAction|IsNotEmpty}")]
    public sealed class Confirm : DialogBase
    {
    }
}
