using System.Collections.ObjectModel;
using System.Windows.Controls;
using MaterialForms.Views;

namespace MaterialForms
{
    public class MaterialForm : ObservableCollection<SchemaBase>
    {
        public UserControl View => new FormView { DataContext = this };
    }
}
