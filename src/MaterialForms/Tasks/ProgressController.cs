namespace MaterialForms.Tasks
{
    public class ProgressController
    {
        internal readonly MaterialDialog Dialog;
        internal readonly ProgressSchema Schema;

        private readonly DialogActionListener listener;

        internal ProgressController(ProgressDialogOptions options)
        {
            Schema = new ProgressSchema
            {
                Progress = options.Progress,
                Maximum = options.Maximum,
                IsIndeterminate = options.IsIndeterminate
            };

            Dialog = new MaterialDialog
            {
                Title = options.Title,
                Message = options.Message,
                PositiveAction = null,
                NegativeAction = options.Cancel,
                Form = Schema
            };

            if (!string.IsNullOrEmpty(options.Cancel))
            {
                listener = new DialogActionListener(Dialog, "negative");
            }
        }

        public string Message
        {
            get { return Dialog.Message; }
            set { Dialog.Message = value; }
        }

        public double Progress
        {
            get { return Schema.Progress; }
            set { Schema.Progress = value; }
        }

        public int Maximum
        {
            get { return Schema.Maximum; }
            set { Schema.Maximum = value; }
        }

        public bool CancellationRequested => listener.ActionPerformed;
    }
}