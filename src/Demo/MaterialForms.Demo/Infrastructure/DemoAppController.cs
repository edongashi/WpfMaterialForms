using Material.Application.Infrastructure;
using MaterialForms.Demo.Routes;

namespace MaterialForms.Demo.Infrastructure
{
    public class DemoAppController : AppController
    {
        protected override void OnInitializing()
        {
            Routes.MenuRoutes.Add(InitialRoute = new HomeRoute());
            FontSize = 15d;
        }
    }
}
