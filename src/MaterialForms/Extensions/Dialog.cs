using System;
using MaterialForms.Wpf.Controls;
using Proxier.Mappers;

namespace MaterialForms.Extensions
{
    public static class Dialog
    {
        public static Tuple<DynamicForm, T> For<T>(T obj = default(T)) where T : class
        {
            if (obj == null || obj is Type)
                obj = (T) Activator.CreateInstance(typeof(T).GetInjectedType());

            return new Tuple<DynamicForm, T>(DialogExtensions.CreateDynamicForm(obj), obj);
        }
    }
}