using System;
using System.Threading.Tasks;
using System.Windows;
using MaterialForms.Extensions.Interfaces;
using MaterialForms.Extensions.Models;
using MaterialForms.Wpf.Controls;
using Proxier.Mappers;

namespace MaterialForms.Extensions.Routines
{
    public abstract class ShowRoutine<T> : IShowRoutine<T>
    {
        public ShowRoutine(DynamicForm form)
        {
            Form = form;
        }

        public void RegisterActionCallback(DynamicForm dynamicForm, DialogResult<T> returnObject,
            Window childWindow, TaskCompletionSource<object> toFlag = null)
        {
            dynamicForm.Action += (sender, s) =>
            {
                if (!(sender is DynamicForm form)) return;
                returnObject.Model =
                    (T) form.Model.CopyTo(Activator.CreateInstance(typeof(T).AddParameterlessConstructor()));
                returnObject.Action = s;
                childWindow?.Close();
                toFlag?.SetResult(null);
            };
        }

        public DynamicForm Form { get; set; }
        public abstract Task<DialogResult<T>> Show();
    }
}