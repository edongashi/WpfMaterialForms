using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialForms.Annotations;

namespace MaterialForms.Wpf.Forms.Base
{
    public abstract class FormBase : IActionHandler, INotifyPropertyChanged
    {
        public void HandleAction(object model, string action, object parameter)
        {
            OnAction(action, parameter);
            ActionPerformed?.Invoke(this, new ActionEventArgs(action));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ActionEventArgs> ActionPerformed;

        protected virtual void OnAction(string action, object parameter)
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}