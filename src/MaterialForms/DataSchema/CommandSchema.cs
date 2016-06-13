using System;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class CommandSchema : SchemaBase
    {
        private string commandHint = "Action";
        private ICommand command;

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

        public Action<object> Callback
        {
            set
            {
                Command = new DelegateCommand(value);
            }
        }

        public ICommand Command
        {
            get { return command; }
            set
            {
                if (Equals(value, command)) return;
                command = value;
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
