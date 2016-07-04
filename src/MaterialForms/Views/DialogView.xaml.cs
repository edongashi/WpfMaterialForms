using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MaterialForms.Views
{
    /// <summary>
    /// Interaction logic for FormView.xaml
    /// </summary>
    public partial class DialogView : UserControl
    {
        public DialogView()
        {
            InitializeComponent();
            CommandManager.AddExecutedHandler(this, CloseDialogCommand_Executed);
        }

        private Session Session => (Session)GetValue(SessionAssist.HostingSessionProperty);

        private void CloseDialogCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var action = e.Parameter as bool?;
            var dialog = (MaterialDialog)DataContext;

            if (action == true)
            {
                if (dialog.ValidatesOnPositiveAction && !dialog.Validate())
                {
                    e.Handled = true;
                    return;
                }

                if (dialog.OnPositiveAction != null)
                {
                    e.Handled = true;
                    HandleCallback(dialog.OnPositiveAction, dialog.ShowsProgressOnPositiveAction);
                }
            }
            else if (action == false && dialog.OnNegativeAction != null)
            {
                e.Handled = true;
                HandleCallback(dialog.OnNegativeAction, false);
            }
        }

        private void AuxiliaryActionButton_Click(object sender, RoutedEventArgs e)
        {
            FormActionCallback callback;
            if ((callback = ((MaterialDialog)DataContext).OnAuxiliaryAction) != null)
            {
                HandleCallback(callback, false);
            }
        }

        private async void HandleCallback(FormActionCallback callback, bool showProgress)
        {
            var session = Session;
            try
            {
                session.Lock();
                IsEnabled = false;
                if (showProgress)
                {
                    ((Storyboard)FindResource("ShowProgressCard")).Begin();
                }

                await callback(session);
            }
            finally
            {
                if (showProgress)
                {
                    ((Storyboard)FindResource("HideProgressCard")).Begin();
                }

                IsEnabled = true;
                session.Unlock();
            }
        }
    }
}
