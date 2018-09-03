using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TinyMVVM.Commands
{
    public class TabItemCollection : Collection<TabItem>
    {
    }

    public class ToolbarItem
    {
        public static readonly DependencyProperty DisplayFilterProperty =
            DependencyProperty.RegisterAttached(
                "DisplayFilterInternal", // Shadow the name so the parser does not skip GetDisplayFilter
                typeof(TabItemCollection),
                typeof(ToolbarItem));

        public static TabItemCollection GetDisplayFilter(Control item)
        {
            var collection = (TabItemCollection)item.GetValue(DisplayFilterProperty);
            if (collection == null) {
                collection = new TabItemCollection();
                item.SetValue(DisplayFilterProperty, collection);
            }
            return collection;
        }

        // Optional, see above note
        //public static void SetDisplayFilter(Control item, TabItemCollection value)
        //{
        //    item.SetValue(DisplayFilterProperty, value);
        //}
    }
}
