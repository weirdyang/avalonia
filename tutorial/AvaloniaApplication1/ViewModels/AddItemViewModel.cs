using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Windows.Input;
using ToDoList.DataModel;

namespace ToDoList.ViewModels
{
    public partial class AddItemViewModel : ObservableRecipient
    {

        public string Description 
        { 
            get => _description; 
            set => this.SetProperty(ref _description, value); 
        }

        private string _description = String.Empty;

        [RelayCommand(CanExecute = nameof(NotEmptyDescription))]
        private void CreateNewToDo(string description)
        {
            var newItem = new ToDoItem
            {
                Description = description,
                IsChecked = false
            };
            WeakReferenceMessenger.Default.Send(new NewToDoItemMessage(newItem));
            Description = string.Empty;
        }
        private bool NotEmptyDescription(string description)
        {
            return !string.IsNullOrWhiteSpace(description);
        }
        [RelayCommand]
        private void Cancel()
        {
            WeakReferenceMessenger.Default.Send(new ChangeViewMessage(true));
        }


    }

}