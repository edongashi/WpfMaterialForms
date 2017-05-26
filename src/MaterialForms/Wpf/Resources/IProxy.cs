namespace MaterialForms.Wpf.Resources
{
    public interface IProxy
    {
        object Value { get; }
    }

    public interface IStringProxy
    {
        string Value { get; }
    }

    public interface IBoolProxy
    {
        bool Value { get; }
    }

    internal class PlainObject : IProxy
    {
        public PlainObject(object value)
        {
            Value = value;
        }

        public object Value { get; }
    }

    internal class PlainBool : IBoolProxy
    {
        public PlainBool(bool value)
        {
            Value = value;
        }

        public bool Value { get; }
    }

    internal class PlainString : IStringProxy
    {
        public PlainString(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
