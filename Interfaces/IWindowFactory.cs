using System.Windows;

namespace TinyMVVM.Interfaces
{
    public interface IWindowFactory
    {
        Window Create<TViewModel>(TViewModel viewModel)
            where TViewModel : class;
    }
}