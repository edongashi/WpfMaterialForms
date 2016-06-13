using System.Windows;
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
    }
}
