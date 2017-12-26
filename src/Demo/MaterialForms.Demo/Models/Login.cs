using MaterialDesignThemes.Wpf;
using MaterialForms.Wpf;
using MaterialForms.Wpf.Annotations;
using MaterialForms.Wpf.Annotations.Display;

namespace MaterialForms.Demo.Models
{
    [Title("Login to continue")]
    [Action("cancel", "CANCEL")]
    [Action("login", "LOG IN")]
    public class Login : IActionHandler
    {
        // Enums may be deserialized from strings.
        [Field(Icon = "Account")]
        public string Username { get; set; }

        // Or be dynamically assigned...
        [Field(Icon = "{Property PasswordIcon}")]
        [Password]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public PackIconKind PasswordIcon => PackIconKind.Key;

        /// <inheritdoc />
        public void HandleAction(object model, string action, object parameter)
        {
            
        }
    }
}
