using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TinyMVVM.Extensions
{
    public static class WindowManagerExtensions
    {
        public static void Configure<TViewModel>(this WindowManager windowManager, Action<Window> windowConfigAction)
        {
            windowManager.WindowCreated += (_, window) =>
            {
                if (window.DataContext is TViewModel)
                {
                    windowConfigAction(window);
                }
            };
        }
    }
}
