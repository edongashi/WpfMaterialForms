using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MaterialForms.Annotations;
using MaterialForms.Views;

namespace MaterialForms
{
    public class MaterialDialog : IViewProvider
    {
        private string title;
        private string subheading;
        private MaterialForm form;
        private string negativeAction = "CANCEL";
        private string positiveAction = "OK";
        private string auxiliaryAction;

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

        public string Subheading
        {
            get { return subheading; }
            set
            {
                if (value == subheading) return;
                subheading = value;
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

        public UserControl View => new DialogView {DataContext = this};

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
