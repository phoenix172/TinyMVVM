using System.Windows;
using TinyMVVM.Interfaces;

namespace TinyMVVM
{
    public class DefaultWindowFactory : IWindowFactory
    {
        public Window Create<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            var window = CreateInstance();
            SetProperties(window, viewModel);
            return window;
        }

        protected virtual Window CreateInstance()
        {
            return new Window();
        }

        protected virtual void SetProperties<TViewModel>(Window window, TViewModel viewModel)
            where TViewModel : class
        {
            window.DataContext = viewModel;
            window.Content = viewModel;
            window.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}