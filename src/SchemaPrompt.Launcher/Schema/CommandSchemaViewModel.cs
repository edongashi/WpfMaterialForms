namespace SchemaPrompt.Launcher.Schema
{
    public class CommandSchemaViewModel : BaseSchemaViewModel
    {
        private string commandHint = "Action";

        public string CommandHint
        {
            get { return commandHint; }
            set
            {
                if (value == commandHint) return;
                commandHint = value;
                OnPropertyChanged();
            }
        }
    }
}
