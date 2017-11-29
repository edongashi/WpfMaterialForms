using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace MaterialForms
{
    internal class ProgressBarSmoother
    {
        public static readonly DependencyProperty SmoothValueProperty = DependencyProperty.RegisterAttached(
            "SmoothValue",
            typeof(double),
            typeof(ProgressBarSmoother),
            new PropertyMetadata(0d, Changing));

        public static double GetSmoothValue(DependencyObject obj)
        {
            return (double) obj.GetValue(SmoothValueProperty);
        }

        public static void SetSmoothValue(DependencyObject obj, double value)
        {
            obj.SetValue(SmoothValueProperty, value);
        }

        private static void Changing(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var anim = new DoubleAnimation((double) e.NewValue, new TimeSpan(0, 0, 0, 0, 500));
            (d as ProgressBar)?.BeginAnimation(RangeBase.ValueProperty, anim, HandoffBehavior.Compose);
        }
    }
}