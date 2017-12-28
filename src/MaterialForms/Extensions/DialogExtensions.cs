using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Controls;
using Proxier.Mappers;

namespace MaterialForms.Extensions
{
    public static class DialogExtensions
    {
        private static Task ShowPopup<TPopup>(this TPopup popup)
            where TPopup : Window
        {
            var task = new TaskCompletionSource<object>();
            popup.Owner = Application.Current.MainWindow;
            popup.Closed += (s, a) => task.SetResult(null);
            popup.Show();
            popup.Focus();
            return task.Task;
        }

        public static async Task<DialogResult<T>> AsWindow<T>(this Tuple<DynamicForm, T> dynamicForm)
        {
            var childWindow = new MetroWindow
            {
                Content = dynamicForm.Item1,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Height = 200,
                Width = 200
            };

            var returnObject = new DialogResult<T>();
            RegisterActionCallback(dynamicForm.Item1, returnObject, childWindow);

            await childWindow.ShowPopup();
            return returnObject;
        }

        public static async Task<DialogResult<T>> AsMaterialDialog<T>(this Tuple<DynamicForm, T> dynamicForm)
        {
            var window = Application.Current.Windows[0];

            if (window == null)
                throw new Exception("Could not determinate the application's window.");

            var dialogHost = window.GetChildOfType<DialogHost>();

            if (dialogHost == null)
                window.Content = new DialogHost {Content = window.Content};

            var returnObject = new DialogResult<T>();

            RegisterActionCallback(dynamicForm.Item1, returnObject, null);

            await dialogHost.ShowDialog(dynamicForm.Item1);

            await Task.Factory.StartNew(() =>
            {
                while (string.IsNullOrEmpty(returnObject.Action))
                {
                }
            });

            return returnObject;
        }
        
        private static void RegisterActionCallback<T>(DynamicForm dynamicForm, DialogResult<T> returnObject,
            Window childWindow)
        {
            dynamicForm.Action += (sender, s) =>
            {
                if (!(sender is DynamicForm form)) return;
                returnObject.Model =
                    (T) form.Model.CopyTo(Activator.CreateInstance(typeof(T).AddParameterlessConstructor()));
                returnObject.Action = s;
                childWindow?.Close();
            };
        }

        public static DynamicForm CreateDynamicForm<T>(T obj = default(T)) where T : class
        {
            return new DynamicForm {Model = obj};
        }

        private static T GetChildOfType<T>(this DependencyObject depObj)
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