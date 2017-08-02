using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.Fields.Defaults;
using MaterialForms.Wpf.FormBuilding;
using MaterialForms.Wpf.Resources;

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
            // Actions are displayed to the right by default.
            LinePosition = Controls.Position.Right;
        }

        public bool IsDefault { get; set; }

        public bool IsCancel { get; set; }

        /// <summary>
        /// Action identifier that is passed to handlers.
        /// </summary>
        public string ActionName { get; }

        /// <summary>
        /// Action parameter. Accepts a dynamic expression.
        /// </summary>
        public object Parameter { get; set; }

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
                ActionParameter = Parameter is string expr
                    ? BoundExpression.ParseSimplified(expr)
                    : new LiteralValue(Parameter),
                Content = Utilities.GetStringResource(Content),
                Icon = Utilities.GetIconResource(Icon),
                IsEnabled = Utilities.GetResource<bool>(IsEnabled, true, Deserializers.Boolean)
            };
        }
    }
}
