using System.ComponentModel;
using System.Windows.Controls;

namespace MaterialForms
{
    internal interface IViewProvider : INotifyPropertyChanged
    {
        UserControl View { get; }
    }
}
