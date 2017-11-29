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
        private ICommand command;
        private string commandHint = "";
        private MaterialForm form;

        public string CommandHint
        {
            get => commandHint;
            set
            {
                if (value == commandHint) return;
                commandHint = value;
                OnPropertyChanged();
            }
        }

        public MaterialForm Form
        {
            get => form;
            set
            {
                if (Equals(value, form)) return;
                form = value;
                OnPropertyChanged();
            }
        }

        public Action<CommandArgs> Callback
        {
            set { Command = new DelegateCommand(arg => value(new CommandArgs(this, arg as Session, form))); }
        }

        public ICommand Command
        {
            get => command;
            private set
            {
                if (Equals(value, command)) return;
                command = value;
                OnPropertyChanged();
            }
        }

        public override bool HoldsValue => false;

        public override UserControl CreateView()
        {
            return new ButtonControl
            {
                DataContext = this
            };
        }

        public override object GetValue()
        {
            return null;
        }
    }
}