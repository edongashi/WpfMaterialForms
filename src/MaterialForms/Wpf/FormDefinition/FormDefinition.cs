using System;
using System.Windows;
using System.Windows.Data;

namespace MaterialForms.Wpf
{
    public class FormDefinition
    {

    }

    public class ModelDefinition
    {
        
    }

    public class ModelMessages
    {
        public ModelMessages(BoundExpression title, BoundExpression details, BoundExpression create, BoundExpression edit, BoundExpression delete)
        {
            Title = title;
            Details = details;
            Create = create;
            Edit = edit;
            Delete = delete;
        }

        public BoundExpression Title { get; }

        public BoundExpression Details { get; }

        public BoundExpression Create { get; }

        public BoundExpression Edit { get; }

        public BoundExpression Delete { get; }
    }
}
