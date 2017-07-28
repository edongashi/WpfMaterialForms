// Type: Microsoft.Expression.Interactivity.Layout.FluidMoveBehavior
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Expression.Interactivity.Layout;

namespace Material.Application.Controls
{
    public sealed class FluidMoveBehaviorNoLeak : FluidMoveBehaviorBaseNoLeak
    {
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration",
            typeof(Duration), typeof(FluidMoveBehaviorNoLeak),
            new PropertyMetadata((object)new Duration(TimeSpan.FromSeconds(1.0))));

        public static readonly DependencyProperty InitialTagProperty = DependencyProperty.Register("InitialTag",
            typeof(TagType), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((object)TagType.Element));

        public static readonly DependencyProperty InitialTagPathProperty = DependencyProperty.Register(
            "InitialTagPath", typeof(string), typeof(FluidMoveBehaviorNoLeak),
            new PropertyMetadata((object)string.Empty));

        private static readonly DependencyProperty InitialIdentityTagProperty =
            DependencyProperty.RegisterAttached("InitialIdentityTag", typeof(object), typeof(FluidMoveBehaviorNoLeak),
                new PropertyMetadata((PropertyChangedCallback)null));

        public static readonly DependencyProperty FloatAboveProperty = DependencyProperty.Register("FloatAbove",
            typeof(bool), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((object)true));

        public static readonly DependencyProperty EaseXProperty = DependencyProperty.Register("EaseX",
            typeof(IEasingFunction), typeof(FluidMoveBehaviorNoLeak),
            new PropertyMetadata((PropertyChangedCallback)null));

        public static readonly DependencyProperty EaseYProperty = DependencyProperty.Register("EaseY",
            typeof(IEasingFunction), typeof(FluidMoveBehaviorNoLeak),
            new PropertyMetadata((PropertyChangedCallback)null));

        private static readonly DependencyProperty OverlayProperty = DependencyProperty.RegisterAttached("Overlay",
            typeof(object), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((PropertyChangedCallback)null));

        private static readonly DependencyProperty CacheDuringOverlayProperty =
            DependencyProperty.RegisterAttached("CacheDuringOverlay", typeof(object), typeof(FluidMoveBehaviorNoLeak),
                new PropertyMetadata((PropertyChangedCallback)null));

        private static readonly DependencyProperty HasTransformWrapperProperty =
            DependencyProperty.RegisterAttached("HasTransformWrapper", typeof(bool), typeof(FluidMoveBehaviorNoLeak),
                new PropertyMetadata((object)false));

        private static Dictionary<object, Storyboard> TransitionStoryboardDictionary =
            new Dictionary<object, Storyboard>();

        static FluidMoveBehaviorNoLeak()
        {
        }

        public Duration Duration
        {
            get { return (Duration)this.GetValue(DurationProperty); }
            set { this.SetValue(DurationProperty, (object)value); }
        }

        public TagType InitialTag
        {
            get { return (TagType)this.GetValue(InitialTagProperty); }
            set { this.SetValue(InitialTagProperty, (object)value); }
        }

        public string InitialTagPath
        {
            get { return (string)this.GetValue(InitialTagPathProperty); }
            set { this.SetValue(InitialTagPathProperty, (object)value); }
        }

        public bool FloatAbove
        {
            get { return (bool)this.GetValue(FloatAboveProperty); }
            set { this.SetValue(FloatAboveProperty, value); }
        }

        public IEasingFunction EaseX
        {
            get { return (IEasingFunction)this.GetValue(EaseXProperty); }
            set { this.SetValue(EaseXProperty, (object)value); }
        }

        public IEasingFunction EaseY
        {
            get { return (IEasingFunction)this.GetValue(EaseYProperty); }
            set { this.SetValue(EaseYProperty, (object)value); }
        }

        protected override bool ShouldSkipInitialLayout
        {
            get
            {
                if (!base.ShouldSkipInitialLayout)
                    return this.InitialTag == TagType.DataContext;
                else
                    return true;
            }
        }

        private static object GetInitialIdentityTag(DependencyObject obj)
        {
            return obj.GetValue(InitialIdentityTagProperty);
        }

        private static void SetInitialIdentityTag(DependencyObject obj, object value)
        {
            obj.SetValue(InitialIdentityTagProperty, value);
        }

        private static object GetOverlay(DependencyObject obj)
        {
            return obj.GetValue(OverlayProperty);
        }

        private static void SetOverlay(DependencyObject obj, object value)
        {
            obj.SetValue(OverlayProperty, value);
        }

        private static object GetCacheDuringOverlay(DependencyObject obj)
        {
            return obj.GetValue(CacheDuringOverlayProperty);
        }

        private static void SetCacheDuringOverlay(DependencyObject obj, object value)
        {
            obj.SetValue(CacheDuringOverlayProperty, value);
        }

        private static bool GetHasTransformWrapper(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasTransformWrapperProperty);
        }

        private static void SetHasTransformWrapper(DependencyObject obj, bool value)
        {
            obj.SetValue(HasTransformWrapperProperty, value);
        }

        protected override void EnsureTags(FrameworkElement child)
        {
            base.EnsureTags(child);
            if (this.InitialTag != TagType.DataContext ||
                child.ReadLocalValue(InitialIdentityTagProperty) is BindingExpression)
                return;
            child.SetBinding(InitialIdentityTagProperty, (BindingBase)new Binding(this.InitialTagPath));
        }

        internal override void UpdateLayoutTransitionCore(FrameworkElement child, FrameworkElement root, object tag,
            FluidMoveBehaviorBaseNoLeak.TagData newTagData)
        {
            var flag1 = false;
            var usingBeforeLoaded = false;
            var initialIdentityTag = GetInitialIdentityTag((DependencyObject)child);
            FluidMoveBehaviorBaseNoLeak.TagData tagData1;
            var flag2 = TagDictionary.TryGetValue(tag, out tagData1);
            if (flag2 && tagData1.InitialTag != initialIdentityTag)
            {
                flag2 = false;
                TagDictionary.Remove(tag);
            }
            Rect rect;
            if (!flag2)
            {
                FluidMoveBehaviorBaseNoLeak.TagData tagData2;
                if (initialIdentityTag != null && TagDictionary.TryGetValue(initialIdentityTag, out tagData2))
                {
                    rect = TranslateRect(tagData2.AppRect, root, newTagData.Parent);
                    flag1 = true;
                    usingBeforeLoaded = true;
                }
                else
                    rect = Rect.Empty;
                tagData1 = new FluidMoveBehaviorBaseNoLeak.TagData()
                {
                    ParentRect = Rect.Empty,
                    AppRect = Rect.Empty,
                    Parent = newTagData.Parent,
                    Child = child,
                    Timestamp = DateTime.Now,
                    InitialTag = initialIdentityTag
                };
                TagDictionary.Add(tag, tagData1);
            }
            else if (tagData1.Parent != VisualTreeHelper.GetParent((DependencyObject)child))
            {
                rect = TranslateRect(tagData1.AppRect, root, newTagData.Parent);
                flag1 = true;
            }
            else
                rect = tagData1.ParentRect;
            var originalChild = child;
            if (!IsEmptyRect(rect) && !IsEmptyRect(newTagData.ParentRect) &&
                (!IsClose(rect.Left, newTagData.ParentRect.Left) || !IsClose(rect.Top, newTagData.ParentRect.Top)) ||
                child != tagData1.Child && TransitionStoryboardDictionary.ContainsKey(tag))
            {
                var currentRect = rect;
                var flag3 = false;
                var storyboard = (Storyboard)null;
                if (TransitionStoryboardDictionary.TryGetValue(tag, out storyboard))
                {
                    var overlay1 = GetOverlay((DependencyObject)tagData1.Child);
                    var adornerContainer = (AdornerContainer)overlay1;
                    flag3 = overlay1 != null;
                    var child1 = tagData1.Child;
                    if (overlay1 != null)
                    {
                        var canvas = adornerContainer.Child as Canvas;
                        if (canvas != null)
                            child1 = canvas.Children[0] as FrameworkElement;
                    }
                    if (!usingBeforeLoaded)
                        currentRect = GetTransform(child1).TransformBounds(currentRect);
                    TransitionStoryboardDictionary.Remove(tag);
                    storyboard.Stop();
                    RemoveTransform(child1);
                    if (overlay1 != null)
                    {
                        AdornerLayer.GetAdornerLayer((Visual)root).Remove((Adorner)adornerContainer);
                        TransferLocalValue(tagData1.Child, CacheDuringOverlayProperty, UIElement.RenderTransformProperty);
                        SetOverlay((DependencyObject)tagData1.Child, (object)null);
                    }
                }
                var overlay = (object)null;
                if (flag3 || flag1 && this.FloatAbove)
                {
                    var canvas1 = new Canvas();
                    canvas1.Width = newTagData.ParentRect.Width;
                    canvas1.Height = newTagData.ParentRect.Height;
                    canvas1.IsHitTestVisible = false;
                    var canvas2 = canvas1;
                    var rectangle1 = new Rectangle();
                    rectangle1.Width = newTagData.ParentRect.Width;
                    rectangle1.Height = newTagData.ParentRect.Height;
                    rectangle1.IsHitTestVisible = false;
                    var rectangle2 = rectangle1;
                    rectangle2.Fill = (Brush)new VisualBrush((Visual)child);
                    canvas2.Children.Add((UIElement)rectangle2);
                    var adornerContainer = new AdornerContainer((UIElement)child)
                    {
                        Child = (UIElement)canvas2
                    };
                    overlay = (object)adornerContainer;
                    SetOverlay((DependencyObject)originalChild, overlay);
                    AdornerLayer.GetAdornerLayer((Visual)root).Add((Adorner)adornerContainer);
                    TransferLocalValue(child, UIElement.RenderTransformProperty, CacheDuringOverlayProperty);
                    child.RenderTransform = (Transform)new TranslateTransform(-10000.0, -10000.0);
                    canvas2.RenderTransform = (Transform)new TranslateTransform(10000.0, 10000.0);
                    child = (FrameworkElement)rectangle2;
                }
                var parentRect = newTagData.ParentRect;
                var transitionStoryboard = this.CreateTransitionStoryboard(child, usingBeforeLoaded, ref parentRect,
                    ref currentRect);
                TransitionStoryboardDictionary.Add(tag, transitionStoryboard);
                transitionStoryboard.Completed += (EventHandler)((sender, e) =>
                {
                    Storyboard local_0;
                    if (!TransitionStoryboardDictionary.TryGetValue(tag, out local_0) || local_0 != transitionStoryboard)
                        return;
                    TransitionStoryboardDictionary.Remove(tag);
                    transitionStoryboard.Stop();
                    RemoveTransform(child);
                    child.InvalidateMeasure();
                    if (overlay == null)
                        return;
                    AdornerLayer.GetAdornerLayer((Visual)root).Remove((Adorner)overlay);
                    TransferLocalValue(originalChild, CacheDuringOverlayProperty, UIElement.RenderTransformProperty);
                    SetOverlay((DependencyObject)originalChild, (object)null);
                });
                transitionStoryboard.Begin();
            }
            tagData1.ParentRect = newTagData.ParentRect;
            tagData1.AppRect = newTagData.AppRect;
            tagData1.Parent = newTagData.Parent;
            tagData1.Child = newTagData.Child;
            tagData1.Timestamp = newTagData.Timestamp;
        }

        private Storyboard CreateTransitionStoryboard(FrameworkElement child, bool usingBeforeLoaded,
            ref Rect layoutRect, ref Rect currentRect)
        {
            var duration = this.Duration;
            var storyboard = new Storyboard();
            storyboard.Duration = duration;
            var num1 = !usingBeforeLoaded || layoutRect.Width == 0.0 ? 1.0 : currentRect.Width / layoutRect.Width;
            var num2 = !usingBeforeLoaded || layoutRect.Height == 0.0 ? 1.0 : currentRect.Height / layoutRect.Height;
            var num3 = currentRect.Left - layoutRect.Left;
            var num4 = currentRect.Top - layoutRect.Top;
            AddTransform(child, (Transform)new TransformGroup()
            {
                Children =
                {
                    (Transform)new ScaleTransform()
                    {
                        ScaleX = num1,
                        ScaleY = num2
                    },
                    (Transform)new TranslateTransform()
                    {
                        X = num3,
                        Y = num4
                    }
                }
            });
            var str = "(FrameworkElement.RenderTransform).";
            var transformGroup = child.RenderTransform as TransformGroup;
            if (transformGroup != null && GetHasTransformWrapper((DependencyObject)child))
                str = string.Concat(new object[4]
                {
                    (object)str,
                    (object)"(TransformGroup.Children)[",
                    (object)(transformGroup.Children.Count - 1),
                    (object)"]."
                });
            if (usingBeforeLoaded)
            {
                if (num1 != 1.0)
                {
                    var doubleAnimation1 = new DoubleAnimation();
                    doubleAnimation1.Duration = duration;
                    doubleAnimation1.From = new double?(num1);
                    doubleAnimation1.To = new double?(1.0);
                    var doubleAnimation2 = doubleAnimation1;
                    Storyboard.SetTarget((DependencyObject)doubleAnimation2, (DependencyObject)child);
                    Storyboard.SetTargetProperty((DependencyObject)doubleAnimation2,
                        new PropertyPath(str + "(TransformGroup.Children)[0].(ScaleTransform.ScaleX)", new object[0]));
                    doubleAnimation2.EasingFunction = this.EaseX;
                    storyboard.Children.Add((Timeline)doubleAnimation2);
                }
                if (num2 != 1.0)
                {
                    var doubleAnimation1 = new DoubleAnimation();
                    doubleAnimation1.Duration = duration;
                    doubleAnimation1.From = new double?(num2);
                    doubleAnimation1.To = new double?(1.0);
                    var doubleAnimation2 = doubleAnimation1;
                    Storyboard.SetTarget((DependencyObject)doubleAnimation2, (DependencyObject)child);
                    Storyboard.SetTargetProperty((DependencyObject)doubleAnimation2,
                        new PropertyPath(str + "(TransformGroup.Children)[0].(ScaleTransform.ScaleY)", new object[0]));
                    doubleAnimation2.EasingFunction = this.EaseY;
                    storyboard.Children.Add((Timeline)doubleAnimation2);
                }
            }
            if (num3 != 0.0)
            {
                var doubleAnimation1 = new DoubleAnimation();
                doubleAnimation1.Duration = duration;
                doubleAnimation1.From = new double?(num3);
                doubleAnimation1.To = new double?(0.0);
                var doubleAnimation2 = doubleAnimation1;
                Storyboard.SetTarget((DependencyObject)doubleAnimation2, (DependencyObject)child);
                Storyboard.SetTargetProperty((DependencyObject)doubleAnimation2,
                    new PropertyPath(str + "(TransformGroup.Children)[1].(TranslateTransform.X)", new object[0]));
                doubleAnimation2.EasingFunction = this.EaseX;
                storyboard.Children.Add((Timeline)doubleAnimation2);
            }
            if (num4 != 0.0)
            {
                var doubleAnimation1 = new DoubleAnimation();
                doubleAnimation1.Duration = duration;
                doubleAnimation1.From = new double?(num4);
                doubleAnimation1.To = new double?(0.0);
                var doubleAnimation2 = doubleAnimation1;
                Storyboard.SetTarget((DependencyObject)doubleAnimation2, (DependencyObject)child);
                Storyboard.SetTargetProperty((DependencyObject)doubleAnimation2,
                    new PropertyPath(str + "(TransformGroup.Children)[1].(TranslateTransform.Y)", new object[0]));
                doubleAnimation2.EasingFunction = this.EaseY;
                storyboard.Children.Add((Timeline)doubleAnimation2);
            }
            return storyboard;
        }

        private static void AddTransform(FrameworkElement child, Transform transform)
        {
            var transformGroup = child.RenderTransform as TransformGroup;
            if (transformGroup == null)
            {
                transformGroup = new TransformGroup();
                transformGroup.Children.Add(child.RenderTransform);
                child.RenderTransform = (Transform)transformGroup;
                SetHasTransformWrapper((DependencyObject)child, true);
            }
            transformGroup.Children.Add(transform);
        }

        private static Transform GetTransform(FrameworkElement child)
        {
            var transformGroup = child.RenderTransform as TransformGroup;
            if (transformGroup != null && transformGroup.Children.Count > 0)
                return transformGroup.Children[transformGroup.Children.Count - 1];
            else
                return (Transform)new TranslateTransform();
        }

        private static void RemoveTransform(FrameworkElement child)
        {
            var transformGroup = child.RenderTransform as TransformGroup;
            if (transformGroup == null)
                return;
            if (GetHasTransformWrapper((DependencyObject)child))
            {
                child.RenderTransform = transformGroup.Children[0];
                SetHasTransformWrapper((DependencyObject)child, false);
            }
            else
                transformGroup.Children.RemoveAt(transformGroup.Children.Count - 1);
        }

        private static void TransferLocalValue(FrameworkElement element, DependencyProperty source,
            DependencyProperty dest)
        {
            var obj = element.ReadLocalValue(source);
            var bindingExpressionBase = obj as BindingExpressionBase;
            if (bindingExpressionBase != null)
                element.SetBinding(dest, bindingExpressionBase.ParentBindingBase);
            else if (obj == DependencyProperty.UnsetValue)
                element.ClearValue(dest);
            else
                element.SetValue(dest, element.GetAnimationBaseValue(source));
            element.ClearValue(source);
        }

        private static bool IsClose(double a, double b)
        {
            return Math.Abs(a - b) < 1E-07;
        }

        private static bool IsEmptyRect(Rect rect)
        {
            if (!rect.IsEmpty && !double.IsNaN(rect.Left))
                return double.IsNaN(rect.Top);
            else
                return true;
        }
    }
}
