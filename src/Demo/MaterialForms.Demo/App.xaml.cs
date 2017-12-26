using System;
using System.Windows;
using MaterialForms.Demo.Infrastructure;

namespace MaterialForms.Demo {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        [STAThread]
        public static void Main () {
            var app = new App ();
            app.InitializeComponent ();
            app.Run ();
        }

        public DemoAppController Controller { get; }

        public App () {

            Controller = new DemoAppController ();
        }

        protected void OnStartup (object sender, StartupEventArgs e) {
            Controller.ShowApplicationWindow ();
        }
    }
}