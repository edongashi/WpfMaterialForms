using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class PasswordSchema : SchemaBase
    {
        public string Value { get; set; }

        //public string GetPassword()
        //{
        //    // TODO: request from control
        //    return null;
        //}

        public override UserControl CreateView()
        {
            return new PasswordTextControl { DataContext = this };
        }

        public override bool HoldsValue => true;

        public override object GetValue() => Value;
    }
}
