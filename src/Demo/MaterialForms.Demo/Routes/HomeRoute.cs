using System.Collections.Generic;
using System.Windows;
using Material.Application.Infrastructure;
using Material.Application.Routing;
using MaterialDesignThemes.Wpf;
using MaterialForms.Demo.Models;
using MaterialForms.Demo.Models.Home;
using MaterialForms.Extensions;
using MaterialForms.Wpf;

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

        protected override void RouteReady(RouteActivationMethod routeActivationMethod, IEnumerable<RouteEventError> errors)
        {
            Dialog.For<Login>().AsMaterialDialog().Show().ContinueWith(task =>
            {
                
            });
            base.RouteReady(routeActivationMethod, errors);
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
