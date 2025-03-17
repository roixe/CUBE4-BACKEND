using System.Windows.Input;

namespace JamaisASec.Services
{
    public class RelayCommandAsync : ICommand
    {
        private readonly Action _execute;

        public RelayCommandAsync(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _execute();
        }

        public event EventHandler? CanExecuteChanged;
    }
}