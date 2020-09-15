using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Word_Cloud.ViewModels;

namespace Word_Cloud.Commands
{
    class GoToWordCloudCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        
        public GoToWordCloudCommand(ViewModelBase vm)
        {

        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            (parameter as MainViewModel).SelectedViewModel = new TextBodyViewModel();
            Debug.WriteLine(parameter.GetType());
        }

    }
}
