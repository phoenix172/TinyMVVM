using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TinyMVVM.Interfaces;

namespace TinyMVVM.Extensions
{
    public static class WindowManagerExtensions
    {
        public static void Configure<TViewModel>(this IWindowManager windowManager, Action<Window> windowConfigAction)
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
