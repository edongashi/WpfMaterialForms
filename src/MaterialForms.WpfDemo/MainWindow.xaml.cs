using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
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
            var schema = new StringSchema { Name = "Name", IconKind = PackIconKind.Account };
            bool? result = await WindowFactory.FromSingleSchema("What is your name?", schema).Show();
            if (result == true)
            {
                string name = schema.Value;
                await WindowFactory.Alert($"Hello {name}!").Show();
            }
        }

        private async void ShowModalInput(object sender, RoutedEventArgs e)
        {
            var schema = new StringSchema { Name = "Name", IconKind = PackIconKind.Account };
            bool? result = await DialogFactory.FromSingleSchema("What is your name?", schema).Show("RootDialog", LargeModalWidth);
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
    }
}
