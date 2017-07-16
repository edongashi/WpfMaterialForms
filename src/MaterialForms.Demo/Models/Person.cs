using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo.Models
{
    public class Person
    {
        [Field(Icon = PackIconKind.Account)]
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
