using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using AttributeBuilder;
using MaterialForms.Wpf.Annotations;

namespace MaterialForms.Mappers
{
    public class Mapper
    {
        /// <inheritdoc />
        public Mapper(object toMap, Attribute[] attributes)
        {
        }
    }

    public static class Class1
    {
        /// <summary>
        /// 
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
        /// <param name="obj"></param>
        /// <param name="propertyLambda"></param>
        /// <param name="attributes"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static TSource InjectAttributes<TSource, TProperty>(this TSource obj,
            Expression<Func<TSource, TProperty>> propertyLambda, Attribute[] attributes)
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

            var aName = new AssemblyName("ProxyBuilder");
            var ab = AppDomain.CurrentDomain.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);
            var mb = ab.DefineDynamicModule(aName.Name);
            var tb = mb.DefineType(type.Name + "Proxy", TypeAttributes.Public, type);

            var custNamePropBldr = tb.DefineProperty(propInfo.Name,
                PropertyAttributes.HasDefault,
                propInfo.PropertyType,
                null);

            var customerNameBldr = tb.DefineField($"{propInfo.Name}_proxy_filter",
                propInfo.PropertyType,
                FieldAttributes.Private);

            // The property set and property get methods require a special
            // set of attributes.
            var getSetAttr =
                MethodAttributes.Public | MethodAttributes.SpecialName |
                MethodAttributes.HideBySig;

            // Define the "get" accessor method for {propInfo.Name}.
            var custNameGetPropMthdBldr =
                tb.DefineMethod($"get_{propInfo.Name}",
                    getSetAttr,
                    typeof(string),
                    Type.EmptyTypes);

            var custNameGetIl = custNameGetPropMthdBldr.GetILGenerator();

            custNameGetIl.Emit(OpCodes.Ldarg_0);
            custNameGetIl.Emit(OpCodes.Ldfld, customerNameBldr);
            custNameGetIl.Emit(OpCodes.Ret);

            // Define the "set" accessor method for CustomerName.
            var custNameSetPropMthdBldr =
                tb.DefineMethod($"set_{propInfo.Name}",
                    getSetAttr,
                    null,
                    new Type[] {propInfo.PropertyType});

            var custNameSetIL = custNameSetPropMthdBldr.GetILGenerator();

            custNameSetIL.Emit(OpCodes.Ldarg_0);
            custNameSetIL.Emit(OpCodes.Ldarg_1);
            custNameSetIL.Emit(OpCodes.Stfld, customerNameBldr);
            custNameSetIL.Emit(OpCodes.Ret);

            // Last, we must map the two methods created above to our PropertyBuilder to 
            // their corresponding behaviors, "get" and "set" respectively. 
            custNamePropBldr.SetGetMethod(custNameGetPropMthdBldr);
            custNamePropBldr.SetSetMethod(custNameSetPropMthdBldr);


            foreach (var injectedAttribute in attributes)
            {
                custNamePropBldr.SetCustomAttribute(() => new FieldAttribute {Name = "Hello"});
            }

            var newtype = tb.CreateType();
            return (TSource) Activator.CreateInstance(newtype);
        }
    }
}