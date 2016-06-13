using System;
using System.Collections.ObjectModel;
using MaterialForms;

namespace MaterialFormsDemo
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var window = new MaterialWindow()
            {
                Dialog = new MaterialDialog
                {
                    Title = "Log in to continue",
                    Form = new MaterialForm
                    {
                        new StringSchema
                        {
                            Name = "Username",
                            IconKind = IconKind.Account
                        },
                        new PasswordSchema
                        {
                            Name = "Password",
                            IconKind = IconKind.Key
                        }
                    }
                }
            };

            window.Show();
        }
    }
}
