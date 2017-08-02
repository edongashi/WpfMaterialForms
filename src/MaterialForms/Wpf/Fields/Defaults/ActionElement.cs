using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MaterialForms.Wpf.Controls;
using MaterialForms.Wpf.Resources;

namespace MaterialForms.Wpf.Fields.Defaults
{
    public class ActionElement : ContentElement
    {
        public string ActionName { get; set; }

        public IValueProvider ActionParameter { get; set; }

        public IValueProvider IsEnabled { get; set; }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new ActionPresenter(context, Resources, formResources)
            {
                Command = new ActionElementCommand(context, ActionName, ActionParameter, IsEnabled),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = LinePosition == Position.Left ? HorizontalAlignment.Left : HorizontalAlignment.Right
            };
        }
    }

    internal class ActionElementCommand : ICommand
    {
        private readonly string actionName;
        private readonly IProxy actionParameter;

        private readonly IResourceContext context;
        private readonly IBoolProxy canExecute;

        public ActionElementCommand(IResourceContext context, string actionName, IValueProvider actionParameter, IValueProvider isEnabled)
        {
            this.context = context;
            this.actionName = actionName;
            switch (isEnabled)
            {
                case LiteralValue v when v.Value is bool b:
                    canExecute = new PlainBool(b);
                    break;
                case null:
                    canExecute = new PlainBool(true);
                    break;
                default:
                    var proxy = isEnabled.GetBoolValue(context);
                    proxy.ValueChanged = () => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                    canExecute = proxy;
                    break;
            }

            this.actionParameter = actionParameter?.GetBestMatchingProxy(context) ?? new PlainObject(null);
        }

        public void Execute(object parameter)
        {
            var arg = actionParameter.Value;
            if (context.GetModelInstance() is IActionHandler modelHandler)
            {
                modelHandler.HandleAction(actionName, arg);
            }

            if (context.GetContextInstance() is IActionHandler contextHandler)
            {
                contextHandler.HandleAction(actionName, arg);
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute.Value;
        }

        public event EventHandler CanExecuteChanged;
    }

    public class ActionPresenter : BindingProvider
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command",
            typeof(ICommand),
            typeof(ActionPresenter),
            new PropertyMetadata(null));

        static ActionPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ActionPresenter), new FrameworkPropertyMetadata(typeof(ActionPresenter)));
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ActionPresenter(IResourceContext context, IDictionary<string, IValueProvider> fieldResources, IDictionary<string, IValueProvider> formResources)
            : base(context, fieldResources, formResources, true)
        {
        }
    }
}
