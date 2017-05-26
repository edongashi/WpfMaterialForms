using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf.Controls
{
    [MarkupExtensionReturnType(typeof(object))]
    public class FormBindingExtension : MarkupExtension
    {
        [ConstructorArgument("path")]
        public string Path { get; set; }

        public FormBindingExtension()
        {
        }

        public FormBindingExtension(string path)
        {
            Path = path;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var pvt = serviceProvider as IProvideValueTarget;
            if (pvt == null)
            {
                return null;
            }

            var frameworkElement = pvt.TargetObject as FrameworkElement;
            if (frameworkElement == null)
            {
                return this;
            }

            var field = frameworkElement.DataContext as IFieldValueProvider;
            if (field == null)
            {
                throw new InvalidOperationException("No suitable DataContext exists to provide form resources.");
            }

            var value = field.ProvideValue(Path);
            if (value is BindingBase binding)
            {
                if (pvt.TargetProperty is DependencyProperty dp && dp.PropertyType == typeof(BindingBase)
                    || pvt.TargetProperty is PropertyInfo p && p.PropertyType == typeof(BindingBase))
                {
                    return binding;
                }

                return binding.ProvideValue(serviceProvider);
            }

            return value;
        }
    }
}
