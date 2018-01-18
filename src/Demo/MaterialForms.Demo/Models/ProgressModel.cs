using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Annotations.Display;

namespace MaterialForms.Demo.Models
{
    public class ProgressModel
    {
        [Text("Loading...")]

        [Field, Progress]
        public double Progress => 60d;
    }
}
