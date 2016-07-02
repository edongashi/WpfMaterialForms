using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                    HandleCallback(dialog.OnPositiveAction);
                }
            }
            else if (action == false && dialog.OnNegativeAction != null)
            {
                e.Handled = true;
                HandleCallback(dialog.OnNegativeAction);
            }
        }

        private void AuxiliaryActionButton_Click(object sender, RoutedEventArgs e)
        {
            Func<Session, Task> callback;
            if ((callback = ((MaterialDialog)DataContext).OnAuxiliaryAction) != null)
            {
                HandleCallback(callback);
            }
        }

        private async void HandleCallback(Func<Session, Task> callback)
        {
            IsEnabled = false;
            try
            {
                await callback(Session);
            }
            finally
            {
                IsEnabled = true;
            }
        }
    }
}
