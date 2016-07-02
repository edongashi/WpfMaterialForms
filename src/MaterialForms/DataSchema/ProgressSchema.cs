using System.Windows.Controls;
using MaterialForms.Controls;

namespace MaterialForms
{
    public class ProgressSchema : SchemaBase
    {
        private double progress;
        private int maximum = 100;
        private bool isIndeterminate;
        private bool showAbsolute;
        private bool showPercentage = true;

        public double Progress
        {
            get { return progress; }
            set
            {
                if (value == progress) return;
                progress = value;
                OnPropertyChanged();
            }
        }

        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value <= 0 || value == maximum) return;
                maximum = value;
                OnPropertyChanged();
            }
        }

        public bool IsIndeterminate
        {
            get { return isIndeterminate; }
            set
            {
                if (value == isIndeterminate) return;
                isIndeterminate = value;
                OnPropertyChanged();
            }
        }

        public bool ShowPercentage
        {
            get { return showPercentage; }
            set
            {
                if (value == showPercentage) return;
                showPercentage = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAbsolute
        {
            get { return showAbsolute; }
            set
            {
                if (value == showAbsolute) return;
                showAbsolute = value;
                OnPropertyChanged();
            }
        }

        public override UserControl CreateView()
        {
            return new ProgressControl
            {
                DataContext = this
            };
        }

        public override bool HoldsValue => false;

        public override object GetValue() => Progress;
    }
}
