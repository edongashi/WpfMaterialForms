using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace MaterialForms
{
    public static class MaterialApplication
    {
        internal static DispatcherOption DefaultDispatcher;
        internal static Dispatcher CustomDispatcher;

        static MaterialApplication()
        {
            DefaultDispatcher = DispatcherOption.CurrentThread;
        }

        public static void CreateApplication()
        {
            var materialFormsApplication = Application.Current ??
                               new Application { ShutdownMode = ShutdownMode.OnExplicitShutdown };
            LoadMaterialDesign(materialFormsApplication);
        }

        public static void LoadMaterialDesign(Application application)
        {
            application.Resources.MergedDictionaries.Add(
                Application.LoadComponent(
                    new Uri("/MaterialForms;component/Resources/Material.xaml", UriKind.Relative)) as ResourceDictionary);
        }

        public static void ShutDownCustomDispatcher()
        {
            if (CustomDispatcher == null)
            {
                return;
            }

            CustomDispatcher.InvokeShutdown();
            CustomDispatcher = null;
        }

        public static void ShutDownApplication()
        {
            ShutDownCustomDispatcher();
            var application = Application.Current;
            if (application == null)
            {
                return;
            }

            if (application.CheckAccess())
            {
                application.Shutdown();
            }
            else
            {
                application.Dispatcher.InvokeAsync(application.Shutdown);
            }
        }

        public static void SetDefaultDispatcher(DispatcherOption dispatcherOption)
        {
            DefaultDispatcher = dispatcherOption;
        }

        public static bool CheckDispatcherAccess() => CheckDispatcherAccess(DefaultDispatcher);

        public static bool CheckDispatcherAccess(DispatcherOption dispatcherOption)
        {
            var dispatcher = GetDispatcher(dispatcherOption);
            return dispatcher.CheckAccess();
        }

        public static void RunDispatcher() => Dispatcher.Run();

        internal static Dispatcher GetDispatcher(DispatcherOption dispatcherOption)
        {
            Dispatcher dispatcher;
            switch (dispatcherOption)
            {
                case DispatcherOption.CurrentThread:
                    dispatcher = Dispatcher.CurrentDispatcher;
                    break;
                case DispatcherOption.CurrentApplication:
                    dispatcher = Application.Current.Dispatcher;
                    break;
                case DispatcherOption.Custom:
                    dispatcher = GetCustomDispatcher();
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return dispatcher;
        }

        internal static Dispatcher GetCustomDispatcher()
        {
            if (CustomDispatcher != null)
            {
                return CustomDispatcher;
            }

            var waitHandle = new ManualResetEventSlim();
            var thread = new Thread(() =>
            {
                CustomDispatcher = Dispatcher.CurrentDispatcher;
                waitHandle.Set();
                Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            waitHandle.Wait();
            return CustomDispatcher;
        }
    }
}
