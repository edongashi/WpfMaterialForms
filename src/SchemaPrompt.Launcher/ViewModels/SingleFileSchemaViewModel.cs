using System.Windows.Controls;
using SchemaPrompt.Launcher.Controls;
using SchemaPrompt.Launcher.ViewModels;

namespace SchemaPrompt.Launcher.ViewModels
{
    internal class SingleFileSchemaViewModel : BaseSchemaViewModel
    {
        private string path;

        public string Path
        {
            get { return path; }
            set
            {
                if (value == path) return;
                path = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new FileLoaderControl()
            {
                DataContext = this
            };
        }
    }
}
