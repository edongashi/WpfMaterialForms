using System;
using System.Threading.Tasks;

namespace MaterialForms.Tasks
{
    /// <summary>
    ///     Allows running background tasks while displaying a controllable progress dialog.
    /// </summary>
    public static class TaskRunner
    {
        #region Non-generic

        public static Task Run(Func<ProgressController, Task> callback)
        {
            return Run(callback, null, (string) null);
        }

        public static Task Run(Func<ProgressController, Task> callback, Session session)
        {
            return Run(callback, null, session);
        }

        public static Task Run(Func<ProgressController, Task> callback, string dialogIdentifier)
        {
            return Run(callback, null, dialogIdentifier);
        }

        public static Task Run(Func<ProgressController, Task> callback, ProgressDialogOptions options)
        {
            return Run(callback, options, (string) null);
        }

        public static Task Run(Func<ProgressController, Task> callback, ProgressDialogOptions options, Session session)
        {
            return Run(callback, options, (session as WindowSession)?.DialogIdentifier);
        }

        public static async Task Run(Func<ProgressController, Task> callback, ProgressDialogOptions options,
            string dialogIdentifier)
        {
            if (options == null)
                options = new ProgressDialogOptions();

            var controller = new ProgressController(options);
            Session session;
            if (dialogIdentifier != null)
                session = controller.Dialog.ShowTracked(dialogIdentifier, 350d);
            else
                session = new MaterialWindow(controller.Dialog).ShowTracked();

            try
            {
                await Task.Run(() => callback(controller));
            }
            finally
            {
                session.Close(true);
            }
        }

        #endregion

        #region Generic

        public static Task<T> Run<T>(Func<ProgressController, Task<T>> callback)
        {
            return Run(callback, null, (string) null);
        }

        public static Task<T> Run<T>(Func<ProgressController, Task<T>> callback, Session session)
        {
            return Run(callback, null, session);
        }

        public static Task<T> Run<T>(Func<ProgressController, Task<T>> callback, string dialogIdentifier)
        {
            return Run(callback, null, dialogIdentifier);
        }

        public static Task<T> Run<T>(Func<ProgressController, Task<T>> callback, ProgressDialogOptions options)
        {
            return Run(callback, options, (string) null);
        }

        public static Task<T> Run<T>(Func<ProgressController, Task<T>> callback, ProgressDialogOptions options,
            Session session)
        {
            return Run(callback, options, (session as WindowSession)?.DialogIdentifier);
        }

        public static async Task<T> Run<T>(Func<ProgressController, Task<T>> callback, ProgressDialogOptions options,
            string dialogIdentifier)
        {
            if (options == null)
                options = new ProgressDialogOptions();

            var controller = new ProgressController(options);
            Session session;
            if (dialogIdentifier != null)
                session = controller.Dialog.ShowTracked(dialogIdentifier, 250d);
            else
                session = new MaterialWindow(controller.Dialog).ShowTracked();

            T result;
            try
            {
                result = await Task.Run(() => callback(controller));
            }
            finally
            {
                session.Close(true);
            }

            return result;
        }

        #endregion
    }
}