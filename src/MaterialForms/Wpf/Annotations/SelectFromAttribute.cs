using System;

namespace MaterialForms.Wpf.Annotations
{
    public class SelectFromAttribute : Attribute
    {
        public SelectFromAttribute(object itemsSource)
        {
            ItemsSource = itemsSource;
        }

        public object ItemsSource { get; set; }

        public string DisplayPath { get; set; }

        public string ValuePath { get; set; }

        public string ItemStringFormat { get; set; }

        public SelectionType Type { get; set; }
    }
}