using System;
using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaterialForms.Tests
{
    [TestClass]
    public class AnnotationTests
    {
    }

    public class User
    {
        [Field(Name = "First Name",
            Tooltip = "Enter your first name here.",
            Icon = PackIconKind.Account)]
        [Value(Must.NotBeEmpty)]
        public string FirstName { get; set; }

        [Field(Name = "Last Name",
            Tooltip = "Enter your last name here.")]
        [Value(Must.NotBeEmpty)]
        public string LastName { get; set; }

        [Value(Must.BeLessThan, "2020-01-01",
            Message = "You said you are born in the year {Value:yyyy}. Are you really from the future?")]
        public DateTime DateOfBirth { get; set; }

        [Field(Name = "Username")]
        [Value(Must.MatchPattern, "^[a-Z][a-Z0-9]*$",
            Message = "{Value} is not a valid username, usernames must match pattern {Argument}.")]
        [Value(Must.NotExistIn, "{ContextBinding Users}",
            Message = "User {Value} is already taken.")]
        public string Username { get; set; }

        [Value("Length", Must.BeGreaterThan, 6,
            Message = "Your password has {Value|Length} characters, which is less than the required {Argument}.")]
        [Value("Length", Must.BeGreaterThan, 12,
            When = "{ContextBinding RequireLongPasswords}",
            Message = "The administrator decided that your password must be really long!")]
        public string Password { get; set; }

        [Value(Must.BeEqualTo, "{Binding Password}",
            Message = "The entered passwords do not match.")]
        public string PasswordConfirm { get; set; }

        [Value(Must.BeTrue, Message = "You must accept the license agreement.")]
        public bool AgreeToLicense { get; set; }
    }
}
