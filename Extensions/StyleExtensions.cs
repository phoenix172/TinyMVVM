﻿using System;
using System.Windows;
using TinyMVVM.Utilities;

namespace TinyMVVM.Extensions
{
    public static class StyleExtensions
    {
        public static void Merge(this Style style1, Style style2)
        {
            Guard.NotNull(style1, nameof(style1));
            Guard.NotNull(style2, nameof(style2));

            if (style1.TargetType.IsAssignableFrom(style2.TargetType))
            {
                style1.TargetType = style2.TargetType;
            }

            if (style2.BasedOn != null)
            {
                Merge(style1, style2.BasedOn);
            }

            foreach (SetterBase currentSetter in style2.Setters)
            {
                style1.Setters.Add(currentSetter);
            }

            foreach (TriggerBase currentTrigger in style2.Triggers)
            {
                style1.Triggers.Add(currentTrigger);
            }

            foreach (object key in style2.Resources.Keys)
            {
                style1.Resources[key] = style2.Resources[key];
            }
        }
    }
}