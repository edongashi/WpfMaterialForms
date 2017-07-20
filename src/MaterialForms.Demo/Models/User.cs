using System;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo.Models
{
    [Form(Grid = new[] { 1d, 1d })]
    public class User
    {
        [Field(Name = "First Name",
            ToolTip = "Enter your first name here.", Row = "1",
            Icon = PackIconKind.Pencil)]
        [Value(Must.NotBeEmpty)]
        public string FirstName { get; set; }

        [Field(Name = "Last Name", Row = "1",
            ToolTip = "Enter your last name here.")]
        [Value(Must.NotBeEmpty)]
        public string LastName { get; set; }

        [Field(Icon = PackIconKind.Calendar)]
        [Value(Must.BeLessThan, "2020-01-01",
            Message = "You said you are born in the year {Value:yyyy}. Are you really from the future?")]
        public DateTime? DateOfBirth { get; set; }

        [Field(Name = "Username",
            Icon = PackIconKind.Account)]
        [Value(Must.MatchPattern, "^[a-zA-Z][a-zA-Z0-9]*$",
            Message = "{Value} is not a valid username, usernames must match pattern {Argument}.")]
        //[Value(Must.NotExistIn, "{ContextBinding Users}",
        //    Message = "User {Value} is already taken.")]
        public string Username { get; set; }

        [Field(Icon = PackIconKind.Key)]
        [Value("Length", Must.BeGreaterThanOrEqualTo, 6,
            Message = "Your password has {Value|Length} characters, which is less than the required {Argument}.")]
        //[Value("Length", Must.BeGreaterThan, 12,
        //    When = "{ContextBinding RequireLongPasswords}",
        //    Message = "The administrator decided that your password must be really long!")]
        public string Password { get; set; }

        [Field(Icon = "Empty")]
        [Value(Must.BeEqualTo, "{Binding Password}",
            Message = "The entered passwords do not match.")]
        public string PasswordConfirm { get; set; }

        [Field(Icon = "Empty")]
        [Value(Must.BeTrue, Message = "You must accept the license agreement.")]
        public bool AgreeToLicense { get; set; }

        // Needed for ListBox display.
        public override string ToString()
        {
            return "User";
        }
    }
}
