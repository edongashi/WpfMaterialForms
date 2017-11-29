using System.Windows;
using System.Windows.Controls;

namespace MaterialForms.Controls
{
    /// <summary>
    ///     Interaction logic for MultiSchemaControl.xaml
    /// </summary>
    public partial class MultiSchemaControl : UserControl
    {
        public MultiSchemaControl(SchemaBase[] schemas, double[] columnWidths)
        {
            InitializeComponent();
            var schemaCount = schemas.Length;
            var length = columnWidths.Length;
            for (var i = 0; i < schemaCount; i++)
            {
                var schema = schemas[i];
                var gridWidth = 1d;
                if (i < length)
                    gridWidth = columnWidths[i];

                LayoutGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(gridWidth, GridUnitType.Star)
                });

                var schemaView = schema?.View;
                if (schemaView != null)
                {
                    schemaView.SetValue(Grid.ColumnProperty, i);
                    schemaView.VerticalAlignment = VerticalAlignment.Center;
                    var leftMargin = i != 0 ? 8d : 0d;
                    var rightMargin = i != schemaCount - 1 ? 8d : 0d;
                    schemaView.Margin = new Thickness(leftMargin, 0d, rightMargin, 0d);
                    LayoutGrid.Children.Add(schemaView);
                }
            }
        }
    }
}