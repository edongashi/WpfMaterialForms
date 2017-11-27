using System.IO;

namespace MaterialForms
{
    public static class Validators
    {
        public static string IsNotEmpty(string value)
        {
            return string.IsNullOrEmpty(value) ? "Value is required." : null;
        }

        public static string RequiredFile(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "Value is required.";

            return !File.Exists(value) ? "File does not exist." : null;
        }

        public static string OptionalFile(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return !File.Exists(value) ? "File does not exist." : null;
        }
    }
}