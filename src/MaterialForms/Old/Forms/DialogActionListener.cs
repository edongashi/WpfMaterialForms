using System;
using System.Threading.Tasks;

namespace MaterialForms
{
    public class DialogActionListener
    {
        public DialogActionListener(MaterialDialog dialog, string action)
        {
            if (string.IsNullOrEmpty(action))
                throw new ArgumentException(nameof(action));

            switch (action.ToLower())
            {
                case "positive":
                    dialog.OnPositiveAction = HandleAction;
                    break;
                case "negative":
                    dialog.OnNegativeAction = HandleAction;
                    break;
                case "auxiliary":
                    dialog.OnAuxiliaryAction = HandleAction;
                    break;
                default:
                    throw new InvalidOperationException("Invalid dialog action.");
            }
        }

        public bool ActionPerformed { get; private set; }

        private Task HandleAction(Session session)
        {
            ActionPerformed = true;
            return Task.CompletedTask;
        }
    }
}