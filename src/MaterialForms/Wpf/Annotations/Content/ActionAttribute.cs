using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Defaults;
using MaterialForms.Wpf.FormBuilding;

namespace MaterialForms.Wpf.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class ActionAttribute : FormContentAttribute
    {
        public ActionAttribute(string name, string content, [CallerLineNumber] int position = 0)
            : base(position)
        {
            ActionName = name;
            Content = content;
            // Actions are grouped by default.
            ShareLine = true;
            // Actions are inserted after elements by default.
            InsertAfter = true;
        }

        public bool IsDefault { get; set; }

        public bool IsCancel { get; set; }

        /// <summary>
        /// Action identifier that is passed to handlers.
        /// </summary>
        public string ActionName { get; }

        /// <summary>
        /// Displayed content. Accepts a dynamic expression.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Determines whether this action can be performed.
        /// Accepts a boolean or a dynamic resource.
        /// </summary>
        public object IsEnabled { get; set; }

        /// <summary>
        /// Displayed icon. Accepts a PackIconKind or a dynamic resource.
        /// </summary>
        public object Icon { get; set; }

        protected override FormElement CreateElement(MemberInfo target)
        {
            return new ActionElement
            {
                ActionName = ActionName,
                Content = Utilities.GetStringResource(Content),
                Icon = Utilities.GetIconResource(Icon),
                IsEnabled = Utilities.GetResource<bool>(IsEnabled, true, Deserializers.Boolean)
            };
        }
    }
}
