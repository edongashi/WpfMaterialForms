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

        public abstract MaterialDialog Dialog { get; }

        public object this[string key] => Dialog.Form[key];

        public abstract Task<bool?> Task { get; }

        public abstract void Close(bool? result);
    }
}
