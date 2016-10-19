using System;

namespace MaterialForms.Extensions
{
    /// <summary>
    /// Helper extensions for object initialization.
    /// </summary>
    public static class InitializerExtensions
    {
        public static T With<T>(this T instance, Action<T> action) where T : class
        {
            action(instance);
            return instance;
        }
    }
}
