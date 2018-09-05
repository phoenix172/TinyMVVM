using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using TinyMVVM.Utilities;

namespace TinyMVVM.Extensions.Markup
{
    [MarkupExtensionReturnType(typeof(Style))]
    public class MultiStyleExtension : MarkupExtension
    {
        private readonly string[] _styleKeys;

        public MultiStyleExtension(string inputStyleKeys)
        {
            Guard.NotNull(inputStyleKeys, nameof(inputStyleKeys));
            _styleKeys = inputStyleKeys.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
        }

        public bool IncludeGlobalStyle { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Style resultStyle = new Style();

            foreach (string currentResourceKey in _styleKeys)
            {
                Style currentStyle = new StaticResourceExtension(currentResourceKey).ProvideValue(serviceProvider) as Style;

                if (currentStyle == null)
                {
                    throw new InvalidOperationException("Could not find style with resource key " + currentResourceKey + ".");
                }

                resultStyle.Merge(currentStyle);
            }

            if(IncludeGlobalStyle)
                MergeGlobalStyle(serviceProvider, resultStyle);

            return resultStyle;
        }

        private void MergeGlobalStyle(IServiceProvider serviceProvider, Style resultStyle)
        {
            var provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var objectType = provideValueTarget?.TargetObject.GetType();
            if (objectType != null &&
                FindGlobalStyle(objectType) is Style globalStyle)
            {
                resultStyle.Merge(globalStyle);
            }
        }

        private Style FindGlobalStyle(Type objectType)
        {
            return Application.Current.TryFindResource(objectType) as Style;
        }
    }
}
