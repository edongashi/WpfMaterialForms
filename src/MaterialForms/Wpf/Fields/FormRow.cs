using System.Collections.Generic;

namespace MaterialForms.Wpf.Fields
{
    public class FormRow
    {
        public FormRow()
        {
            Elements = new List<FormElementContainer>();
        }

        public List<FormElementContainer> Elements { get; }
    }

    public class FormElementContainer
    {
        public FormElementContainer(int column, int columnSpan, FormElement element)
        {
            Column = column;
            ColumnSpan = columnSpan;
            Element = element;
        }

        public int Column { get; }

        public int ColumnSpan { get; }

        public FormElement Element { get; }
    }
}
