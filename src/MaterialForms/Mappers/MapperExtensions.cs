using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using AttributeBuilder;

namespace MaterialForms.Mappers
{
    /// <summary>
    ///     Mapper extensions
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static CustomAttributeBuilder BuildCustomAttribute(this Attribute attribute)
        {
            var type = attribute.GetType();
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(i => i.CanWrite)
                .ToArray();

            var propertyValues = from p in properties
                select p.GetValue(attribute, null);

            return new CustomAttributeBuilder(constructor ?? throw new InvalidOperationException(),
                Type.EmptyTypes,
                properties,
                propertyValues.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="type"></param>
        /// <param name="propInfo"></param>
        /// <param name="expression"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static Type InjectAttributes(this Type type, PropertyInfo propInfo,
            params Expression<Func<Attribute>>[] expressions)
        {
            var assemblyName = new AssemblyName("ProxyBuilder");
            var assemblyBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            var typeBuilder = moduleBuilder.DefineType(type.Name + "Proxy", TypeAttributes.Public, type);
            var constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
            {
                var constructorBuilder =
                    typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Any, Type.EmptyTypes);
                var cGen = constructorBuilder.GetILGenerator();
                cGen.Emit(OpCodes.Nop);
                cGen.Emit(OpCodes.Ret);
            }

            var custNamePropBldr = typeBuilder.DefineProperty(propInfo.Name,
                PropertyAttributes.HasDefault,
                propInfo.PropertyType,
                null);

            var customerNameBldr = typeBuilder.DefineField($"{propInfo.Name}_proxy_filter",
                propInfo.PropertyType,
                FieldAttributes.Private);


            const MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName |
                                                MethodAttributes.HideBySig;

            var custNameGetPropMthdBldr =
                typeBuilder.DefineMethod($"get_{propInfo.Name}",
                    getSetAttr,
                    propInfo.PropertyType,
                    Type.EmptyTypes);

            var custNameGetIl = custNameGetPropMthdBldr.GetILGenerator();

            custNameGetIl.Emit(OpCodes.Ldarg_0);
            custNameGetIl.Emit(OpCodes.Ldfld, customerNameBldr);
            custNameGetIl.Emit(OpCodes.Ret);

            var custNameSetPropMthdBldr =
                typeBuilder.DefineMethod($"set_{propInfo.Name}",
                    getSetAttr,
                    null,
                    new[] {propInfo.PropertyType});

            var custNameSetIl = custNameSetPropMthdBldr.GetILGenerator();
            custNameSetIl.Emit(OpCodes.Ldarg_0);
            custNameSetIl.Emit(OpCodes.Ldarg_1);
            custNameSetIl.Emit(OpCodes.Stfld, customerNameBldr);
            custNameSetIl.Emit(OpCodes.Ret);
            custNamePropBldr.SetGetMethod(custNameGetPropMthdBldr);
            custNamePropBldr.SetSetMethod(custNameSetPropMthdBldr);

            foreach (var expression in expressions)
                custNamePropBldr.SetCustomAttribute(expression);
            return typeBuilder.CreateType();
        }


        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyLambda"></param>
        /// <param name="attributes"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static TSource InjectAttributes<TSource, TProperty>(this TSource obj,
            Expression<Func<TSource, TProperty>> propertyLambda, Expression<Func<Attribute>> expression)
        {
            var type = typeof(TSource);

            if (!(propertyLambda.Body is MemberExpression member))
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a field, not a property.");

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType ?? throw new InvalidOperationException()))
                throw new ArgumentException(
                    $"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");

            return InjectAttributes<TSource>(typeof(TSource), propInfo, expression);
        }

        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(
            this TSource source,
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof(TSource);

            if (!(propertyLambda.Body is MemberExpression member))
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda}' refers to a field, not a property.");

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(
                    $"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");

            return propInfo;
        }
    }
}