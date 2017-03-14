using System.ComponentModel;
using System.Windows.Controls;

namespace MaterialForms
{
    public interface IViewProvider : INotifyPropertyChanged
    {
        UserControl View { get; }
    }
}
