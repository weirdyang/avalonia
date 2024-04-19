using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IRecipient<ChangeViewMessage>
    {
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

        public ToDoListViewModel ToDoList { get; }
        public MainWindowViewModel()
        {
            ToDoList = Ioc.Default.GetRequiredService<ToDoListViewModel>();
            _contentViewModel = ToDoList;
            WeakReferenceMessenger.Default.Register(this);
        }
        private ObservableRecipient _contentViewModel;
        public ObservableRecipient ContentViewModel
        {
            get => _contentViewModel;
            private set => this.SetProperty(ref _contentViewModel, value);
        }

        public void AddItem()
        {
            ContentViewModel = new AddItemViewModel();
        }

        public void Receive(ChangeViewMessage message)
        {
            if (message.Value)
            {
                ContentViewModel = ToDoList;
            }
        }
    }
}
