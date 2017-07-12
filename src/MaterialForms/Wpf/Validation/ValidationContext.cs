using System.Globalization;

namespace MaterialForms.Wpf.Validation
{
    public class ValidationContext
    {
        public ValidationContext(object model, object context, string propertyName, object propertyValue, CultureInfo cultureInfo)
        {
            Model = model;
            Context = context;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            CultureInfo = cultureInfo;
        }

        public object Model { get; }

        public object Context { get; }

        public string PropertyName { get; }

        public object PropertyValue { get; }

        public CultureInfo CultureInfo { get; }
    }
}
