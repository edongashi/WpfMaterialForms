using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MaterialForms.Annotations;

namespace MaterialForms
{
    public class MaterialWindow : INotifyPropertyChanged
    {
        private static int staticDialogId;
        private static DispatcherOption defaultDispatcher;
        private static Dispatcher customDispatcher;

        static MaterialWindow()
        {
            defaultDispatcher = DispatcherOption.CurrentThread;
            var materialFormsApplication = Application.Current ??
                                           new Application { ShutdownMode = ShutdownMode.OnExplicitShutdown };
            LoadResources(materialFormsApplication);
        }

        public static void LoadResources(Application application)
        {
            application.Resources.MergedDictionaries.Add(
                Application.LoadComponent(
                    new Uri("MaterialForms;component/Resources/Material.xaml",
                    UriKind.Relative)) as ResourceDictionary);
        }

        public static void ShutDownCustomDispatcher()
        {
            if (customDispatcher == null)
            {
                return;
            }

            customDispatcher.InvokeShutdown();
            customDispatcher = null;
        }

        public static void ShutDownApplication()
        {
            Application.Current.Shutdown();
        }

        public static void SetDefaultDispatcher(DispatcherOption dispatcherOption)
        {
            defaultDispatcher = dispatcherOption;
        }

        public static bool CheckDispatcherAccess() => CheckDispatcherAccess(defaultDispatcher);

        public static bool CheckDispatcherAccess(DispatcherOption dispatcherOption)
        {
            var dispatcher = GetDispatcher(dispatcherOption);
            return dispatcher.CheckAccess();
        }

        public static void RunDispatcher() => Dispatcher.Run();

        private static Dispatcher GetDispatcher(DispatcherOption dispatcherOption)
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

        private static Dispatcher GetCustomDispatcher()
        {
            if (customDispatcher != null)
            {
                return customDispatcher;
            }

            var waitHandle = new ManualResetEventSlim();
            var thread = new Thread(() =>
            {
                customDispatcher = Dispatcher.CurrentDispatcher;
                waitHandle.Set();
                Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            waitHandle.Wait();
            return customDispatcher;
        }

        private string title = "Dialog";
        private double width = 400d;
        private double height = double.NaN;
        private bool showMinButton;
        private bool showMaxRestoreButton = true;
        private bool showCloseButton;
        private bool canResize;
        private MaterialDialog dialog;

        public MaterialWindow()
        {
        }

        public MaterialWindow(MaterialDialog dialog)
        {
            Dialog = dialog;
        }

        public WindowSession CurrentSession { get; private set; }

        public string Title
        {
            get { return title; }
            set
            {
                if (value == title) return;
                title = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get { return width; }
            set
            {
                if (value.Equals(width)) return;
                width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                if (value.Equals(height)) return;
                height = value;
                OnPropertyChanged();
            }
        }

        public bool ShowMinButton
        {
            get { return showMinButton; }
            set
            {
                if (value == showMinButton) return;
                showMinButton = value;
                OnPropertyChanged();
            }
        }

        public bool ShowMaxRestoreButton
        {
            get { return showMaxRestoreButton; }
            set
            {
                if (value == showMaxRestoreButton) return;
                showMaxRestoreButton = value;
                OnPropertyChanged();
            }
        }

        public bool ShowCloseButton
        {
            get { return showCloseButton; }
            set
            {
                if (value == showCloseButton) return;
                showCloseButton = value;
                OnPropertyChanged();
            }
        }

        public bool CanResize
        {
            get { return canResize; }
            set
            {
                if (value == canResize) return;
                canResize = value;
                OnPropertyChanged();
            }
        }

        public MaterialDialog Dialog
        {
            get { return dialog; }
            set
            {
                if (Equals(value, dialog)) return;
                dialog = value;
                OnPropertyChanged();
            }
        }

        public Task<bool?> Show() => Show(defaultDispatcher);

        public Task<bool?> Show(DispatcherOption dispatcherOption) => ShowTracked(GetDispatcher(dispatcherOption)).Task;

        public WindowSession ShowTracked() => ShowTracked(defaultDispatcher);

        public WindowSession ShowTracked(DispatcherOption dispatcherOption) => ShowTracked(GetDispatcher(dispatcherOption));

        private WindowSession ShowTracked(Dispatcher dispatcher)
        {
            if (CurrentSession != null)
            {
                throw new InvalidOperationException("A window session for this instance is already open.");
            }

            var id = Interlocked.Increment(ref staticDialogId);
            var completion = new TaskCompletionSource<bool?>();
            CurrentSession = new WindowSession(id, completion.Task);
            dispatcher.InvokeAsync(() =>
            {
                try
                {
                    var source = CurrentSession.CancellationSource;
                    var token = source.Token;
                    if (token.IsCancellationRequested)
                    {
                        completion.SetResult(null);
                        return;
                    }

                    var window = new MaterialFormsWindow(this, id);
                    CurrentSession.Window = window;
                    if (token.IsCancellationRequested)
                    {
                        completion.SetResult(null);
                        return;
                    }

                    window.Loaded += (sender, args) =>
                    {
                        lock (source)
                        {
                            if (token.IsCancellationRequested)
                            {
                                completion.SetResult(null);
                                window.Close();
                            }

                            CurrentSession.Loaded = true;
                        }
                    };

                    completion.SetResult(window.ShowDialog());
                    CurrentSession.Closed = true;
                    CurrentSession = null;
                }
                catch (Exception ex)
                {
                    completion.SetException(ex);
                }
            });

            return CurrentSession;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
