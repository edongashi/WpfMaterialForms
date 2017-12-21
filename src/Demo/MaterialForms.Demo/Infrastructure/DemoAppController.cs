using System.Windows;
using MahApps.Metro.Controls;
using Material.Application.Infrastructure;
using Material.Application.Routing;
using MaterialForms.Demo.Models;
using MaterialForms.Demo.Routes;
using MaterialForms.Mappers;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Controls;

namespace MaterialForms.Demo.Infrastructure {
    public class LoginExtensions : MaterialMapper<Login> {
        public LoginExtensions () {
            AddPropertyAttribute(i => i.RememberMe,() => new FieldAttribute { Name = "sdfgsrysert" });
        }
    }

    public class DemoAppController : AppController {
        protected override void OnInitializing () {
            var factory = Routes.RouteFactory;
            Routes.MenuRoutes.Add (InitialRoute = factory.Get<HomeRoute> ());
            Routes.MenuRoutes.Add (factory.Get<ExamplesRoute> ());
            Routes.MenuRoutes.Add (factory.Get<XmlExamplesRoute> ());
            FontSize = 15d;

            var window = new MetroWindow () { Content = new DynamicForm { Model = new Login () }, WindowStartupLocation = WindowStartupLocation.CenterScreen, Height = 200, Width = 200 };
            window.Show ();
        }

    }
}