using System;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class CommandArgs
    {
        public CommandArgs(CommandSchema sender, Session session, MaterialForm commandForm)
        {
            Sender = sender;
            Session = session;
            CommandForm = commandForm;
        }

        public CommandSchema Sender { get; }

        public Session Session { get; }

        public MaterialForm CommandForm { get; }
    }

    public class CommandSchema : SchemaBase
    {
        private string commandHint = "";
        private ICommand command;
        private MaterialForm form;

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

        public MaterialForm Form
        {
            get { return form; }
            set
            {
                if (Equals(value, form)) return;
                form = value;
                OnPropertyChanged();
            }
        }

        public Action<CommandArgs> Callback
        {
            set
            {
                Command = new DelegateCommand(arg => value(new CommandArgs(this, arg as Session, form)));
            }
        }

        public ICommand Command
        {
            get { return command; }
            private set
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

        public override bool HoldsValue => false;

        public override object GetValue() => null;
    }
}
