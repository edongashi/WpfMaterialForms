using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Demo.Models
{
    [Title("Login to continue")]
    [Action("cancel", "CANCEL")]
    [Action("login", "LOG IN")]
    public class Login
    {
        // Enums may be deserialized from strings.
        [Field(Icon = "Account")]
        public string Username { get; set; }

        // Or be dynamically assigned...
        [Field(Icon = "{Property PasswordIcon}")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public PackIconKind PasswordIcon => PackIconKind.Key;

        public override string ToString()
        {
            return "Login";
        }
    }
}
