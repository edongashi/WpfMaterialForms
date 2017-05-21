using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public abstract class Resource : IEquatable<Resource>
    {
        protected Resource(IValueConverter valueConverter)
        {
            ValueConverter = valueConverter;
        }

        public IValueConverter ValueConverter { get; }

        public abstract bool IsDynamic { get; }

        public abstract bool Equals(Resource other);

        public abstract BindingBase GetBinding(FrameworkElement element);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Resource)obj);
        }

        public abstract override int GetHashCode();
    }
}
