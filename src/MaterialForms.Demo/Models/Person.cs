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
        [Value("Length", Must.BeGreaterThan, 5)]
        public string LastName { get; set; }
    }
}
