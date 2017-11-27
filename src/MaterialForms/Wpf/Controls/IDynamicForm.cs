namespace MaterialForms.Wpf.Controls
{
    public interface IDynamicForm
    {
        object Model { get; }

        object Value { get; }

        object Context { get; }
    }
}