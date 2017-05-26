namespace MaterialForms.Wpf.Controls
{
    public interface IMaterialForm
    {
        object Model { get; }

        object Value { get; }

        object Context { get; }
    }
}
