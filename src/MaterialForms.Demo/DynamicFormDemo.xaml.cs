using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MaterialForms.Demo.Models;

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
    }
}
