using EasyTestMaker.Repositories;
using EasyTestMaker.Services;
using EasyTestMaker.UnitOfWork;
using EasyTestMaker.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace EasyTestMaker
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        // Service provider instance.
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            // Configure and build services.
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Configures services for dependency injection.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        private void ConfigureServices(IServiceCollection services)
        {
            // Add services for dependency injection.
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddTransient(typeof(IAPIService<>), typeof(APIService<>));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, EasyTestMaker.UnitOfWork.UnitOfWork>();



            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            
        }

        /// <summary>
        /// Gets a service instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service instance.</returns>
        public static T GetService<T>()
        {
            return (T)_serviceProvider?.GetService(typeof(T));
        }
    
        /// <summary>
        /// Event handler for the startup event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Create and show main window with the main view model as the data context.
            var mainWindow = new MainWindow();
            var viewModel = new MainViewModel();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
        }
    }
}
