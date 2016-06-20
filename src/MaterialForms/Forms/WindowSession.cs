using System;
using System.Threading;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace MaterialForms
{
    public class WindowSession
    {
        private readonly int id;

        internal WindowSession(int id, Task<bool?> task)
        {
            this.id = id;
            Task = task;
            CancellationSource = new CancellationTokenSource();
        }

        public bool Loaded { get; internal set; }

        public bool Closed { get; internal set; }

        public Task<bool?> Task { get; }

        public void Close()
        {
            Closed = true;
            lock (CancellationSource)
            {
                CancellationSource.Cancel();
                if (!Loaded)
                {
                    return;
                }

                var dispatcher = Window.Dispatcher;
                if (dispatcher.CheckAccess())
                {
                    Window.Close();
                }
                else
                {
                    Window.Dispatcher.Invoke(() => Window.Close());
                }
            }
        }

        internal CancellationTokenSource CancellationSource { get; }

        internal MaterialFormsWindow Window { get; set; }

        public Task<object> ShowDialog(IViewProvider dialog, double dialogWidth = double.NaN)
        {
            var view = dialog.View;
            view.Width = dialogWidth;
            return DialogHost.Show(view, "DialogHost" + id);
        }
    }
}
