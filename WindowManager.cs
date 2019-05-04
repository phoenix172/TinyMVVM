using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TinyMVVM.Interfaces;

namespace TinyMVVM
{
    public class WindowManager
    {
        private readonly ConcurrentBag<Window> _windows = new ConcurrentBag<Window>();
        // ReSharper disable once SuspiciousTypeConversion.Global

		/// <summary>
		/// The <see cref="Window"/> objects owned by this <see cref="WindowManager"/>.
		/// </summary>
        public IReadOnlyCollection<Window> Windows => (IReadOnlyCollection<Window>)_windows;

		/// <summary>
		/// Creates a <see cref="WindowManager"/> with <see cref="WindowFactory"/> set to a <see cref="DefaultWindowFactory"/> instance.
		/// </summary>
        public WindowManager()
            :this(new DefaultWindowFactory())
        {
            
        }

		/// <summary>
		/// Creates a <see cref="WindowManager"/> with the supplied <see cref="IWindowFactory"/> implementation.
		/// </summary>
		/// <param name="windowFactory">The <see cref="IWindowFactory"/> implementation.</param>
        public WindowManager(IWindowFactory windowFactory)
        {
            WindowFactory = windowFactory;
        }

		/// <summary>
		/// The window factory used to create instances of <see cref="Window"/>, if no type is specified explicitly.
		/// </summary>
        public IWindowFactory WindowFactory { get; set; }

        /// <summary>
        /// Creates a <see cref="Window"/> using the supplied <see cref="WindowFactory"/> and sets its
        /// DataContext and Content to the supplied <paramref name="viewModel"/>.
        /// </summary>
        /// <typeparam name="TViewModel">The type of the <paramref name="viewModel"/></typeparam>
        /// <param name="viewModel">The viewModel instance</param>
		public void Show<TViewModel>(TViewModel viewModel) 
            where TViewModel : class
        {
            var window = CreateWindow(viewModel);
            window.Show();
        }

		/// <summary>
		/// Creates an instance of the specified <typeparamref name="TWindow"/> type and sets its
		/// DataContext to the supplied <paramref name="viewModel"/>.
		/// </summary>
		/// <typeparam name="TViewModel">The type of the <paramref name="viewModel"/></typeparam>
		/// <typeparam name="TWindow">The type of the <see cref="Window"/> to instantiate. Needs to have a public parameterless constructor.</typeparam>
		/// <param name="viewModel">The viewModel instance</param>
		public void Show<TViewModel,TWindow>(TViewModel viewModel)
	        where TWindow : Window, new()
        {
	        var window = new TWindow();
	        window.DataContext = viewModel;
	        window.Show();
        }

		/// <summary>
		/// Creates a <see cref="Window"/> using the supplied <see cref="WindowFactory"/>, sets its
		/// DataContext and Content to the supplied <paramref name="viewModel"/> and configures it as a dialog.
		/// </summary>
		/// <typeparam name="TViewModel">The type of the <paramref name="viewModel"/></typeparam>
		/// <param name="viewModel">The viewModel instance</param>
		public bool? ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : class
        {
            var window = CreateDialogWindow(viewModel);
            return window.ShowDialog();
        }

		/// <summary>
		/// Creates an instance of the specified <typeparamref name="TDialogWindow"/> type, sets its
		/// DataContext to the supplied <paramref name="viewModel"/> and configures it as a dialog.
		/// </summary>
		/// <typeparam name="TViewModel">The type of the <paramref name="viewModel"/></typeparam>
		/// <typeparam name="TDialogWindow">The type of the <see cref="Window"/> to instantiate. Needs to have a public parameterless constructor.</typeparam>
		/// <param name="viewModel">The viewModel instance</param>
		public void ShowDialog<TViewModel, TDialogWindow>(TViewModel viewModel)
	        where TDialogWindow : Window, new()
        {
	        var window = new TDialogWindow();
	        window.DataContext = viewModel;
	        window.ShowDialog();
        }

		/// <summary>
		/// Closes the window corresponding to the passed <paramref name="viewModel"/> instance
		/// </summary>
		/// <typeparam name="TViewModel">The type of the <paramref name="viewModel"/></typeparam>
		/// <param name="viewModel">The viewModel instance</param>
		/// <param name="dialogResult">The dialog result to return from the window</param>
		public void CloseWindow<TViewModel>(TViewModel viewModel, bool? dialogResult = null)
            where TViewModel : class
        {
            var window = Windows.FirstOrDefault(x => (x.DataContext as TViewModel)==viewModel);
            window.DialogResult = dialogResult;
            window.Close();
        }

        private Window CreateDialogWindow<TViewModel>(TViewModel viewModel)
            where TViewModel : class
        {
            var window = CreateWindow(viewModel);
            window.ResizeMode = ResizeMode.NoResize;
            return window;
        }

        private Window CreateWindow<TViewModel>(TViewModel viewModel)
            where TViewModel : class
        {
            var window = WindowFactory.Create(viewModel);
            BindClosable(viewModel);
            _windows.Add(window);
            WindowCreated?.Invoke(this, window);
            return window;
        }

        private void BindClosable<TViewModel>(TViewModel viewModel) where TViewModel : class
        {
            if (viewModel is ICanClose closable)
                closable.CloseRequested += (sender, dialogResult)
                    => CloseWindow(viewModel, dialogResult);
        }

		/// <summary>
		/// Raised every time a window is created. Can be used for configuring window properties after creation.
		/// </summary>
        public event EventHandler<Window> WindowCreated;
    }
}