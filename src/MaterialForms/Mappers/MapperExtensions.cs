using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using AttributeBuilder;
using Ninject;
using Ninject.Infrastructure.Language;

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
                    var highEquiv = target.GetType().GetHighestProperty(propertyInfo.Name);

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
            if (!Mapper.TypesOverrides.ContainsKey(obj.GetType())) return obj;
            return (T) Mapper.TypesOverrides[obj.GetType()].Spawn();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ExtensionMapper FindOverridableType(this Type type)
        {
            if (Mapper.TypesOverrides.ContainsKey(type))
                return Mapper.TypesOverrides[type];

            var injType = type.GetParentTypes()
                .FirstOrDefault(allBaseType => Mapper.TypesOverrides.ContainsKey(allBaseType));

            return injType != null ? Mapper.TypesOverrides[injType] : null;
        }

        /// <summary>
        /// Do all injections this type have defined on the global overrides list.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetInjectedType(this Type type)
        {
            var mapper = type.FindOverridableType();
            if (mapper == null) return type;

            type = mapper.Mapper.Mappings.Where(i => i.PropertyInfo != null).Aggregate(type,
                (current, expression) =>
                    current.InjectPropertyAttributes(expression.PropertyInfo, expression.Expression));
            return mapper.Mapper.Mappings.Where(i => i.PropertyInfo == null).Aggregate(type,
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
            var constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor != null)
            {
                return type;
            }

            var moduleBuilder = ModuleBuilder();
            var typeBuilder = moduleBuilder.DefineType(type.Name + "ctor", TypeAttributes.Public, type);

            var constructorBuilder =
                typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
            var cGen = constructorBuilder.GetILGenerator();
            cGen.Emit(OpCodes.Nop);
            cGen.Emit(OpCodes.Ret);

            return typeBuilder.CreateType();
        }

        private static ModuleBuilder ModuleBuilder()
        {
            var assemblyName = new AssemblyName("ProxyBuilder");
            var assemblyBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            return moduleBuilder;
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

            var moduleBuilder = ModuleBuilder();
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

        /// <summary>
        /// Get all base types from a type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            if ((type == null) || (type.BaseType == null))
            {
                return new List<Type> {type};
            }

            var returnList = type.GetInterfaces().ToList();

            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                returnList.Add(currentBaseType);
                currentBaseType = currentBaseType.BaseType;
            }

            return returnList;
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
            var moduleBuilder = ModuleBuilder();
            type = type.AddParameterlessConstructor();
            var typeBuilder = moduleBuilder.DefineType(type.Name + "Proxy", TypeAttributes.Public, type);
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var custNamePropBldr = propInfo.Name.CreateProperty(typeBuilder, propInfo.PropertyType);

            foreach (var expression in expressions)
                custNamePropBldr.SetCustomAttribute(expression);
            return typeBuilder.CreateType();
        }

        /// <summary>
        /// Inject attributes into a property from a Type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="propType"></param>
        /// <returns></returns>
        public static Type InjectProperty(this Type type, string name, Type propType)
        {
            type = type.AddParameterlessConstructor();
            var moduleBuilder = ModuleBuilder();
            var typeBuilder = moduleBuilder.DefineType(type.Name + "prop", TypeAttributes.Public, type);
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var custNamePropBldr = name.CreateProperty(typeBuilder, propType);
            return typeBuilder.CreateType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeBuilder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyBuilder CreateProperty(this string name, TypeBuilder typeBuilder, Type type)
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