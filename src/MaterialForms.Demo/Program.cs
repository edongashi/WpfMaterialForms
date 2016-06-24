using System;
using System.Collections.ObjectModel;
using MaterialForms;

namespace MaterialForms.Demo
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var window = new MaterialWindow();
            window.Dialog = new MaterialDialog
            {
                Title = "Demo material forms",
                Form = new MaterialForm
                {
                    new CommandSchema
                    {
                        Name = "Login window",
                        CommandHint = "SHOW",
                        Callback = ShowLogin
                    },
                    new CommandSchema
                    {
                        Name = "Settings window",
                        CommandHint = "SHOW",
                        Callback = ShowDemo(SettingsDialog)
                    },
                    new CommandSchema
                    {
                        Name = "Dialog inside window",
                        CommandHint = "SHOW",
                        Callback = async arg =>
                        {
                            await window.CurrentSession.ShowDialog(new MaterialDialog
                            {
                                Message = "Discard draft?",
                                PositiveAction = "DISCARD"
                            }, 250d);
                        }
                    },
                    new CommandSchema
                    {
                        Name = "E-mail window",
                        CommandHint = "SHOW",
                        Callback = ShowDemo(EmailDialog)
                    }
                }
            };

            window.Show();
            MaterialWindow.RunDispatcher();
        }

        public static async void ShowLogin(object nothing)
        {
            var window = new MaterialWindow(new MaterialDialog
            {
                Title = "Please log in to continue",
                PositiveAction = "LOG IN",
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
                    },
                    new BooleanSchema
                    {
                        Name = "Remember me",
                        IsCheckBox = true
                    }
                }
            });

            bool? result = await window.Show();
            if (result == true)
            {
                var formData = window.Dialog.Form.GetValuesList();
                await WindowFactory.Alert($"Username: {formData[0]}\nPassword: {formData[1]}\nRemember me: {formData[2]}", "Positive").Show();
            }
            else
            {
                await WindowFactory.Alert("Dialog dismissed", "Negative").Show();
            }
        }

        public static Action<object> ShowDemo(Func<MaterialDialog> dialog)
        {
            Action<object> callback = arg =>
            {
                var window = new MaterialWindow
                {
                    Dialog = dialog(),
                };

                window.Show();
            };

            return callback;
        }

        public static MaterialDialog SettingsDialog()
        {
            return new MaterialDialog
            {
                Title = "Settings",
                Form = new MaterialForm
                {
                    new CaptionSchema
                    {
                        Name = "Connectivity"
                    },
                    new BooleanSchema
                    {
                        Name = "WiFi",
                        IconKind = IconKind.Wifi,
                        Value = true
                    },
                    new BooleanSchema
                    {
                        Name = "Mobile Data",
                        IconKind = IconKind.Signal
                    },
                    new CaptionSchema
                    {
                        Name = "Device"
                    },
                    new NumberRangeSchema
                    {
                        Name = "Volume",
                        IconKind = IconKind.VolumeHigh,
                        MinValue = 0,
                        MaxValue = 10,
                        Value = 5
                    },
                    new KeyValueSchema
                    {
                        Name = "Ringtone",
                        Value = "Over the horizon",
                        IconKind = IconKind.MusicNote
                    }
                }
            };
        }

        public static MaterialDialog EmailDialog()
        {
            return new MaterialDialog
            {
                Title = "Send e-mail",
                PositiveAction = "SEND",
                Form = new MaterialForm
                {
                    new StringSchema
                    {
                        Name = "To",
                        IconKind = IconKind.Email
                    },
                    new StringSchema
                    {
                        Name = "Message",
                        IsMultiLine = true,
                        IconKind = IconKind.Comment
                    }
                }
            };
        }
    }
}
