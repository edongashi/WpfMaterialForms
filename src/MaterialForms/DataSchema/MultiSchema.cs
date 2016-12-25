using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    /// <summary>
    /// Allows adding multiple schemas within the same row.
    /// </summary>
    public class MultiSchema : SchemaBase
    {
        public MultiSchema(params SchemaBase[] schemas)
        {
            if (schemas == null || schemas.Length < 2)
            {
                throw new ArgumentException("A minimum of two schemas is required.");
            }

            Schemas = schemas;
        }

        /// <summary>
        /// Gets the schemas being presented.
        /// </summary>
        public SchemaBase[] Schemas { get; }

        /// <summary>
        /// Gets the star size width of each schema in their respective index.
        /// Default width for each element is one unit.
        /// </summary>
        public IEnumerable<double> RelativeColumnWidths { get; set; }

        public override UserControl CreateView()
        {
            var columnWidths = RelativeColumnWidths as double[] ?? RelativeColumnWidths?.ToArray() ?? new double[0];
            return new MultiSchemaControl(Schemas, columnWidths);
        }

        public override bool HoldsValue => Schemas.Any(schema => schema.HoldsValue);

        protected override bool OnValidation()
        {
            var isValid = true;
            foreach (var schema in Schemas)
            {
                if (schema == null)
                {
                    continue;
                }

                isValid &= schema.Validate();
            }

            return isValid;
        }

        public override object GetValue() => GetForm();

        public MaterialForm GetForm()
        {
            return new MaterialForm(Schemas);
        }
    }
}
