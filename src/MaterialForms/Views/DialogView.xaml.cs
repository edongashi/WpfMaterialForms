using System;
using System.Threading.Tasks;
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
        }

        private void CloseDialogCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var action = e.Parameter as bool?;
            var dialog = (MaterialDialog)DataContext;
            if (action == true && dialog.OnPositiveAction != null)
            {
                e.Handled = true;
                HandleCallback(dialog.OnPositiveAction);
            }
            else if (action == false && dialog.OnNegativeAction != null)
            {
                e.Handled = true;
                HandleCallback(dialog.OnNegativeAction);
            }
        }

        private void CloseDialogCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var action = e.Parameter as bool?;
            var dialog = (MaterialDialog)DataContext;
            e.CanExecute = (action == true && dialog.OnPositiveAction != null)
                           || (action == false && dialog.OnNegativeAction != null);
        }

        private void AuxiliaryActionButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
                var session = (Session)GetValue(SessionAssist.HostingSessionProperty);
                await callback(session);
            }
            finally
            {
                IsEnabled = true;
            }
        }
    }
}
