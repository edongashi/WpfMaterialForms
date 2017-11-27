namespace MaterialForms
{
    public static class WindowFactory
    {
        public static MaterialWindow Alert(string message)
        {
            return Alert(message, null);
        }

        public static MaterialWindow Alert(string message, string title)
        {
            return Alert(message, title, "OK");
        }

        public static MaterialWindow Alert(string message, string title, string action)
        {
            return new MaterialWindow(new MaterialDialog(message, title, action, null))
            {
                Width = 300d
            };
        }

        public static MaterialWindow Prompt(string message)
        {
            return Prompt(message, null);
        }

        public static MaterialWindow Prompt(string message, string title)
        {
            return Prompt(message, title, "OK");
        }

        public static MaterialWindow Prompt(string message, string title, string positiveAction)
        {
            return Prompt(message, title, positiveAction, "CANCEL");
        }

        public static MaterialWindow Prompt(string message, string title, string positiveAction, string negativeAction)
        {
            return new MaterialWindow(new MaterialDialog(message, title, positiveAction, negativeAction))
            {
                Width = 300d
            };
        }

        public static MaterialWindow FromSingleSchema(SchemaBase schema)
        {
            return FromSingleSchema(null, schema);
        }

        public static MaterialWindow FromSingleSchema(string message, SchemaBase schema)
        {
            return FromSingleSchema(message, null, schema);
        }

        public static MaterialWindow FromSingleSchema(string message, string title, SchemaBase schema)
        {
            return FromSingleSchema(message, title, "OK", schema);
        }

        public static MaterialWindow FromSingleSchema(string message, string title, string positiveAction,
            SchemaBase schema)
        {
            return FromSingleSchema(message, title, positiveAction, "CANCEL", schema);
        }

        public static MaterialWindow FromSingleSchema(string message, string title, string positiveAction,
            string negativeAction, SchemaBase schema)
        {
            return new MaterialWindow(new MaterialDialog(message, title, positiveAction, negativeAction,
                new MaterialForm(schema)));
        }
    }
}