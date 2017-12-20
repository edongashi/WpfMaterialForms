using System;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using MaterialForms.Demo.Infrastructure;
using MaterialForms.Demo.Models;
using MaterialForms.Mappers;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Controls;

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