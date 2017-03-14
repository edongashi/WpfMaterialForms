namespace MaterialForms
{
    public static class DialogFactory
    {
        public static MaterialDialog Alert(string message) => Alert(message, null);

        public static MaterialDialog Alert(string message, string title) => Alert(message, title, "OK");

        public static MaterialDialog Alert(string message, string title, string action)
        {
            return new MaterialDialog(message, title, action, null);
        }

        public static MaterialDialog Prompt(string message) => Prompt(message, null);

        public static MaterialDialog Prompt(string message, string title) => Prompt(message, title, "OK");

        public static MaterialDialog Prompt(string message, string title, string positiveAction) => Prompt(message, title, positiveAction, "CANCEL");

        public static MaterialDialog Prompt(string message, string title, string positiveAction, string negativeAction)
        {
            return new MaterialDialog(message, title, positiveAction, negativeAction);
        }

        public static MaterialDialog FromSingleSchema(SchemaBase schema) => FromSingleSchema(null, schema);

        public static MaterialDialog FromSingleSchema(string message, SchemaBase schema) => FromSingleSchema(message, null, schema);

        public static MaterialDialog FromSingleSchema(string message, string title, SchemaBase schema) => FromSingleSchema(message, title, "OK", schema);

        public static MaterialDialog FromSingleSchema(string message, string title, string positiveAction, SchemaBase schema) => FromSingleSchema(message, title, positiveAction, "CANCEL", schema);

        public static MaterialDialog FromSingleSchema(string message, string title, string positiveAction, string negativeAction, SchemaBase schema)
        {
            return new MaterialDialog(message, title, positiveAction, negativeAction, new MaterialForm(schema));
        }
    }
}
