using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MaterialForms.Wpf.Fields
{
    /// <summary>
    /// Markup extension for creating deferred bindings.
    /// </summary>
    public class FormBindingExtension : MarkupExtension
    {
        [ConstructorArgument("name")]
        public string Name { get; set; }

        public FormBindingExtension()
        {
        }

        public FormBindingExtension(string name)
        {
            Name = name;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var pvt = serviceProvider as IProvideValueTarget;
            if (pvt?.TargetObject == null)
            {
                return null;
            }
            
            if (pvt.TargetObject is FrameworkElement frameworkElement)
            {
                var field = frameworkElement.DataContext as IBindingProvider;
                if (field == null)
                {
                    return null;
                }

                var value = field.ProvideValue(Name);
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

            if (pvt.TargetObject is TriggerBase || pvt.TargetObject is SetterBase)
            {
                if (pvt.TargetProperty is DependencyProperty dp2 && dp2.PropertyType == typeof(BindingBase)
                    || pvt.TargetProperty is PropertyInfo p2 && p2.PropertyType == typeof(BindingBase))
                {
                    return new Binding($"[{Name}].Value");
                }

                return new Binding($"[{Name}].Value").ProvideValue(serviceProvider);
            }

            return this;
        }
    }
}
