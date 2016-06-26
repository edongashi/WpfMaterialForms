using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;

namespace MaterialForms
{
    /// <summary>
    /// Manages the lifecycle of a window displayed to the user.
    /// </summary>
    public class WindowSession : Session
    {
        // Synchronization
        private readonly object syncRoot = new object();
        private readonly Dispatcher dispatcher;

        // Window state
        private MaterialFormsWindow wpfWindow;
        private Task<bool?> task;
        private bool closeRequested;
        private bool? closeResult;

        internal WindowSession(MaterialWindow window, Dispatcher dispatcher)
        {
            Window = window;
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Occurs when the window has loaded and has been displayed to the user.
        /// </summary>
        public event EventHandler Loaded;

        /// <summary>
        /// Gets the data context of the displayed window.
        /// </summary>
        public MaterialWindow Window { get; }

        /// <summary>
        /// Gets whether the session has finished and the window is closed.
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Gets whether the window has loaded and has been displayed to the user. A true value does not guarantee that the window is still open.
        /// </summary>
        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Gets the dialog associated with the MaterialWindow context.
        /// </summary>
        public override MaterialDialog Dialog => Window.Dialog;

        /// <summary>
        /// Gets the task representing the window session being displayed. The task returns the window's dialog result.
        /// </summary>
        /// <remarks>Waiting synchronously for this task from the same dispatcher will cause a deadlock.</remarks>
        public override Task<bool?> Task => task;

        /// <summary>
        /// Closes the displayed window unconditionally.
        /// </summary>
        /// <param name="result">The value that will be returned from the session's task.</param>
        public override void Close(bool? result)
        {
            lock (syncRoot)
            {
                if (IsClosed)
                {
                    return;
                }

                closeRequested = true;
                closeResult = result;
                if (!IsLoaded)
                {
                    return;
                }

                if (dispatcher.CheckAccess())
                {
                    wpfWindow.Close();
                }
                else
                {
                    dispatcher.Invoke(() => wpfWindow.Close());
                }
            }
        }

        public Task<bool?> ShowDialog(MaterialDialog dialog, double dialogWidth = double.NaN) => ShowDialogTracked(dialog, dialogWidth).Task;

        public DialogSession ShowDialogTracked(MaterialDialog dialog, double dialogWidth = double.NaN)
        {
            if (!IsLoaded || IsClosed)
            {
                throw new InvalidOperationException("Current session has not yet loaded or has been closed.");
            }

            return new DialogSession("DialogHost" + Id, dialog, dialogWidth).Show();
        }

        public Task Alert(string message) => Alert(message, null);

        public Task Alert(string message, string title) => Alert(message, title, "OK");

        public Task Alert(string message, string title, string action)
        {
            return ShowDialog(new MaterialDialog(message, title, action, null), 275d);
        }

        public Task<bool?> Prompt(string message) => Prompt(message, null);

        public Task<bool?> Prompt(string message, string title) => Prompt(message, title, "OK");

        public Task<bool?> Prompt(string message, string title, string positiveAction) => Prompt(message, title, positiveAction, "CANCEL");

        public async Task<bool?> Prompt(string message, string title, string positiveAction, string negativeAction)
        {
            return await ShowDialog(new MaterialDialog(message, title, positiveAction, negativeAction), 275d) as bool?;
        }

        internal WindowSession Show()
        {
            var completion = new TaskCompletionSource<bool?>();
            dispatcher.InvokeAsync(() =>
            {
                try
                {
                    if (closeRequested)
                    {
                        completion.SetResult(closeResult);
                        return;
                    }

                    wpfWindow = new MaterialFormsWindow(this, Id);
                    if (closeRequested)
                    {
                        completion.SetResult(closeResult);
                        return;
                    }

                    wpfWindow.Loaded += (sender, args) =>
                    {
                        lock (syncRoot)
                        {
                            if (closeRequested)
                            {
                                completion.SetResult(closeResult);
                                wpfWindow.Close();
                                return;
                            }

                            IsLoaded = true;
                        }

                        Loaded?.Invoke(this, EventArgs.Empty);
                    };

                    var result = wpfWindow.ShowDialog();
                    completion.SetResult(closeRequested ? closeResult : result);
                }
                catch (Exception ex)
                {
                    completion.SetException(ex);
                }
                finally
                {
                    IsClosed = true;
                    wpfWindow = null;
                }
            });

            task = completion.Task;
            return this;
        }
    }
}
