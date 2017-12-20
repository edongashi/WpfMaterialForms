using System;
using System.Reflection;
using System.Windows;
using MaterialForms.Demo.Infrastructure;
using MaterialForms.Demo.Models;
using MaterialForms.Mappers;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            var login = new Login();
            var newLoginType = login.InjectAttributes(u => u.RememberMe, new[] {new FieldAttribute {Name = "Fuck"}});
            var props = newLoginType.GetType().GetProperty("RememberMe")?.GetCustomAttributes();
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public DemoAppController Controller { get; }

        public App()
        {
            Controller = new DemoAppController();
        }

        protected void OnStartup(object sender, StartupEventArgs e)
        {
            Controller.ShowApplicationWindow();
        }
    }
}
