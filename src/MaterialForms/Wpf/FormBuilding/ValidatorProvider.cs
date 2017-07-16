using System;
using System.Windows.Controls;
using MaterialForms.Wpf.Resources;
using MaterialForms.Wpf.Validation;

namespace MaterialForms.Wpf.FormBuilding
{
    internal class ValidatorProvider : IValidatorProvider
    {
        private readonly Func<IResourceContext, ValidationRule> func;

        public ValidatorProvider(Func<IResourceContext, ValidationRule> func)
        {
            this.func = func;
        }

        public ValidationRule GetValidator(IResourceContext context)
        {
            return func(context);
        }
    }
}