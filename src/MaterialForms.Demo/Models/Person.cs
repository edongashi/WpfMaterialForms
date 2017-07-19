using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo.Models
{
    [Form(Grid = new[] { 1d, 1d })]
    public class Person
    {
        [Field(Row = "1")]
        [Value(Must.NotBeEmpty)]
        public string FirstName { get; set; }

        [Field(Row = "1")]
        [Value("Length", Must.BeGreaterThanOrEqualTo, 5, Message = "Must have at least {Argument} characters.")]
        public string LastName { get; set; }
    }
}
