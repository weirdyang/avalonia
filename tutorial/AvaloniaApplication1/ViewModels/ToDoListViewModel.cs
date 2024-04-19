using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoList.DataModel;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class ToDoListViewModel : ObservableRecipient, IRecipient<NewToDoItemMessage>
    {
        private IToDoListService _toDoListService;
        public ToDoListViewModel(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
            ListItems = new ObservableCollection<ToDoItem>(toDoListService.GetItems());
            WeakReferenceMessenger.Default.Register(this);
        }

        public ObservableCollection<ToDoItem> ListItems { get; }

        public void Receive(NewToDoItemMessage message)
        {
            this.ListItems.Add(message.Value);
        }
    }

    public class ChangeViewMessage : ValueChangedMessage<bool>
    {
        public ChangeViewMessage(bool v): base(v)
        {
        }
    }
    public class NewToDoItemMessage : ValueChangedMessage<ToDoItem>
    {
        public NewToDoItemMessage(ToDoItem value) : base(value)
        {
        }
    }
}