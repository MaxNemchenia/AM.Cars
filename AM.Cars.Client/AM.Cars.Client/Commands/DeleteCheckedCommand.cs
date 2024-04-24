using System.Windows.Input;

namespace AM.Cars.Client.Commands;

public class DeleteCheckedCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Action _execute;

    public DeleteCheckedCommand(Action execute) => _execute = execute;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
        _execute();
    }
}
