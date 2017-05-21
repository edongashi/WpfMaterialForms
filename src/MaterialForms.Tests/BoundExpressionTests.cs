using System.Collections.Generic;
using System.Windows;
using MaterialForms.Wpf;
using MaterialForms.Wpf.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaterialForms.Tests
{
    public class DummyForm : FrameworkElement, IMaterialForm
    {
        public object Model { get; set; }

        public object Context { get; set; }

        public object Value { get; set; }
    }

    [TestClass]
    public class BoundExpressionTests
    {
        [TestMethod]
        public void TestSingleResource()
        {
            T TestSingleResource<T>(string str) where T : Resource
            {
                var expression = BoundExpression.Parse(str);
                Assert.IsNull(expression.StringFormat);
                Assert.AreEqual(1, expression.Resources.Count);
                Assert.IsInstanceOfType(expression.Resources[0], typeof(T));
                return (T)expression.Resources[0];
            }

            var staticResource = TestSingleResource<StaticResource>("{StaticResource SRName}");
            Assert.AreEqual("SRName", staticResource.ResourceKey);
            Assert.IsFalse(staticResource.IsDynamic);

            var dynamicResource = TestSingleResource<DynamicResource>("{DynamicResource DRName}");
            Assert.AreEqual("DRName", dynamicResource.ResourceKey);
            Assert.IsTrue(dynamicResource.IsDynamic);

            var binding = TestSingleResource<PropertyBinding>("{Binding Person.Name}");
            Assert.AreEqual("Person.Name", binding.PropertyPath);
            Assert.IsFalse(binding.OneTimeBinding);
            Assert.IsTrue(binding.IsDynamic);

            binding = TestSingleResource<PropertyBinding>("{Property Person.Name}");
            Assert.AreEqual("Person.Name", binding.PropertyPath);
            Assert.IsTrue(binding.OneTimeBinding);
            Assert.IsFalse(binding.IsDynamic);

            var contextBinding = TestSingleResource<ContextPropertyBinding>("{ContextBinding Person.Name}");
            Assert.AreEqual("Person.Name", contextBinding.PropertyPath);
            Assert.IsFalse(contextBinding.OneTimeBinding);
            Assert.IsTrue(contextBinding.IsDynamic);

            contextBinding = TestSingleResource<ContextPropertyBinding>("{ContextProperty Person.Name}");
            Assert.AreEqual("Person.Name", contextBinding.PropertyPath);
            Assert.IsTrue(contextBinding.OneTimeBinding);
            Assert.IsFalse(contextBinding.IsDynamic);
        }

        [TestMethod]
        public void TestSingleResourceWithFormat()
        {
            void TestSingleResource<T>(string str, string format) where T : Resource
            {
                var expression = BoundExpression.Parse(str);
                Assert.AreEqual(format, expression.StringFormat);
                Assert.AreEqual(1, expression.Resources.Count);
                Assert.IsInstanceOfType(expression.Resources[0], typeof(T));
            }

            TestSingleResource<StaticResource>("{StaticResource Name:dd/MM/yyyy}", "{0:dd/MM/yyyy}");
            TestSingleResource<DynamicResource>("{DynamicResource Name:c}", "{0:c}");
            TestSingleResource<PropertyBinding>("{Binding Name,20:0.00}", "{0,20:0.00}");
            TestSingleResource<PropertyBinding>("{Property Name:dd/MM/yyyy}", "{0:dd/MM/yyyy}");
            TestSingleResource<ContextPropertyBinding>("{ContextBinding Name:dd/MM/yyyy}", "{0:dd/MM/yyyy}");
            TestSingleResource<ContextPropertyBinding>("{ContextProperty Name:dd/MM/yyyy}", "{0:dd/MM/yyyy}");
        }

        [TestMethod]
        public void TestMultipleResources()
        {
            var expression =
                BoundExpression.Parse(
                    "Your name is {Binding Name}. Hello {Binding Name,-30}, welcome to {ContextProperty Place}. It is year {DynamicResource CurrentYear:yyyy}!");

            Assert.AreEqual("Your name is {0}. Hello {0,-30}, welcome to {1}. It is year {2:yyyy}!", expression.StringFormat);
            Assert.AreEqual(3, expression.Resources.Count);
        }

        [TestMethod]
        public void TestBraceEscapes()
        {
            try
            {
                BoundExpression.Parse("Invalid curly brace { in this sentence.");
                Assert.Fail();
            }
            catch
            {
            }

            var expression = BoundExpression.Parse("Escaped {{Binding Name}} {StaticResource }}N{{a}}me{{}}{{:{{dd/MM/yyyy}}}");
            Assert.AreEqual("Escaped {{Binding Name}} {0:{{dd/MM/yyyy}}}", expression.StringFormat);
            Assert.AreEqual(1, expression.Resources.Count);
            var resource = (StaticResource)expression.Resources[0];
            Assert.AreEqual("}N{a}me{}{", resource.ResourceKey);
        }

        [TestMethod]
        public void TestObjectBinding()
        {
            var expression = BoundExpression.Parse("{Binding} {Binding Test} {ContextBinding}");
            var binding = (PropertyBinding)expression.Resources[0];
            Assert.AreEqual("", binding.PropertyPath);
            var contextBinding = (ContextPropertyBinding)expression.Resources[2];
            Assert.AreEqual("", contextBinding.PropertyPath);
        }

        [TestMethod]
        public void TestGetValueSingleResource()
        {
            var form = new DummyForm
            {
                Value = new List<int> { 1, 2, 3 },
                Context = new List<int> { 1, 2, 3, 4, 5 }
            };

            var expression1 = BoundExpression.Parse("{Binding Count} items.");
            var expression2 = BoundExpression.Parse("{Property Count} items.");
            var expression3 = BoundExpression.Parse("{ContextBinding Count} items.");
            var expression4 = BoundExpression.Parse("{ContextProperty Count} items.");
            var value1 = expression1.GetValue(form).Value;
            var value2 = expression2.GetValue(form).Value;
            var value3 = expression3.GetValue(form).Value;
            var value4 = expression4.GetValue(form).Value;
            var string1 = expression1.GetStringValue(form).Value;
            var string2 = expression2.GetStringValue(form).Value;
            var string3 = expression3.GetStringValue(form).Value;
            var string4 = expression4.GetStringValue(form).Value;
            Assert.AreEqual(3, value1);
            Assert.AreEqual(3, value2);
            Assert.AreEqual(5, value3);
            Assert.AreEqual(5, value4);
            Assert.AreEqual("3 items.", string1);
            Assert.AreEqual("3 items.", string2);
            Assert.AreEqual("5 items.", string3);
            Assert.AreEqual("5 items.", string4);

            var expression = BoundExpression.Parse("{Binding Count}");
            Assert.AreEqual(3, expression.GetValue(form).Value);
            Assert.AreEqual("3", expression.GetStringValue(form).Value);
        }

        [TestMethod]
        public void TestGetValueMultipleResources()
        {
            var form = new DummyForm
            {
                Value = new List<int> { 1, 2, 3 },
                Context = new List<int> { 1, 2, 3, 4, 5 }
            };

            var expression = BoundExpression.Parse("{Binding Count} items from {ContextBinding Count}.");
            Assert.IsNull(expression.GetValue(form).Value);
            Assert.AreEqual("3 items from 5.", expression.GetStringValue(form).Value);
        }

        [TestMethod]
        public void TestContextualResources()
        {
            var proxy = new BindingProxy
            {
                Value = 42
            };

            var expression = BoundExpression.Parse("Input number must be between {MinValue:0.00} and {MaxValue}.",
                new Dictionary<string, Resource>
                {
                    ["MinValue"] = new LiteralValue(15.1125d),
                    ["MaxValue"] = new BindingProxyResource(proxy, true)
                });

            var value = expression.GetStringValue(null);
            Assert.AreEqual("Input number must be between 15.11 and 42.", value.Value);
        }
    }
}
