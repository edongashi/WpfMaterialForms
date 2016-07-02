using System.Threading;
using System.Threading.Tasks;

namespace MaterialForms
{
    public abstract class Session
    {
        private static int staticId;

        protected Session()
        {
            Id = Interlocked.Increment(ref staticId);
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
        /// Gets the value of the form schema with the specified key.
        /// </summary>
        /// <param name="key">The key of the schema.</param>
        public object this[string key] => Dialog.Form[key];

        /// <summary>
        /// Gets the task that represents the dialog's session lifecycle.
        /// </summary>
        public abstract Task<bool?> Task { get; }

        /// <summary>
        /// Closes the session unconditionally..
        /// </summary>
        /// <param name="result">The value that will be returned from the session's task.</param>
        public abstract void Close(bool? result);
    }
}
