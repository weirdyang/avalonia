using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using ToDoList.Services;
using ToDoList.ViewModels;
using ToDoList.Views;

namespace ToDoList
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App? Current => Application.Current as App;


        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private void ConfigureServices()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<IToDoListService, ToDoListService>()
                .AddTransient<ToDoListViewModel>()
                .AddTransient<MainWindowViewModel>()
                .BuildServiceProvider());

        }
        public override void OnFrameworkInitializationCompleted()
        {
            ConfigureServices();

            var mainViewModel = Ioc.Default.GetRequiredService<MainWindowViewModel>();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainViewModel
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}