using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf.Resources
{
    public abstract class Resource : IEquatable<Resource>
    {
        public abstract bool IsDynamic { get; }

        public abstract BindingBase GetBinding(FrameworkElement element);

        public abstract bool Equals(Resource other);

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

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Resource)obj);
        }

        public abstract override int GetHashCode();
    }
}
