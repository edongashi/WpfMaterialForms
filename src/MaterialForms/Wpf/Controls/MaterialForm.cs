using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Controls
{
    public interface IMaterialForm
    {
        object Model { get; }

        object Value { get; }

        object Context { get; }
    }

    public class MaterialForm : Control, IMaterialForm
    {
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

        public static readonly DependencyProperty ContextProperty = DependencyProperty.Register(
            "Context",
            typeof(object),
            typeof(MaterialForm),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty = ValuePropertyKey.DependencyProperty;

        private static void ModelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((MaterialForm)obj).UpdateModel(e.OldValue, e.NewValue);
        }

        public MaterialForm()
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

        public object Context
        {
            get => GetValue(ContextProperty);
            set => SetValue(ContextProperty, value);
        }

        private void UpdateModel(object oldModel, object newModel)
        {
            SetValue(ValuePropertyKey, newModel);
            //if (Equals(oldModel, newModel))
            //{
            //    return;
            //}

            //if (Equals(Value, newModel))
            //{
            //    return;
            //}

            //if (newModel == null)
            //{
            //    // null -> Clear Form
            //    ClearForm();
            //    SetValue(ValuePropertyKey, null);
            //}
            //else if (newModel is FormDefinition)
            //{
            //    // MaterialFormDefinition -> Build form
            //    throw new NotImplementedException();
            //    //RebuildForm((MaterialFormSchema)newModel, null);
            //    //SetValue(ValuePropertyKey, null);
            //}
            //else if (newModel is Type)
            //{
            //    // Type -> Build form, Value = new Type
            //    var type = (Type)newModel;
            //    var instance = TypeManager.CreateInstance(type);
            //    //RebuildForm(GetDefinition(type), instance);
            //    //SetValue(ValuePropertyKey, instance);
            //}
            //else if (oldModel.GetType() == newModel.GetType())
            //{
            //    // Same type -> update values only
            //    var type = newModel.GetType();
            //    if (TypeManager.IsSimpleType(type))
            //    {
            //        SetCurrentValue(ModelProperty, newModel);
            //    }
            //    else
            //    {

            //    }

            //    SetValue(ValuePropertyKey, newModel);
            //}
            //else
            //{
            //    // Complex object -> Build form, Value = model
            //    //RebuildForm(GetDefinition(newModel.GetType()), newModel);
            //    SetValue(ValuePropertyKey, newModel);
            //}
        }

        private void ClearForm()
        {
            var resources = Resources;
            var keys = resources.Keys;
            foreach (var key in keys)
            {
                if (key is DynamicResourceKey)
                {
                    var proxy = (BindingProxy)resources[key];
                    proxy.Value = null;
                    resources.Remove(key);
                }
            }
        }
    }
}
