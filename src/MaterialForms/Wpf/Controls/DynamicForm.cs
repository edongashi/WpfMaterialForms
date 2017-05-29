using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Controls
{
    public class DynamicForm : Control, IDynamicForm
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

        internal static readonly DependencyPropertyKey FormDefinitionPropertyKey = DependencyProperty.RegisterReadOnly(
            "FormDefinition",
            typeof(object),
            typeof(FormDefinition),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ContextProperty = DependencyProperty.Register(
            "Context",
            typeof(object),
            typeof(DynamicForm),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ItemsPanelProperty = DependencyProperty.Register(
            "ItemsPanel",
            typeof(ItemsPanelTemplate),
            typeof(DynamicForm),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty = ValuePropertyKey.DependencyProperty;

        public static readonly DependencyProperty FormDefinitionProperty = FormDefinitionPropertyKey.DependencyProperty;

        private static void ModelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((DynamicForm)obj).UpdateModel(e.OldValue, e.NewValue);
        }

        public DynamicForm()
        {
            BindingOperations.SetBinding(this, ContextProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(DataContextProperty),
                Mode = BindingMode.OneWay
            });
        }

        /// <summary>
        /// Gets or sets the model associated with this form.
        /// If the value is a MaterialFormDefinition, a form will be built based on that definition.
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

        public FormDefinition FormDefinition => (FormDefinition)GetValue(FormDefinitionProperty);

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

        public ItemsPanelTemplate ItemsPanel
        {
            get => (ItemsPanelTemplate)GetValue(ItemsPanelProperty);
            set => SetValue(ItemsPanelProperty, value);
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
            else if (oldModel.GetType() == newModel.GetType())
            {
                // Same type -> update values only
                SetValue(ValuePropertyKey, newModel);
            }
            else if (newModel is FormDefinition formDefinition)
            {
                // MaterialFormDefinition -> Build form
                var instance = formDefinition.CreateInstance();
                RebuildForm(formDefinition);
                SetValue(ValuePropertyKey, instance);
            }
            else if (newModel is Type type)
            {
                // Type -> Build form, Value = new Type
                formDefinition = FormBuilder.Default.GetDefinition(type);
                var instance = formDefinition.CreateInstance();
                RebuildForm(formDefinition);
                SetValue(ValuePropertyKey, instance);
            }
            else
            {
                // object -> Build form, Value = model
                RebuildForm(FormBuilder.Default.GetDefinition(newModel.GetType()));
                SetValue(ValuePropertyKey, newModel);
            }
        }

        private void RebuildForm(FormDefinition formDefinition)
        {
            ClearForm();

        }

        private void ClearForm()
        {
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
