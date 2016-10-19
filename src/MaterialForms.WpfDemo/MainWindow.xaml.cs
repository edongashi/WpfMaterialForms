using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using MaterialForms.Extensions;
using MaterialForms.Tasks;

namespace MaterialForms.WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const double ModalWidth = 250d;
        public const double LargeModalWidth = 350d;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ShowAlert(object sender, RoutedEventArgs e)
        {
            await WindowFactory.Alert("Hello World!").Show();
        }

        private async void ShowModalAlert(object sender, RoutedEventArgs e)
        {
            await DialogFactory.Alert("Hello World!").Show("RootDialog", ModalWidth);
        }

        private async void ShowPrompt(object sender, RoutedEventArgs e)
        {
            bool? result = await WindowFactory.Prompt("Delete item?").Show();
            if (result == true)
            {
                await WindowFactory.Alert("Item deleted.").Show();
            }
        }

        private async void ShowModalPrompt(object sender, RoutedEventArgs e)
        {
            bool? result = await DialogFactory.Prompt("Delete item?").Show("RootDialog", ModalWidth);
            if (result == true)
            {
                await DialogFactory.Alert("Item deleted.").Show("RootDialog", ModalWidth);
            }
        }

        private async void ShowInput(object sender, RoutedEventArgs e)
        {
            StringSchema schema = new StringSchema { Name = "Name", IconKind = PackIconKind.Account };
            bool? result = await WindowFactory.FromSingleSchema("What is your name?", schema).Show();
            if (result == true)
            {
                string name = schema.Value;
                await WindowFactory.Alert($"Hello {name}!").Show();
            }
        }

        private async void ShowModalInput(object sender, RoutedEventArgs e)
        {
            StringSchema schema = new StringSchema { Name = "Name", IconKind = PackIconKind.Account };
            bool? result =
                await DialogFactory.FromSingleSchema("What is your name?", schema).Show("RootDialog", LargeModalWidth);
            if (result == true)
            {
                string name = schema.Value;
                await DialogFactory.Alert($"Hello {name}!").Show("RootDialog", ModalWidth);
            }
        }

        private async void ShowProgress(object sender, RoutedEventArgs e)
        {
            await TaskRunner.Run(async controller =>
            {
                for (var i = 1; i <= 100; i++)
                {
                    controller.Progress = i;
                    await Task.Delay(50);
                }
            }, new ProgressDialogOptions
            {
                Message = "Processing data..."
            });
        }

        private async void ShowModalProgress(object sender, RoutedEventArgs e)
        {
            await TaskRunner.Run(async controller =>
            {
                for (var i = 1; i <= 100; i++)
                {
                    controller.Progress = i;
                    await Task.Delay(50);
                }
            }, new ProgressDialogOptions
            {
                Message = "Processing data..."
            }, "RootDialog");
        }

        private async void ShowMultiSchema(object sender, RoutedEventArgs e)
        {
            var dialog = new MaterialDialog
            {
                Form = new MaterialForm
                {
                    new CaptionSchema { Name = "Personal details" },
                    new MultiSchema(
                        new StringSchema { Name = "First Name", Key = "first", IconKind = PackIconKind.Account },
                        new StringSchema { Name = "Last Name", Key = "last" })
                    {
                        Key = "PersonalDetails"
                    },
                    new CaptionSchema { Name = "Reservation details" },
                    new MultiSchema(
                        new DateSchema { Name = "Date", IconKind = PackIconKind.CalendarClock },
                        new TimeSchema { Name = "Time" })
                    {
                        RelativeColumnWidths = new[] { 6d, 4d }
                    }
                }
            };

            bool? result = await dialog.Show("RootDialog", LargeModalWidth);
            if (result == true)
            {
                MaterialForm personalDetails = (MaterialForm)dialog.Form["PersonalDetails"];
                string firstName = (string)personalDetails["first"];
                string lastName = (string)personalDetails["last"];
                await DialogFactory.Alert($"Hello {firstName} {lastName}!").Show("RootDialog", ModalWidth);
            }
        }

        private async void ShowLightTheme(object sender, RoutedEventArgs e)
        {
            await DialogFactory
                .Alert("Light dialog independent of application resources")
                .With(dialog => dialog.Theme = DialogTheme.Light)
                .Show("RootDialog", LargeModalWidth);
        }

        private async void ShowDarkTheme(object sender, RoutedEventArgs e)
        {
            await DialogFactory
                .Alert("Dark dialog independent of application resources")
                .With(dialog => dialog.Theme = DialogTheme.Dark)
                .Show("RootDialog", LargeModalWidth);
        }

        private void DarkModeChecked(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().SetLightDark(true);
        }

        private void DarkModeUnchecked(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().SetLightDark(false);
        }
    }
}
