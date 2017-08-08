using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MaterialForms.Annotations;

namespace MaterialForms
{
    public class MaterialWindow : INotifyPropertyChanged
    {
        private string title = "Dialog";
        private double width = 400d;
        private double height = double.NaN;
        private bool showMinButton;
        private bool showMaxRestoreButton = true;
        private bool showCloseButton;
        private bool canResize;
        private MaterialDialog dialog;

        public MaterialWindow()
        {
        }

        public MaterialWindow(MaterialDialog dialog)
        {
            Dialog = dialog;
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

        public double Width
        {
            get { return width; }
            set
            {
                if (value.Equals(width)) return;
                width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                if (value.Equals(height)) return;
                height = value;
                OnPropertyChanged();
            }
        }

        public bool ShowMinButton
        {
            get { return showMinButton; }
            set
            {
                if (value == showMinButton) return;
                showMinButton = value;
                OnPropertyChanged();
            }
        }

        public bool ShowMaxRestoreButton
        {
            get { return showMaxRestoreButton; }
            set
            {
                if (value == showMaxRestoreButton) return;
                showMaxRestoreButton = value;
                OnPropertyChanged();
            }
        }

        public bool ShowCloseButton
        {
            get { return showCloseButton; }
            set
            {
                if (value == showCloseButton) return;
                showCloseButton = value;
                OnPropertyChanged();
            }
        }

        public bool CanResize
        {
            get { return canResize; }
            set
            {
                if (value == canResize) return;
                canResize = value;
                OnPropertyChanged();
            }
        }

        public MaterialDialog Dialog
        {
            get { return dialog; }
            set
            {
                if (Equals(value, dialog)) return;
                dialog = value;
                OnPropertyChanged();
            }
        }

        public Task<bool?> Show() => Show(MaterialApplication.DefaultDispatcher);

        public Task<bool?> Show(DispatcherOption dispatcherOption) => ShowTracked(dispatcherOption).Task;

        public WindowSession ShowTracked() => ShowTracked(MaterialApplication.DefaultDispatcher);

        public WindowSession ShowTracked(DispatcherOption dispatcherOption)
        {
            return new WindowSession(this, MaterialApplication.GetDispatcher(dispatcherOption)).Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
