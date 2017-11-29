using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace MaterialForms.Controls
{
    /// <summary>
    ///     Interaction logic for SingleLineTextControl.xaml
    /// </summary>
    public partial class FileLoaderControl : UserControl
    {
        public FileLoaderControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as SingleFileSchema;
            if (context == null)
                return;

            var dialog = new OpenFileDialog();
            if (context.Filter != null)
                dialog.Filter = context.Filter;

            if (dialog.ShowDialog() == true)
            {
                var fileName = dialog.FileName;
                context.Value = fileName;
            }
        }
    }
}