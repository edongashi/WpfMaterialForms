using System;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MaterialForms.Extensions.Models;
using MaterialForms.Wpf.Controls;

namespace MaterialForms.Extensions.Routines
{
    public class ShowDialogRoutine<T> : ShowRoutine<T>
    {
        public override async Task<DialogResult<T>> Show()
        {
            var window = Application.Current.Windows[0];

            if (window == null)
                throw new Exception("Could not determinate the application's window.");

            var task = new TaskCompletionSource<object>();

            var dialogHost = window.GetChildOfType<DialogHost>();

            if (dialogHost == null)
                window.Content = new DialogHost {Content = window.Content};

            var returnObject = new DialogResult<T>();

            RegisterActionCallback(Form, returnObject, null, task);

            await dialogHost.ShowDialog(Form);

            await task.Task;

            return returnObject;
        }


        public ShowDialogRoutine(DynamicForm form) : base(form)
        {
        }
    }
}