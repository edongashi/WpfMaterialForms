using System;

namespace MaterialForms.Wpf.Annotations
{
    public sealed class SelectFromAttribute : FieldTypeAttribute
    {
        public SelectFromAttribute(object itemsSource)
        {
            ItemsSource = itemsSource;
        }

        public object ItemsSource { get; set; }

        public string DisplayPath { get; set; }

        public string ValuePath { get; set; }

        public string ItemStringFormat { get; set; }

        public object SelectionType { get; set; }
    }
}
