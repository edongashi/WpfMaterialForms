using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MaterialForms.Annotations;

namespace MaterialForms
{
    public class MaterialWindow : INotifyPropertyChanged
    {
        private bool canResize;
        private MaterialDialog dialog;
        private double height = double.NaN;
        private bool showCloseButton;
        private bool showMaxRestoreButton = true;
        private bool showMinButton;
        private string title = "Dialog";
        private double width = 400d;

        public MaterialWindow()
        {
        }

        public MaterialWindow(MaterialDialog dialog)
        {
            Dialog = dialog;
        }

        public string Title
        {
            get => title;
            set
            {
                if (value == title) return;
                title = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => width;
            set
            {
                if (value.Equals(width)) return;
                width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => height;
            set
            {
                if (value.Equals(height)) return;
                height = value;
                OnPropertyChanged();
            }
        }

        public bool ShowMinButton
        {
            get => showMinButton;
            set
            {
                if (value == showMinButton) return;
                showMinButton = value;
                OnPropertyChanged();
            }
        }

        public bool ShowMaxRestoreButton
        {
            get => showMaxRestoreButton;
            set
            {
                if (value == showMaxRestoreButton) return;
                showMaxRestoreButton = value;
                OnPropertyChanged();
            }
        }

        public bool ShowCloseButton
        {
            get => showCloseButton;
            set
            {
                if (value == showCloseButton) return;
                showCloseButton = value;
                OnPropertyChanged();
            }
        }

        public bool CanResize
        {
            get => canResize;
            set
            {
                if (value == canResize) return;
                canResize = value;
                OnPropertyChanged();
            }
        }

        public MaterialDialog Dialog
        {
            get => dialog;
            set
            {
                if (Equals(value, dialog)) return;
                dialog = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Task<bool?> Show()
        {
            return Show(MaterialApplication.DefaultDispatcher);
        }

        public Task<bool?> Show(DispatcherOption dispatcherOption)
        {
            return ShowTracked(dispatcherOption).Task;
        }

        public WindowSession ShowTracked()
        {
            return ShowTracked(MaterialApplication.DefaultDispatcher);
        }

        public WindowSession ShowTracked(DispatcherOption dispatcherOption)
        {
            return new WindowSession(this, MaterialApplication.GetDispatcher(dispatcherOption)).Show();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}