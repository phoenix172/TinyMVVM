using System;
using System.Windows;

namespace TinyMVVM.Commands
{
    public static class Mvvm
    {
        public static readonly DependencyProperty CommandBindingsProperty = DependencyProperty.RegisterAttached(
            "CommandBindings", typeof(MvvmCommandBindingCollection), typeof(Mvvm),
            new PropertyMetadata(null, OnCommandBindingsChanged));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static MvvmCommandBindingCollection GetCommandBindings(UIElement target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            return (MvvmCommandBindingCollection)target.GetValue(CommandBindingsProperty);
        }

        public static void SetCommandBindings(UIElement target, MvvmCommandBindingCollection value)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            target.SetValue(CommandBindingsProperty, value);
        }

        private static void OnCommandBindingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is UIElement uiDependencyObject)) return;

            if (e.OldValue is MvvmCommandBindingCollection oldValue)
            {
                oldValue.DettachFrom(uiDependencyObject);
            }

            if (e.NewValue is MvvmCommandBindingCollection newValue)
            {
                newValue.AttachTo(uiDependencyObject);
            }
        }
    }
}