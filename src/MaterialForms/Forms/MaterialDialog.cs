using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialForms.Annotations;
using MaterialForms.Views;

namespace MaterialForms
{
    public class MaterialDialog : IViewProvider
    {
        private string title;
        private string message;
        private MaterialForm form;
        private string negativeAction;
        private string positiveAction;
        private string auxiliaryAction;

        public MaterialDialog()
            : this(message: null)
        {
        }

        public MaterialDialog(params SchemaBase[] schemas)
            : this(new MaterialForm(schemas))
        {
        }

        public MaterialDialog(MaterialForm form)
            : this(null, form)
        {
        }

        public MaterialDialog(string message)
            : this(message, title: null)
        {
        }

        public MaterialDialog(string message, params SchemaBase[] schemas)
            : this(message, new MaterialForm(schemas))
        {
        }

        public MaterialDialog(string message, MaterialForm form)
            : this(message, null, form)
        {
        }

        public MaterialDialog(string message, string title)
            : this(message, title, form: null)
        {
        }

        public MaterialDialog(string message, string title, params SchemaBase[] schemas)
            : this(message, title, new MaterialForm(schemas))
        {
        }

        public MaterialDialog(string message, string title, MaterialForm form)
            : this(message, title, "OK", "CANCEL", form)
        {
        }

        public MaterialDialog(string message, string title, string positiveAction, string negativeAction)
            : this(message, title, positiveAction, negativeAction, null)
        {
        }

        public MaterialDialog(string message, string title, string positiveAction, string negativeAction, MaterialForm form)
            : this(message, title, positiveAction, negativeAction, null, form)
        {
        }

        public MaterialDialog(string message, string title, string positiveAction, string negativeAction, string auxiliaryAction, MaterialForm form)
        {
            Message = message;
            Title = title;
            PositiveAction = positiveAction;
            NegativeAction = negativeAction;
            AuxiliaryAction = auxiliaryAction;
            Form = form;
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (value == title) return;
                title = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                if (value == message) return;
                message = value;
                OnPropertyChanged();
            }
        }

        public string PositiveAction
        {
            get { return positiveAction; }
            set
            {
                if (value == positiveAction) return;
                positiveAction = value;
                OnPropertyChanged();
            }
        }

        public Func<Session, Task> OnPositiveAction { get; set; }

        public string NegativeAction
        {
            get { return negativeAction; }
            set
            {
                if (value == negativeAction) return;
                negativeAction = value;
                OnPropertyChanged();
            }
        }

        public Func<Session, Task> OnNegativeAction { get; set; }

        public string AuxiliaryAction
        {
            get { return auxiliaryAction; }
            set
            {
                if (value == auxiliaryAction) return;
                auxiliaryAction = value;
                OnPropertyChanged();
            }
        }

        public Func<Session, Task> OnAuxiliaryAction { get; set; }

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

        public UserControl View => new DialogView { DataContext = this };

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
