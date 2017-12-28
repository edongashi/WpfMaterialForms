using System;

namespace MaterialForms.Extensions
{
    public class DialogResult<T>
    {
        public DialogResult(T model, string action)
        {
            Model = model;
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public DialogResult(T model)
        {
            Model = model;
        }

        public DialogResult()
        {
        }

        public T Model { get; set; }
        public string Action { get; set; }
    }
}