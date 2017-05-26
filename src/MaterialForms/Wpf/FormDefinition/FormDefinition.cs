using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf
{
    public class FormDefinition
    {
        public object CreateInstance()
        {
            return null;
        }

        public List<FormElement> FormElements { get; set; }
    }

    public class ModelDefinition
    {
        
    }
}
