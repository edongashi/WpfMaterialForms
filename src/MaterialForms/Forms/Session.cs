using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MaterialForms
{
    public abstract class Session
    {
        private static int staticId;

        private readonly Dictionary<string, string> pendingInvalidations;
        private bool locked;

        protected Session()
        {
            Id = Interlocked.Increment(ref staticId);
            pendingInvalidations = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the unique ID associated with this session.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the dialog object for which this session exists.
        /// </summary>
        public abstract MaterialDialog Dialog { get; }

        /// <summary>
        /// Gets or sets the value of the form schema with the specified key.
        /// </summary>
        /// <param name="key">The key of the schema.</param>
        public object this[string key]
        {
            get { return Dialog.Form[key]; }
            set { Dialog.Form[key] = value; }
        }

        /// <summary>
        /// Gets the task that represents the dialog's session lifecycle.
        /// </summary>
        public abstract Task<bool?> Task { get; }

        /// <summary>
        /// Closes the session unconditionally.
        /// </summary>
        /// <param name="result">The value that will be returned from the session's task.</param>
        public abstract void Close(bool? result);

        /// <summary>
        /// Displays an error message for the specified schema.
        /// </summary>
        public void Invalidate(string key, string message)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(message))
            {
                return;
            }

            if (locked)
            {
                pendingInvalidations[key] = message;
            }
            else
            {
                Dialog.Form?.GetSchema(key)?.Invalidate(message);
            }
        }

        internal void Lock()
        {
            locked = true;
        }

        internal void Unlock()
        {
            var form = Dialog.Form;
            if (form == null)
            {
                pendingInvalidations.Clear();
                return;
            }

            foreach (var pair in pendingInvalidations)
            {
                form.GetSchema(pair.Key)?.Invalidate(pair.Value);
            }

            pendingInvalidations.Clear();
            locked = false;
        }
    }
}
