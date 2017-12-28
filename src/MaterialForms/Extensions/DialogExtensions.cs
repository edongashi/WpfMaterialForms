using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MaterialForms.Extensions.Interfaces;
using MaterialForms.Extensions.Routines;
using MaterialForms.Wpf.Controls;
using Proxier.Mappers;

namespace MaterialForms.Extensions
{
    public static class DialogExtensions
    {
        public static Task ShowPopup<TPopup>(this TPopup popup)
            where TPopup : Window
        {
            var task = new TaskCompletionSource<object>();
            popup.Owner = Application.Current.MainWindow;
            popup.Closed += (s, a) => task.SetResult(null);
            popup.Show();
            popup.Focus();
            return task.Task;
        }

        public static IShowRoutine<T> AsCustom<T>(this Tuple<DynamicForm, T> dynamicForm, IShowRoutine<T> routine)
        {
            routine.Form = dynamicForm.Item1;
            return routine;
        }

        public static ShowWindowRoutine<T> AsWindow<T>(this Tuple<DynamicForm, T> dynamicForm)
        {
            return new ShowWindowRoutine<T>(dynamicForm.Item1);
        }

        public static ShowDialogRoutine<T> AsMaterialDialog<T>(this Tuple<DynamicForm, T> dynamicForm)
        {
            return new ShowDialogRoutine<T>(dynamicForm.Item1);
        }

        public static DynamicForm CreateDynamicForm<T>(T obj = default(T)) where T : class
        {
            return new DynamicForm {Model = obj};
        }

        internal static T GetChildOfType<T>(this DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj == null) return null;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = child as T ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }

            return null;
        }
    }
}