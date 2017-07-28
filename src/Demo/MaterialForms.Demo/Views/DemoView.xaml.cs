using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialForms.Demo.Models;
using MaterialForms.Wpf;
using Newtonsoft.Json;

namespace MaterialForms.Demo.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class DemoView : UserControl
    {
        public DemoView()
        {
            InitializeComponent();
            ModelsList.ItemsSource = GetModels().ToList();
        }

        private IEnumerable<object> GetModels()
        {
            yield return new User();
            yield return new Login();
            yield return new DataTypes();
            yield return new Settings();
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

        private void FormThemeChanged(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
            {
                return;
            }

            Application.Current.Resources.MergedDictionaries.Clear();
            string source;
            if (MaterialDesignTheme.IsChecked == true)
            {
                source = "Themes/Material.xaml";
            }
            else if (MahappsMetroTheme.IsChecked == true)
            {
                source = "Themes/Metro.xaml";
            }
            else if (WpfTheme.IsChecked == true)
            {
                source = "Themes/Wpf.xaml";
            }
            else
            {
                throw new InvalidOperationException();
            }

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(source, UriKind.Relative)
            });
        }

        private void OnValidateClick(object sender, RoutedEventArgs e)
        {
            ModelState.Validate(Form.Model);
        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            ModelState.Reset(Form.Model);
        }

    }
}
