using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Word_Cloud.Commands;

namespace Word_Cloud.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel()
        {
            _SelectedViewModel = new TitlePageViewModel();
            GoToWordCloudCommand = new GoToWordCloudCommand(SelectedViewModel);
        }
        public ICommand GoToWordCloudCommand { get; set; }
        private ViewModelBase _SelectedViewModel;
        public ViewModelBase SelectedViewModel
        {
            get
            {
                return _SelectedViewModel;
            }
            set
            {
                _SelectedViewModel = value;
                Debug.WriteLine("ViewModel Changed.");
                NotifyPropertyChanged();
            }
        }
    }
}
