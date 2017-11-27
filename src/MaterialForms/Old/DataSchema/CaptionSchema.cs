using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class CaptionSchema : SchemaBase
    {
        public override bool HoldsValue => false;

        public override UserControl CreateView()
        {
            return new CaptionControl {DataContext = this};
        }

        public override object GetValue()
        {
            return null;
        }
    }
}