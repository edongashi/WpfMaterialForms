using System;
using System.Collections.Generic;
using System.Linq;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.FormBuilding
{
    internal static class Utilities
    {
        public static BindingProxy GetValueProxy(IResourceContext context, string propertyKey)
        {
            var key = new BindingProxyKey(propertyKey);
            if (context.TryFindResource(key) is BindingProxy proxy)
            {
                return proxy;
            }

            proxy = new BindingProxy();
            context.AddResource(key, proxy);
            return proxy;
        }

        public static Func<IResourceContext, IProxy> GetValueProvider(string propertyKey)
        {
            BindingProxy ValueProvider(IResourceContext context)
            {
                return GetValueProxy(context, propertyKey);
            }

            return ValueProvider;
        }

        public static Func<IResourceContext, IErrorStringProvider> GetErrorProvider(string message, string propertyKey)
        {
            var func = GetValueProvider(propertyKey);
            var boundExpression = BoundExpression.Parse(message, new Dictionary<string, object>
            {
                ["Value"] = func
            });

            if (boundExpression.IsPlainString)
            {
                var errorMessage = boundExpression.StringFormat;
                return context => new PlainErrorStringProvider(errorMessage);
            }

            if (boundExpression.Resources.Any(
                res => res is DeferredProxyResource resource && resource.ProxyProvider == func))
            {
                var key = propertyKey;
                return context => new ValueErrorStringProvider(boundExpression.GetStringValue(context), GetValueProxy(context, key));
            }

            return context => new ErrorStringProvider(boundExpression.GetStringValue(context));
        }
    }
}
