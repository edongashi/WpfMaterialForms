using System;
using System.Collections.Generic;
using FastMember;

namespace MaterialForms.Wpf.Models
{
    public sealed class Snapshot
    {
        private readonly Dictionary<string, object> data;

        internal Snapshot(Dictionary<string, object> data, string snapshotTag, DateTime snapshotTime)
        {
            this.data = data;
            SnapshotTag = snapshotTag;
            SnapshotTime = snapshotTime;
        }

        /// <summary>
        /// Gets the tag associated with this snapshot.
        /// </summary>
        public string SnapshotTag { get; }

        /// <summary>
        /// Gets the time when this snapshow was taken.
        /// </summary>
        public DateTime SnapshotTime { get; }

        internal void Apply(object model, bool throwOnError)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var accessor = ObjectAccessor.Create(model);
            if (throwOnError)
            {
                foreach (var pair in data)
                {
                    accessor[pair.Key] = pair.Value;
                }
            }
            else
            {
                foreach (var pair in data)
                {
                    try
                    {
                        accessor[pair.Key] = pair.Value;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }
    }
}
