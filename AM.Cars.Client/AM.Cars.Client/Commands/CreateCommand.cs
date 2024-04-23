using System.Windows.Input;

namespace AM.Cars.Client.Commands;

public class CreateCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Action _execute;

    public CreateCommand(Action execute) => _execute = execute;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
        _execute();
    }
}

