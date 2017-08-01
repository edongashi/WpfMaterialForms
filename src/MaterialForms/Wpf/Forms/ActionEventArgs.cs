using System;

namespace MaterialForms.Wpf.Forms
{
    public class ActionEventArgs : EventArgs
    {
        public ActionEventArgs(string action)
        {
            Action = action;
        }

        public string Action { get; }
    }
}
