using System;
using System.Windows.Input;

namespace TPUM.Client.GUI.ViewModel.Commands
{
    internal class RelayCommand : ICommand
    {
        public RelayCommand(Action execute) : this(execute, null!) { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            if (parameter == null)
                return _canExecute();
            return _canExecute();
        }

        public virtual void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler? CanExecuteChanged;

        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
    }
}