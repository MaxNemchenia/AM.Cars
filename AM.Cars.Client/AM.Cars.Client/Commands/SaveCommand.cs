﻿using System.Windows.Input;

namespace AM.Cars.Client.Commands;

public class SaveCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Action<object> _execute;

    public SaveCommand(Action<object> execute) => _execute = execute;

    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
        _execute(parameter);
    }
}
