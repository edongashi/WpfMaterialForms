namespace MaterialForms
{
    public static class Validators
    {
        public static string IsNotEmpty(string value)
        {
            return string.IsNullOrEmpty(value) ? "Value is required." : null;
        }
    }
}
