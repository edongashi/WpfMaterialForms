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
        [Field(
            Name = "First Name",
            Tooltip = "Enter your first name here.")]
        public string FirstName { get; set; }

        [Field(
            Name = "Last Name",
            Tooltip = "Enter your last name here.")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Field(
            Name = "Username",
            Tooltip = "Enter your first name here.")]
        public string Username { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }
    }
}
