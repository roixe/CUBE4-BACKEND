using System.Windows.Input;

namespace JamaisASec.Services
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = _ => true; 
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public bool CanExecute(object? parameter)
        {
            return parameter is T param && _canExecute(param);
        }

        public void Execute(object? parameter)
        {
            if (parameter is T param)
            {
                _execute(param);
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}
