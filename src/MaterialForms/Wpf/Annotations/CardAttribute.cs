using System.Runtime.CompilerServices;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Defaults;

namespace MaterialForms.Wpf.Annotations
{
    public sealed class CardAttribute : FormContentAttribute
    {
        public CardAttribute(int rows, [CallerLineNumber] int position = 0)
            : base(position)
        {
            StartsNewRow = false;
            RowSpan = rows;
        }

        protected override FormElement CreateElement()
        {
            return new CardElement();
        }
    }
}