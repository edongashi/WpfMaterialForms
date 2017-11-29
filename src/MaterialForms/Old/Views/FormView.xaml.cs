using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaterialForms.Views
{
    /// <summary>
    ///     Interaction logic for FormView.xaml
    /// </summary>
    public partial class FormView : UserControl
    {
        public FormView()
        {
            InitializeComponent();
        }


        private void FormView_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = DataContext as MaterialForm;
                if (form == null)
                    return;

                if (form.FocusedSchema < 0)
                    return;

                if (ItemsControl.ItemContainerGenerator.Items.Count > form.FocusedSchema)
                {
                    var element =
                        ItemsControl.ItemContainerGenerator.ContainerFromIndex(form.FocusedSchema) as IInputElement;
                    if (element == null)
                        return;

                    FocusManager.SetFocusedElement(this, element);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}