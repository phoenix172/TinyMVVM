using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace TinyMVVM.Extensions.Markup.Converters
{
    public class BooleanToVisibility : MarkupExtension, IValueConverter
    {
        public bool Invert { get; set; }

        /// <summary>Converts a Boolean value to a <see cref="T:System.Windows.Visibility" /> enumeration value.</summary>
        /// <param name="value">The Boolean value to convert. This value can be a standard Boolean value or a nullable Boolean value.</param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>
        /// <see cref="F:System.Windows.Visibility.Visible" /> if <paramref name="value" /> is <see langword="true" />; otherwise, <see cref="F:System.Windows.Visibility.Collapsed" />.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
                flag = (bool) value;
            else if (value is bool?)
            {
                bool? nullable = (bool?) value;
                flag = nullable.HasValue && nullable.Value;
            }
            if(Invert)
                return (object) (Visibility) (flag ? 2 : 0);
            return (object) (Visibility) (flag ? 0 : 2);
        }

        /// <summary>Converts a <see cref="T:System.Windows.Visibility" /> enumeration value to a Boolean value.</summary>
        /// <param name="value">A <see cref="T:System.Windows.Visibility" /> enumeration value. </param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="value" /> is <see cref="F:System.Windows.Visibility.Visible" />; otherwise, <see langword="false" />.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
                return (object) ((Visibility) value == Visibility.Visible && !Invert);
            return (object) Invert;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}