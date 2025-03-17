using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JamaisASec.Services
{
    public class RelayCommandAsync<T> : ICommand
    {
        private readonly Func<T, Task> _executeAsync;
        private readonly Predicate<T>? _canExecute;

        public RelayCommandAsync(Func<T, Task> executeAsync, Predicate<T>? canExecute = null)
        {
            _executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || parameter is T t && _canExecute(t);
        }

        public async void Execute(object? parameter)
        {
            if (parameter is T t)
            {
                await _executeAsync(t);
            }
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}