using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo.Models
{
    public class Person
    {
        [Value(Must.NotBeEmpty)]
        public string FirstName { get; set; }

        [Value("Length", Must.BeGreaterThanOrEqualTo, 5, Message = "Must have at least {Argument} characters.")]
        public string LastName { get; set; }

        [Field(Icon = PackIconKind.Account)]
        [Value(Must.NotBeEmpty)]
        public string Username { get; set; }

        // Needed for ListBox display.
        public override string ToString()
        {
            return "Person";
        }
    }
}
