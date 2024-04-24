using System.Windows.Input;

namespace AM.Cars.Client.Commands;

public class SaveCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Func<Task> _execute;

    public SaveCommand(Func<Task> execute) => _execute = execute;

    public bool CanExecute(object parameter) => true;

    public async void Execute(object parameter)
    {
        await _execute();
    }
}
