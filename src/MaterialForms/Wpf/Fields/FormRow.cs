using System.Collections.Generic;

namespace MaterialForms.Wpf.Fields
{
    public class FormRow
    {
        public FormRow(FormElement singleElement)
            : this(true, 1)
        {
            Elements.Add(new FormElementContainer(0, 1, singleElement));
        }

        public FormRow() 
            : this(true, 1)
        {
        }

        public FormRow(bool startsNewRow, int rowSpan)
        {
            StartsNewRow = startsNewRow;
            RowSpan = rowSpan;
            Elements = new List<FormElementContainer>();
        }

        public bool StartsNewRow { get; }

        public int RowSpan { get; }

        public List<FormElementContainer> Elements { get; }
    }

    public class FormElementContainer
    {
        public FormElementContainer(int column, int columnSpan, FormElement element) 
            : this(column, columnSpan, new List<FormElement> { element })
        {
        }

        public FormElementContainer(int column, int columnSpan, List<FormElement> elements)
        {
            Column = column;
            ColumnSpan = columnSpan;
            Elements = elements;
        }

        public int Column { get; }

        public int ColumnSpan { get; }

        public List<FormElement> Elements { get; }
    }

    public enum FormElementsAlignment
    {
        Stretch,
        Left,
        Right
    }
}
