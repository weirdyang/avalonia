using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using ToDoList.Services;
using ToDoList.ViewModels;
using ToDoList.Views;

namespace ToDoList
{
    public partial class App : Application
    {
        public App()
        {
            this.Services = ConfigureServices();    
        }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IToDoListService, ToDoListService>();
            services.AddTransient<ToDoListViewModel>();
            services.AddTransient<MainWindowViewModel>();
            return services.BuildServiceProvider();
        }
        public override void OnFrameworkInitializationCompleted()
        {
            var mainViewModel = Services.GetRequiredService<MainWindowViewModel>();
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