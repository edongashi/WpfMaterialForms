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

        public IValueProvider IsReset { get; set; }

        public IValueProvider Validates { get; set; }

        protected internal override IBindingProvider CreateBindingProvider(IResourceContext context, IDictionary<string, IValueProvider> formResources)
        {
            return new ActionPresenter(context, Resources, formResources)
            {
                Command = new ActionElementCommand(context, ActionName, ActionParameter, IsEnabled, Validates, IsReset),
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
        private readonly IBoolProxy validates;
        private readonly IBoolProxy resets;

        public ActionElementCommand(IResourceContext context, string actionName, IValueProvider actionParameter, IValueProvider isEnabled, IValueProvider validates, IValueProvider isReset)
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

            this.validates = validates != null ? (IBoolProxy)validates.GetBoolValue(context) : new PlainBool(false);
            resets = isReset != null ? (IBoolProxy)isReset.GetBoolValue(context) : new PlainBool(false);
            this.actionParameter = actionParameter?.GetBestMatchingProxy(context) ?? new PlainObject(null);
        }

        public void Execute(object parameter)
        {
            var arg = actionParameter.Value;
            var model = context.GetModelInstance();
            if (resets.Value && ModelState.IsModel(model))
            {
                ModelState.Reset(model);
            }
            else if (validates.Value && ModelState.IsModel(model))
            {
                var isValid = ModelState.Validate(model);
                if (!isValid)
                {
                    return;
                }
            }

            if (model is IActionHandler modelHandler)
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
