using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaterialForms.Tests
{
    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    [TestClass]
    public class MaterialFormTests
    {
        private MaterialForm GetSimpleForm()
        {
            return new MaterialForm
            {
                new StringSchema
                {
                    Key = "Username",
                    Value = "Value_Username"
                },
                new CaptionSchema
                {
                    Name = "Enter password"
                },
                new StringSchema
                {
                    Key = "Password",
                    Value = "Value_Password"
                }
            };
        }

        [TestMethod]
        public void GetValuesDictionaryTest()
        {
            var form = GetSimpleForm();
            var dictionary = form.GetValuesDictionary();
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual("Value_Username", (string)dictionary["Username"]);
            Assert.AreEqual("Value_Password", (string)dictionary["Password"]);
        }

        [TestMethod]
        public void GetValuesListTest()
        {
            var form = GetSimpleForm();
            var list = form.GetValuesList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Value_Username", (string)list[0]);
            Assert.AreEqual("Value_Password", (string)list[1]);
        }

        [TestMethod]
        public void GetValuesDynamicTest()
        {
            var form = GetSimpleForm();
            var obj = form.GetValuesDynamic();
            Assert.IsTrue(obj.Username == "Value_Username");
            Assert.IsTrue(obj.Password == "Value_Password");
        }

        [TestMethod]
        public void BindNewTest()
        {
            var form = GetSimpleForm();
            var model = form.Bind<LoginModel>();
            Assert.AreEqual("Value_Username", model.Username);
            Assert.AreEqual("Value_Password", model.Password);
        }

        [TestMethod]
        public void BindExistingTest()
        {
            var form = GetSimpleForm();
            var model = new LoginModel();
            form.Bind(model);
            Assert.AreEqual("Value_Username", model.Username);
            Assert.AreEqual("Value_Password", model.Password);
        }
    }
}
