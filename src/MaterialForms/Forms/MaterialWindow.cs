using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MaterialForms.Annotations;

namespace MaterialForms
{
    public class MaterialWindow : INotifyPropertyChanged
    {
        private static int dialogId;
        private static readonly Application MaterialFormsApplication;

        static MaterialWindow()
        {
            MaterialFormsApplication = new Application();
            LoadResources(MaterialFormsApplication);
        }

        public static void LoadResources(Application application)
        {
            application.Resources.MergedDictionaries.Add(
                Application.LoadComponent(
                    new Uri("MaterialForms;component/Resources/Material.xaml",
                    UriKind.Relative)) as ResourceDictionary);
        }

        private int currentDialogId;
        private MaterialFormsWindow currentWindow;

        private string title = "Dialog";
        private double width = 400d;
        private double height = double.NaN;
        private bool showMinButton;
        private bool showMaxRestoreButton = true;
        private bool showCloseButton = true;
        private bool canResize;
        private MaterialDialog dialog;

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

        public void Show()
        {
            currentDialogId = dialogId++;
            currentWindow = new MaterialFormsWindow(this, currentDialogId);
            currentWindow.ShowDialog();
            currentWindow = null;
        }

        public void Close() => currentWindow?.Close();

        public async Task ShowDialog(MaterialDialog dialog, double width = double.NaN)
        {
            var view = dialog.View;
            view.Width = width;
            await DialogHost.Show(view, "DialogHost" + currentDialogId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
