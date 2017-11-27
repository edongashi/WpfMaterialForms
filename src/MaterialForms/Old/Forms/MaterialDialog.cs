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
        /// <summary>
        ///     Represents the default theme used by new dialogs.
        /// </summary>
        public static DialogTheme DefaultDialogTheme = DialogTheme.Inherit;

        private string auxiliaryAction;
        private MaterialForm form;
        private string message;
        private string negativeAction;
        private string positiveAction;
        private DialogTheme theme;

        private string title;

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

        public MaterialDialog(string message, string title, string positiveAction, string negativeAction,
            MaterialForm form)
            : this(message, title, positiveAction, negativeAction, null, form)
        {
        }

        public MaterialDialog(string message, string title, string positiveAction, string negativeAction,
            string auxiliaryAction, MaterialForm form)
        {
            Message = message;
            Title = title;
            PositiveAction = positiveAction;
            NegativeAction = negativeAction;
            AuxiliaryAction = auxiliaryAction;
            Form = form;
            Theme = DefaultDialogTheme;
        }

        /// <summary>
        ///     Gets or sets the title that appears in the dialog. This is not the same as the window title.
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the message that appears in the dialog.
        /// </summary>
        public string Message
        {
            get => message;
            set
            {
                if (value == message) return;
                message = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the text that appears on the positive dialog button. A null or empty value hides the button.
        /// </summary>
        public string PositiveAction
        {
            get => positiveAction;
            set
            {
                if (value == positiveAction) return;
                positiveAction = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the dialog theme.
        /// </summary>
        public DialogTheme Theme
        {
            get => theme;
            set
            {
                if (value == theme) return;
                theme = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the callback that will be invoked when the positive button is clicked by the user.
        ///     When this property is not null, the dialog must be closed explicitly using Session.Close(result).
        /// </summary>
        public FormActionCallback OnPositiveAction { get; set; }

        /// <summary>
        ///     Gets or sets the text that appears on the negative dialog button. A null or empty value hides the button.
        /// </summary>
        public string NegativeAction
        {
            get => negativeAction;
            set
            {
                if (value == negativeAction) return;
                negativeAction = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the callback that will be invoked when the negative button is clicked by the user.
        ///     When this property is not null, the dialog must be closed explicitly using Session.Close(result).
        /// </summary>
        public FormActionCallback OnNegativeAction { get; set; }

        /// <summary>
        ///     Gets or sets the text that appears on the auxiliary dialog button. A null or empty value hides the button.
        /// </summary>
        public string AuxiliaryAction
        {
            get => auxiliaryAction;
            set
            {
                if (value == auxiliaryAction) return;
                auxiliaryAction = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the callback that will be invoked when the auxiliary button is clicked by the user.
        /// </summary>
        public FormActionCallback OnAuxiliaryAction { get; set; }

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

        /// <summary>
        ///     Gets or sets whether to perform form validation before handling the positive dialog action.
        /// </summary>
        public bool ValidatesOnPositiveAction { get; set; } = true;

        /// <summary>
        ///     Gets or sets whether to show a progress indicator while performing the positive dialog action.
        ///     Applies only when a custom handler is assigned to the PositiveAction property.
        /// </summary>
        public bool ShowsProgressOnPositiveAction { get; set; }

        /// <summary>
        ///     Gets a new instance of a DialogView bound to this object.
        /// </summary>
        public UserControl View => new DialogView(this);

        public bool Validate()
        {
            var currentForm = Form;
            return currentForm == null || currentForm.Validate();
        }

        public Task<bool?> Show(string dialogIdentifier)
        {
            return Show(dialogIdentifier, double.NaN);
        }

        public Task<bool?> Show(string dialogIdentifier, double width)
        {
            return ShowTracked(dialogIdentifier, width).Task;
        }

        public DialogSession ShowTracked(string dialogIdentifier)
        {
            return ShowTracked(dialogIdentifier, double.NaN);
        }

        public DialogSession ShowTracked(string dialogIdentifier, double width)
        {
            return new DialogSession(dialogIdentifier, this, width).Show();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}