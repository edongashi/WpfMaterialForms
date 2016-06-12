using System.Windows.Controls;
using SchemaPrompt.Launcher.Controls;
using SchemaPrompt.Launcher.ViewModels;

namespace SchemaPrompt.Launcher.ViewModels
{
    internal class CommandSchemaViewModel : BaseSchemaViewModel
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

        public override UserControl CreateView()
        {
            return new ButtonControl
            {
                DataContext = this
            };
        }
    }
}
