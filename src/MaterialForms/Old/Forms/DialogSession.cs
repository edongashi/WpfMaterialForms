using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MaterialDialogSession = MaterialDesignThemes.Wpf.DialogSession;

namespace MaterialForms
{
    public class DialogSession : Session
    {
        private readonly string dialogIdentifier;
        private readonly double dialogWidth;
        private MaterialDialogSession sessionInstance;
        private Task<bool?> task;

        internal DialogSession(string dialogIdentifier, MaterialDialog dialog, double dialogWidth)
        {
            this.dialogIdentifier = dialogIdentifier;
            this.dialogWidth = dialogWidth;
            Dialog = dialog;
        }

        public override MaterialDialog Dialog { get; }

        public override Task<bool?> Task => task;

        public override void Close(bool? result)
        {
            try
            {
                sessionInstance.Close(result);
            }
            catch
            {
                // ignored
            }
        }

        internal DialogSession Show()
        {
            var view = Dialog.View;
            view.Width = dialogWidth;
            view.SetValue(SessionAssist.HostingSessionProperty, this);
            task = StartTask(view);
            return this;
        }

        private async Task<bool?> StartTask(DependencyObject view)
        {
            return await DialogHost.Show(view, dialogIdentifier,
                (object sender, DialogOpenedEventArgs args) => { sessionInstance = args.Session; }) as bool?;
        }
    }
}