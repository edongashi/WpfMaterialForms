using System;
using System.Reflection;
using MaterialForms.Wpf.Fields;

namespace MaterialForms.Wpf
{
    internal class PropertyContext
    {
        private FormBuilder formBuilder ;

        private Type[] parentTypes ;

        private PropertyInfo property ;

        public PropertyContext(FormBuilder formBuilder, Type[] parentTypes, PropertyInfo property)
        {
            this.formBuilder = formBuilder;
            this.parentTypes = parentTypes;
            this.property = property;
        }
    }

    internal class FieldContext : PropertyContext
    {
        private FormElement formElement ;

        public FieldContext(FormBuilder formBuilder, Type[] parentTypes, PropertyInfo property, FormElement formElement)
            : base(formBuilder, parentTypes, property)
        {
            this.formElement = formElement;
        }
    }


    public class ForPropertySyntax
    {
        private readonly FormBuilder formBuilder;

        private readonly Type type;

        public ForPropertySyntax(FormBuilder formBuilder, Type type)
        {
            this.formBuilder = formBuilder;
            this.type = type;
        }

        public WithAttributeSyntax WithAttribute<TAttribute>() where TAttribute : Attribute
        {
            return new WithAttributeSyntax(formBuilder, type, typeof(TAttribute));
        }

        public object UseFactory(Func<PropertyContext, FormElement> factory)
        {
            return null;
        }

        public object UseInitializer(Action<FieldContext> initializer)
        {
            return null;
        }
    }

    public class WithAttributeSyntax
    {
        private readonly FormBuilder formBuilder;
        private readonly Type propertyType;
        private readonly Type attributeType;

        public WithAttributeSyntax(FormBuilder formBuilder, Type propertyType, Type attributeType)
        {
            this.formBuilder = formBuilder;
            this.propertyType = propertyType;
            this.attributeType = attributeType;
        }
    }
}
