using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Word_Cloud.Models;
using Word_Cloud.Service;

namespace Word_Cloud.Commands
{
    class GenerateCloudCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public GenerateCloudCommand(string e)
        {

        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

        }
    }
}
