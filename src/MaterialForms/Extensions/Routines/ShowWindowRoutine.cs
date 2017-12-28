using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MaterialForms.Extensions.Models;
using MaterialForms.Wpf.Controls;

namespace MaterialForms.Extensions.Routines
{
    public class ShowWindowRoutine<T> : ShowRoutine<T>
    {
        public override async Task<DialogResult<T>> Show()
        {
            var childWindow = new MetroWindow
            {
                Content = Form,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Height = 200,
                Width = 200
            };

            var returnObject = new DialogResult<T>();
            RegisterActionCallback(Form, returnObject, childWindow);

            await childWindow.ShowPopup();
            return returnObject;
        }

        public ShowWindowRoutine(DynamicForm form) : base(form)
        {
        }
    }
}