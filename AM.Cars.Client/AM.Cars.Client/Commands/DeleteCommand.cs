using System.Windows.Input;

namespace AM.Cars.Client.Commands;

public class DeleteCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Action<object> _execute;

    public DeleteCommand(Action<object> execute) => _execute = execute;

    public bool CanExecute(object parameter) => true;

    public async void Execute(object parameter)
    {
        _execute(parameter);
    }
}
