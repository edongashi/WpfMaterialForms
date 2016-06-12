namespace SchemaPrompt.Launcher.Schema
{
    public class BooleanSchemaViewModel : BaseSchemaViewModel
    {
        private bool value;

        public bool Value
        {
            get { return value; }
            set
            {
                if (value == this.value) return;
                this.value = value;
                OnPropertyChanged();
            }
        }
    }
}
