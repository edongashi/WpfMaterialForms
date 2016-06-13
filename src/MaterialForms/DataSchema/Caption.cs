using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class CaptionSchema : SchemaBase
    {
        public override UserControl CreateView()
        {
            return new CaptionControl { DataContext = this };
        }
    }
}
