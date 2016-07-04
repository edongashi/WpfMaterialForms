namespace MaterialForms.Tasks
{
    public class ProgressDialogOptions
    {
        public string Message { get; set; } = null;

        public string Title { get; set; } = null;

        public string Cancel { get; set; } = null;

        public double Progress { get; set; } = 0d;

        public int Maximum { get; set; } = 100;

        public bool IsIndeterminate { get; set; } = false;
    }
}