using Material.Application.Routing;
using MaterialDesignThemes.Wpf;

namespace MaterialForms.Demo.Routes
{
    public class HomeRoute : Route
    {
        public HomeRoute()
        {
            RouteConfig.Title = "Home";
            RouteConfig.Icon = PackIconKind.Home;
        }
    }
}
