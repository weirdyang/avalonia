using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;

namespace PhotoBrowser.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

        [ObservableProperty]
        private List<string> names;

        public MainWindowViewModel()
        {
            this.Names = new List<string>()
            {
                "TEST1", "TEST2", "TEST3"
            };
            this.CurrentName = this.Names[0];
            this.CurrentIndex = 0;
        }

        [ObservableProperty]
        private string currentName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(NavigateNextCommand))]
        [NotifyCanExecuteChangedFor(nameof(NavigatePreviousCommand))]
        private int currentIndex;
        private bool IsNotFirstItem()
        {
            return CurrentIndex != 0;
        }

        private bool IsNotLastItem()
        {
            return CurrentIndex != Names.Count - 1;
        }

        [RelayCommand(CanExecute = nameof(IsNotFirstItem))]
        private void NavigatePrevious()
        {
            this.CurrentIndex = CurrentIndex == 0 ? 0 : CurrentIndex - 1;
            this.CurrentName = Names[CurrentIndex];

        }
        [RelayCommand(CanExecute = nameof(IsNotLastItem))]
        private void NavigateNext()
        {
            this.CurrentIndex = CurrentIndex == Names.Count - 1 ? CurrentIndex : CurrentIndex + 1;
            this.CurrentName = Names[CurrentIndex];
        }
    }
}
