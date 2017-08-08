using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MaterialForms.Views
{
    /// <summary>
    /// Interaction logic for FormView.xaml
    /// </summary>
    public partial class DialogView : UserControl
    {
        private readonly MaterialDialog dialog;

        public DialogView(MaterialDialog dialog)
        {
            if (dialog == null)
            {
                throw new ArgumentNullException(nameof(dialog));
            }

            this.dialog = dialog;
            DataContext = dialog;
            var isDark = false;
            if (dialog.Theme == DialogTheme.Dark)
            {
                isDark = true;
            }
            else if (dialog.Theme == DialogTheme.Inherit)
            {
                if (Application.Current.Resources.MergedDictionaries
                    .Any(rd => rd.Source != null && rd.Source.OriginalString
                    .Contains("/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark")))
                {
                    isDark = true;
                }
            }

            if (isDark)
            {
                SetValue(ColorAssist.ForegroundProperty, Brushes.White);
                SetValue(ColorAssist.OpacityProperty, 0.70d);
                SetValue(ColorAssist.DisabledOpacityProperty, 0.50d);
            }

            var theme = isDark ? "Dark.xaml" : "Light.xaml";
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme." + theme)
            });
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialForms;component/Resources/TextStyles.xaml")
            });
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialForms;component/Resources/DialogViewResources.xaml")
            });

            InitializeComponent();
            CommandManager.AddExecutedHandler(this, CloseDialogCommand_Executed);
        }

        private Session Session => (Session)GetValue(SessionAssist.HostingSessionProperty);

        private void CloseDialogCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var action = e.Parameter as bool?;
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
