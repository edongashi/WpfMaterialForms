using System;
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
        //static DynamicFormDemo()
        //{
        //    AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        //    {
        //        MessageBox.Show("An unhandled exception occured. Exception details copied to clipboard.");
        //        Clipboard.SetText(e.ExceptionObject.ToString());
        //    };
        //}

        public DynamicFormDemo()
        {
            InitializeComponent();
            ModelsList.ItemsSource = GetModels().ToList();
        }

        private IEnumerable<object> GetModels()
        {
            yield return new Person();
            yield return new DataTypes();
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

            Form.ReloadElements();
        }
    }
}
