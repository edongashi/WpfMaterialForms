using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class ProgressSchema : SchemaBase
    {
        private bool isIndeterminate;
        private int maximum = 100;
        private double progress;
        private bool showAbsolute;
        private bool showPercentage = true;

        public double Progress
        {
            get => progress;
            set
            {
                if (value == progress) return;
                progress = value;
                OnPropertyChanged();
            }
        }

        public int Maximum
        {
            get => maximum;
            set
            {
                if (value <= 0 || value == maximum) return;
                maximum = value;
                OnPropertyChanged();
            }
        }

        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set
            {
                if (value == isIndeterminate) return;
                isIndeterminate = value;
                OnPropertyChanged();
            }
        }

        public bool ShowPercentage
        {
            get => showPercentage;
            set
            {
                if (value == showPercentage) return;
                showPercentage = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAbsolute
        {
            get => showAbsolute;
            set
            {
                if (value == showAbsolute) return;
                showAbsolute = value;
                OnPropertyChanged();
            }
        }

        public override bool HoldsValue => false;

        public override UserControl CreateView()
        {
            return new ProgressControl
            {
                DataContext = this
            };
        }

        public override object GetValue()
        {
            return Progress;
        }

        public override void SetValue(object obj)
        {
            if (obj is double)
            {
                Progress = (double) obj;
            }
            else if (obj is int)
            {
                Progress = (int) obj;
            }
            else if (obj is string)
            {
                double result;
                double.TryParse((string) obj, out result);
                Progress = result;
            }
        }
    }
}