using System;

namespace MaterialForms.Wpf
{
    public class ActionEventArgs : EventArgs
    {
        public ActionEventArgs(object model, string action, object parameter)
        {
            Model = model;
            Action = action;
            Parameter = parameter;
        }

        public object Model { get; }

        public string Action { get; }

        public object Parameter { get; }
    }
}
