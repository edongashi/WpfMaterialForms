using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MaterialForms.Wpf
{
    public class MaterialForm : Control
    {
        private readonly ObservableCollection<MaterialField> fields;

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model",
            typeof(object),
            typeof(MaterialForm),
            new FrameworkPropertyMetadata(null, ModelChanged));

        internal static readonly DependencyPropertyKey ValuePropertyKey = DependencyProperty.RegisterReadOnly(
            "Value",
            typeof(object),
            typeof(MaterialForm),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty = ValuePropertyKey.DependencyProperty;

        private static void ModelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((MaterialForm)obj).UpdateModel(e.OldValue, e.NewValue);
        }

        public ReadOnlyObservableCollection<MaterialField> Fields { get; }

        public MaterialForm()
        {
            fields = new ObservableCollection<MaterialField>();
            Fields = new ReadOnlyObservableCollection<MaterialField>(fields);
            Initialized += (s, e) => UpdateModel(null, Model);
        }

        /// <summary>
        /// Gets or sets the model associated with this form.
        /// If the value is a MaterialFormDefinition, a form will be build based on that definition.
        /// If the value is a Type, a form will be built and bound to a new instance of that type.
        /// If the value is a simple object, a single field bound to this property will be displayed.
        /// If the value is a complex object, a form will be built and bound to properties of that instance.
        /// </summary>
        public object Model
        {
            get { return GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        /// <summary>
        /// Gets the value of the current model instance.
        /// </summary>
        public object Value => GetValue(ValueProperty);

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
            else if (newModel is MaterialFormSchema)
            {
                // MaterialFormDefinition -> Build form
                throw new NotImplementedException();
                RebuildForm((MaterialFormSchema)newModel, null);
                SetValue(ValuePropertyKey, null);
            }
            else if (newModel is Type)
            {
                // Type -> Build form, Value = new Type
                var type = (Type)newModel;
                var instance = TypeManager.CreateInstance(type);
                RebuildForm(GetDefinition(type), instance);
                SetValue(ValuePropertyKey, instance);
            }
            else if (oldModel.GetType() == newModel.GetType())
            {
                // Same type -> update values only
                var type = newModel.GetType();
                if (TypeManager.IsSimpleType(type))
                {
                    SetCurrentValue(ModelProperty, newModel);
                }
                else
                {
                    foreach (var field in Fields)
                    {
                        field.UpdateSource(newModel);
                    }
                }

                SetValue(ValuePropertyKey, newModel);
            }
            else
            {
                // Complex object -> Build form, Value = model
                RebuildForm(GetDefinition(newModel.GetType()), newModel);
                SetValue(ValuePropertyKey, newModel);
            }
        }

        private void ClearForm()
        {
            fields.Clear();
        }

        private void RebuildForm(MaterialFormSchema schema, object valueHolder)
        {

        }

        private MaterialFormSchema GetDefinition(Type type)
        {
            return TypeManager.GetDefinition(type);
        }

        internal MaterialFormSchema FormSchema { get; set; }
    }
}
