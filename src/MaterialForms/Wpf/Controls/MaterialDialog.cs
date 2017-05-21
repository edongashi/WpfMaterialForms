using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MaterialForms.Annotations;

namespace MaterialForms.Wpf.Controls
{
    public abstract class MaterialDialog : ContentControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
