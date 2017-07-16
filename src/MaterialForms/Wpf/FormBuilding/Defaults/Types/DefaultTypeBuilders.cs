using System;
using System.Reflection;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Defaults;

namespace MaterialForms.Wpf.FormBuilding.Defaults.Types
{
    internal class StringFieldBuilder : TypeBuilder<String> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            return new StringField(property.Name);
        }
    }

    internal class DateTimeFieldBuilder : TypeBuilder<DateTime> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class BooleanFieldBuilder : TypeBuilder<Boolean> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class CharFieldBuilder : TypeBuilder<Char> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class ByteFieldBuilder : TypeBuilder<Byte> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class SByteFieldBuilder : TypeBuilder<SByte> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class Int16FieldBuilder : TypeBuilder<Int16> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class Int32FieldBuilder : TypeBuilder<Int32> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class Int64FieldBuilder : TypeBuilder<Int64> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class UInt16FieldBuilder : TypeBuilder<UInt16> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class UInt32FieldBuilder : TypeBuilder<UInt32> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class UInt64FieldBuilder : TypeBuilder<UInt64> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class SingleFieldBuilder : TypeBuilder<Single> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class DoubleFieldBuilder : TypeBuilder<Double> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class DecimalFieldBuilder : TypeBuilder<Decimal> {
        protected override FormElement Build(PropertyInfo property, Func<string, object> deserializer)
        {
            throw new NotImplementedException();
        }
    }

}
