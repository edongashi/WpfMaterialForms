namespace MaterialForms
{
    public static class WindowFactory
    {
        public static MaterialWindow Alert(string message) => Alert(message, null);

        public static MaterialWindow Alert(string message, string title) => Alert(message, title, "OK");

        public static MaterialWindow Alert(string message, string title, string action)
        {
            return new MaterialWindow(new MaterialDialog(message, title, action, null))
            {
                Width = 300d
            };
        }

        public static MaterialWindow Prompt(string message) => Prompt(message, null);

        public static MaterialWindow Prompt(string message, string title) => Prompt(message, title, "OK");

        public static MaterialWindow Prompt(string message, string title, string positiveAction) => Prompt(message, title, positiveAction, "CANCEL");

        public static MaterialWindow Prompt(string message, string title, string positiveAction, string negativeAction)
        {
            return new MaterialWindow(new MaterialDialog(message, title, positiveAction, negativeAction))
            {
                Width = 300d
            };
        }
    }
}
