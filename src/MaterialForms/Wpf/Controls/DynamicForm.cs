using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Fields;
using MaterialForms.Wpf.FormBuilding;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Controls
{
    [TemplatePart(Name = "PART_ItemsGrid", Type = typeof(Grid))]
    public sealed class DynamicForm : Control, IDynamicForm
    {
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model",
            typeof(object),
            typeof(DynamicForm),
            new FrameworkPropertyMetadata(null, ModelChanged));

        internal static readonly DependencyPropertyKey ValuePropertyKey = DependencyProperty.RegisterReadOnly(
            "Value",
            typeof(object),
            typeof(DynamicForm),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ContextProperty = DependencyProperty.Register(
            "Context",
            typeof(object),
            typeof(DynamicForm),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty FormBuilderProperty = DependencyProperty.Register(
            "FormBuilder",
            typeof(IFormBuilder),
            typeof(DynamicForm),
            new FrameworkPropertyMetadata(FormBuilding.FormBuilder.Default));

        public static readonly DependencyProperty ValueProperty = ValuePropertyKey.DependencyProperty;

        static DynamicForm()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicForm), new FrameworkPropertyMetadata(typeof(DynamicForm)));
        }

        private readonly IResourceContext resourceContext;

        private readonly List<FormContentPresenter> currentElements;
        private double[] columns;
        private int rows;

        private Grid itemsGrid;

        public DynamicForm()
        {
            IsTabStop = false;
            resourceContext = new FormResourceContext(this);
            columns = new double[0];
            currentElements = new List<FormContentPresenter>();
            BindingOperations.SetBinding(this, ContextProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(DataContextProperty),
                Mode = BindingMode.OneWay
            });
        }

        /// <summary>
        /// Gets or sets the form builder that is responsible for building forms.
        /// </summary>
        public IFormBuilder FormBuilder
        {
            get => (IFormBuilder)GetValue(FormBuilderProperty);
            set => SetValue(FormBuilderProperty, value);
        }

        /// <summary>
        /// Gets or sets the model associated with this form.
        /// If the value is a IFormDefinition, a form will be built based on that definition.
        /// If the value is a Type, a form will be built and bound to a new instance of that type.
        /// If the value is a simple object, a single field bound to this property will be displayed.
        /// If the value is a complex object, a form will be built and bound to properties of that instance.
        /// </summary>
        public object Model
        {
            get => GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        /// <summary>
        /// Gets the value of the current model instance.
        /// </summary>
        public object Value => GetValue(ValueProperty);

        /// <summary>
        /// Gets or sets the context associated with this form.
        /// Models can utilize this property to get data from
        /// outside of their instance scope.
        /// </summary>
        public object Context
        {
            get => GetValue(ContextProperty);
            set => SetValue(ContextProperty, value);
        }

        public void ReloadElements()
        {
            FillGrid();
        }

        private static void ModelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DynamicForm)obj).UpdateModel(e.OldValue, e.NewValue);
        }

        private void UpdateModel(object oldModel, object newModel)
        {
            if (Equals(oldModel, newModel))
            {
                return;
            }

            if (Equals(Value, newModel))
            {
                return;
            }

            if (newModel == null)
            {
                // null -> Clear Form
                ClearForm();
                SetValue(ValuePropertyKey, null);
            }
            else if (oldModel != null && oldModel.GetType() == newModel.GetType())
            {
                // Same type -> update values only.
                SetValue(ValuePropertyKey, newModel);
            }
            else if (newModel is IFormDefinition formDefinition)
            {
                // IFormDefinition -> Build form
                var instance = formDefinition.CreateInstance(resourceContext);
                RebuildForm(formDefinition);
                SetValue(ValuePropertyKey, instance);
            }
            else if (newModel is Type type)
            {
                // Type -> Build form, Value = new Type
                formDefinition = FormBuilder.GetDefinition(type);
                var instance = formDefinition.CreateInstance(resourceContext);
                RebuildForm(formDefinition);
                SetValue(ValuePropertyKey, instance);
            }
            else
            {
                // object -> Build form, Value = model
                RebuildForm(FormBuilder.GetDefinition(newModel.GetType()));
                SetValue(ValuePropertyKey, newModel);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            itemsGrid?.Children.Clear();
            itemsGrid = Template.FindName("PART_ItemsGrid", this) as Grid;
            FillGrid();
        }

        private void RebuildForm(IFormDefinition formDefinition)
        {
            ClearForm();
            if (formDefinition == null)
            {
                return;
            }

            rows = formDefinition.FormRows.Count;
            columns = formDefinition.Grid;
            currentElements.Clear();
            for (var i = 0; i < rows; i++)
            {
                var row = formDefinition.FormRows[i];
                foreach (var element in row.Elements)
                {
                    currentElements.Add(new FormContentPresenter(i, element.Column, element.ColumnSpan,
                        element.Element.CreateBindingProvider(resourceContext, formDefinition.Resources)));
                }
            }

            FillGrid();
        }

        private void FillGrid()
        {
            if (itemsGrid == null)
            {
                return;
            }

            itemsGrid.Children.Clear();
            itemsGrid.RowDefinitions.Clear();
            itemsGrid.ColumnDefinitions.Clear();
            for (var i = 0; i < rows; i++)
            {
                itemsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            foreach (var column in columns)
            {
                itemsGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = column > 0d
                    ? new GridLength(column, GridUnitType.Star)
                    : new GridLength(-column, GridUnitType.Pixel)
                });
            }

            foreach (var content in currentElements)
            {
                var contentPresenter = new ContentPresenter
                {
                    Content = content.BindingProvider,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Grid.SetRow(contentPresenter, content.Row);
                Grid.SetColumn(contentPresenter, content.Column);
                Grid.SetColumnSpan(contentPresenter, content.ColumnSpan);
                itemsGrid.Children.Add(contentPresenter);
            }
        }

        private void ClearForm()
        {
            itemsGrid?.Children.Clear();
            var resources = Resources;
            var keys = resources.Keys;
            foreach (var key in keys)
            {
                if (key is DynamicResourceKey || key is BindingProxyKey)
                {
                    var proxy = (BindingProxy)resources[key];
                    proxy.Value = null;
                    resources.Remove(key);
                }
            }
        }
    }
}
