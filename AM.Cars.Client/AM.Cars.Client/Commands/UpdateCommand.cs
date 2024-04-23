using AM.Cars.Client.Application.ApiAdapters.Interfaces;
using AM.Cars.Client.Domain.Models;
using AM.Cars.Client.Views;
using System.Windows.Input;

namespace AM.Cars.Client.Commands;

public class UpdateCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Action<object> _execute;

    public UpdateCommand(Action<object> execute) => _execute = execute;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
        _execute(parameter);
    }
}
