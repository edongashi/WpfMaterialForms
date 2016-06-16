using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace MaterialForms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MaterialFormsWindow : MetroWindow
    {
        public MaterialFormsWindow(MaterialWindow dataContext, int dialogId = 0)
        {
            DataContext = dataContext;
            InitializeComponent();
            DialogHost.Identifier = "DialogHost" + dialogId;
        }

        private void CloseDialogCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void CloseDialogCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
