using System;
using System.Globalization;
using System.Reflection;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Defaults;
using Display = MaterialForms.Wpf.Annotations.Display;

namespace MaterialForms.Wpf.FormBuilding.Defaults.Types
{
    internal class StringFieldBuilder : TypeBuilder<String> {
        protected override FormElement Build(IFormProperty property, Func<string, object> deserializer)
        {
            var maskAttr = property.GetCustomAttribute<MaskedAttribute>();
            if (property.GetCustomAttribute<MaskedAttribute>() != null)
            {
                return new StringField(property.Name, maskAttr.Mask);
            }
            else
            {
                return new StringField(property.Name);
            }
        }
    }

    internal class BooleanFieldBuilder : IFieldBuilder {
        public FormElement TryBuild(IFormProperty property, Func<string, object> deserializer)
        {
            var isSwitch = property.GetCustomAttribute<Display.ToggleAttribute>() != null;
            return new BooleanField(property.Name)
            {
                IsSwitch = isSwitch
            };
        }
    }

    internal class DateTimeFieldBuilder : IFieldBuilder
    {
        public FormElement TryBuild(IFormProperty property, Func<string, object> deserializer)
        {
            return new DateField(property.Name);
        }
    }

    internal class ConvertedFieldBuilder : IFieldBuilder
    {
        public ConvertedFieldBuilder(Func<string, CultureInfo, object> deserializer)
        {
            Deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }

        public Func<string, CultureInfo, object> Deserializer { get; }

        public FormElement TryBuild(IFormProperty property, Func<string, object> deserializer)
        {
            return new ConvertedField(property.Name, property.PropertyType, Deserializer);
        }
    }

    //internal class ByteFieldBuilder : TypeBuilder<Byte> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class SByteFieldBuilder : TypeBuilder<SByte> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class Int16FieldBuilder : TypeBuilder<Int16> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class Int32FieldBuilder : TypeBuilder<Int32> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class Int64FieldBuilder : TypeBuilder<Int64> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class UInt16FieldBuilder : TypeBuilder<UInt16> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class UInt32FieldBuilder : TypeBuilder<UInt32> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class UInt64FieldBuilder : TypeBuilder<UInt64> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class SingleFieldBuilder : TypeBuilder<Single> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class DoubleFieldBuilder : TypeBuilder<Double> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class DecimalFieldBuilder : TypeBuilder<Decimal> {
    //    protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}
