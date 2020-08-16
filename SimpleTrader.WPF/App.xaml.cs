using Microsoft.Extensions.DependencyInjection;
using SimpleTracker.EntityFramework;
using SimpleTracker.EntityFramework.Services;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Test for MajorIndexService
            //new MajorIndexService().GetMajorIndex(Domain.Models.MajorIndexType.DowJones).ContinueWith((task) =>
            //{
            //    var index = task.Result;
            //});

            //IDataService<Account> accountService = new AccountDataService(new SimpleTracker.EntityFramework.SimpleTraderDbContextFactory());
            //IStockPriceService stockService = new StockPriceService();
            //IBuyStockService buyStockService = new BuyStockService(stockService, accountService);

            //Account buyer = await accountService.Get(1);

            //await buyStockService.BuyStock(buyer, "T", 50);


            //Dependency injection
            IServiceProvider serviceProvider = CreateServiceProvider();

            //GetRequiredService will throw exception if no this type service is registered
            //GetService will reply null instead
            //IBuyStockService buyStockService = serviceProvider.GetRequiredService<IBuyStockService>();

            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();


            // A demo for createa a new instance of mainview model via scope
            //using(IServiceScope scope = serviceProvider.CreateScope())
            //{
            //    var differentViewModel = scope.ServiceProvider.GetRequiredService<MainViewModel>();
            //}

            base.OnStartup(e);
        }



        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();


            //Singleton - One instance per application
            //Transient - different instance everytime
            //AddScopd - one instance per "scope"
            services.AddSingleton<SimpleTraderDbContextFactory>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IBuyStockService, BuyStockService>();


            // Add WPF view Model to service provider too
            services.AddScoped<MainViewModel>();
            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<BuyViewModel>();

            services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();


            // Register main window
            // We need use func for this registion becasue we use object as parameter in MainView constructor instead of MainViewModel
            // So we need add a func here to specify the view model
            // We can create a factory for MainViewModel too? Right?
            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();

        }
    }
}
