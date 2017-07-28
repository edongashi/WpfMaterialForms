using System.ComponentModel;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf.Controls
{
    internal class FormContentPresenter : INotifyPropertyChanged
    {
        public FormContentPresenter(int row, int column, int columnSpan, IBindingProvider bindingProvider)
        {
            Row = row;
            Column = column;
            ColumnSpan = columnSpan;
            BindingProvider = bindingProvider;
        }

        public int Row { get; }

        public int Column { get; }

        public int ColumnSpan { get; }

        public IBindingProvider BindingProvider { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
