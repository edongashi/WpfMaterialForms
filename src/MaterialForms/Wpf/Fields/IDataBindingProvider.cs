using System.Collections.Generic;
using System.Windows.Data;

namespace MaterialForms.Wpf.Fields
{
    public interface IDataBindingProvider : IBindingProvider
    {
        IEnumerable<BindingExpressionBase> GetBindings();

        void ClearBindings();
    }
}