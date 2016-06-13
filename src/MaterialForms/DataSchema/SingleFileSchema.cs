using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class SingleFileSchema : SchemaBase
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
