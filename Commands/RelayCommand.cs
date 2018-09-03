using System;
using System.Windows.Input;

namespace TinyMVVM.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(T parameter)
        {
            return this._canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(T parameter)
        {
            _execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T) parameter);
        }

        void ICommand.Execute(object parameter)
        {
            _execute((T) parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute, Func<bool> canExecute = null) 
            : base((param)=>execute(), (param)=>canExecute?.Invoke() ?? true)
        {
        }
    }
}
