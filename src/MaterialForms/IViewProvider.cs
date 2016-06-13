using System.ComponentModel;
using System.Windows.Controls;

namespace MaterialForms
{
    interface IViewProvider : INotifyPropertyChanged
    {
        UserControl View { get; }
    }
}
