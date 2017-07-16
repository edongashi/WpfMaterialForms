using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MaterialForms.Demo.Models;
using Newtonsoft.Json;

namespace MaterialForms.Demo
{
    /// <summary>
    /// Interaction logic for DynamicFormDemo.xaml
    /// </summary>
    public partial class DynamicFormDemo : Window
    {
        public DynamicFormDemo()
        {
            InitializeComponent();
            ModelsList.ItemsSource = GetModels().ToList();
        }

        private IEnumerable<object> GetModels()
        {
            yield return new Person();
        }

        private void Serialize(object sender, RoutedEventArgs e)
        {
            var model = ModelsList.SelectedItem;
            if (model == null)
            {
                JsonTextBox.Text = "";
                return;
            }

            JsonTextBox.Text = JsonConvert.SerializeObject(model, Formatting.Indented);
        }
    }
}
