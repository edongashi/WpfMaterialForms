using System;
using System.Collections.Generic;
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
        public static object CopyTo(this object baseClassInstance, object target)
        {
            foreach (var propertyInfo in baseClassInstance.GetType().GetHighestProperties().Select(i => i.PropertyInfo))
                try
                {
                    var value = propertyInfo.GetValue(baseClassInstance, null);
                    var highEquiv = target.GetType().GetHighestProperties(propertyInfo.Name);

                    if (null != value)
                        highEquiv.SetValue(target, value, null);
                }
                catch
                {
                    // ignored
                }

            return target;
        }

        /// <summary>
        /// Do all injections this type have defined on the global overrides list.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetInjectedObject<T>(this T obj)
        {
            var yo = (T) obj.CopyTo(
                Activator.CreateInstance(obj.GetType().GetInjectedType().AddParameterlessConstructor()));

            return yo;
        }

        /// <summary>
        /// Do all injections this type have defined on the global overrides list.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetInjectedType(this Type type)
        {
            var fullName = type.FullName;
            if (fullName == null || !Mapper.TypesOverrides.ContainsKey(fullName)) return type;
            type = Mapper.TypesOverrides[fullName].Where(i => i.PropertyInfo != null).Aggregate(type,
                (current, expression) =>
                    current.InjectPropertyAttributes(expression.PropertyInfo, expression.Expression));
            return Mapper.TypesOverrides[fullName].Where(i => i.PropertyInfo == null).Aggregate(type,
                (current, expression) => current.InjectClassAttributes(expression.Expression));
        }

        /// <summary>
        /// Inject attributes into a Type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propInfo"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static Type AddParameterlessConstructor(this Type type)
        {
            var assemblyName = new AssemblyName("ProxyBuilder");
            var assemblyBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            var typeBuilder = moduleBuilder.DefineType(type.Name + "wConstructor", TypeAttributes.Public, type);
            var constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
            {
                var constructorBuilder =
                    typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Any, Type.EmptyTypes);
                var cGen = constructorBuilder.GetILGenerator();
                cGen.Emit(OpCodes.Nop);
                cGen.Emit(OpCodes.Ret);
            }
            else
            {
                return type;
            }

            return typeBuilder.CreateType();
        }

        /// <summary>
        /// Inject attributes into a Type (class)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static Type InjectClassAttributes(this Type type, params Expression<Func<Attribute>>[] expressions)
        {
            if (!type.IsClass)
                throw new Exception("Type is not a class, cannot inject.");

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

            foreach (var expression in expressions)
                typeBuilder.SetCustomAttribute(expression);

            return typeBuilder.CreateType();
        }

        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            // is there any base type?
            if ((type == null) || (type.BaseType == null))
            {
                yield break;
            }

            // return all implemented or inherited interfaces
            foreach (var i in type.GetInterfaces())
            {
                yield return i;
            }

            // return all inherited types
            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType= currentBaseType.BaseType;
            }
        }
        
        /// <summary>
        /// Inject attributes into a property from a Type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propInfo"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public static Type InjectPropertyAttributes(this Type type, PropertyInfo propInfo,
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

            var custNamePropBldr = propInfo.Name.CreateProperty(typeBuilder, propInfo.PropertyType);
            
            foreach (var expression in expressions)
                custNamePropBldr.SetCustomAttribute(expression);
            return typeBuilder.CreateType();
        }

        public static PropertyBuilder CreateProperty(this string name, TypeBuilder typeBuilder,Type type)
        {
            var custNamePropBldr = typeBuilder.DefineProperty(name,
                PropertyAttributes.HasDefault,
                type,
                null);

            var customerNameBldr = typeBuilder.DefineField($"{name}_proxy_filter",
                type,
                FieldAttributes.Private);


            const MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName |
                                                MethodAttributes.HideBySig;

            var custNameGetPropMthdBldr =
                typeBuilder.DefineMethod($"get_{name}",
                    getSetAttr,
                    type,
                    Type.EmptyTypes);

            var custNameGetIl = custNameGetPropMthdBldr.GetILGenerator();

            custNameGetIl.Emit(OpCodes.Ldarg_0);
            custNameGetIl.Emit(OpCodes.Ldfld, customerNameBldr);
            custNameGetIl.Emit(OpCodes.Ret);

            var custNameSetPropMthdBldr =
                typeBuilder.DefineMethod($"set_{name}",
                    getSetAttr,
                    null,
                    new[] {type});

            var custNameSetIl = custNameSetPropMthdBldr.GetILGenerator();
            custNameSetIl.Emit(OpCodes.Ldarg_0);
            custNameSetIl.Emit(OpCodes.Ldarg_1);
            custNameSetIl.Emit(OpCodes.Stfld, customerNameBldr);
            custNameSetIl.Emit(OpCodes.Ret);
            custNamePropBldr.SetGetMethod(custNameGetPropMthdBldr);
            custNamePropBldr.SetSetMethod(custNameSetPropMthdBldr);
            return custNamePropBldr;
        }

        /// <summary>
        /// Get a propertyInfo using lambdas.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyLambda"></param>
        /// <returns></returns>
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