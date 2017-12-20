using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using Material.Application.Infrastructure;
using Material.Application.Routing;
using MaterialDesignThemes.Wpf;
using MaterialForms.Demo.Models;
using MaterialForms.Demo.Models.Home;
using MaterialForms.Mappers;
using MaterialForms.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Controls;

namespace MaterialForms.Demo.Routes
{
    public class HomeRoute : Route, IActionHandler
    {
        private readonly INotificationService notificationService;

        public HomeRoute(INotificationService notificationService)
        {
            this.notificationService = notificationService;
            RouteConfig.Title = "Introduction";
            RouteConfig.Icon = PackIconKind.Home;
            Model = new Introduction();
        }

        public object Model { get; }

        public void HandleAction(object model, string action, object parameter)
        {
            switch (action?.ToLower())
            {
                case "copy" when parameter is string str:
                    Clipboard.SetText(str);
                    notificationService.Notify("Copied to clipboard.");
                    break;
                case "examples":
                    GoToMenuRoute<ExamplesRoute>();
                    break;
                case "xmlexamples":
                    GoToMenuRoute<XmlExamplesRoute>();
                    break;
                case "oldexamples":
                    var window = new MainWindow();
                    window.ShowDialog();
                    break;
            }
        }
    }
}
