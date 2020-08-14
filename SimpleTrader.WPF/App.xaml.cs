using SimpleTracker.EntityFramework.Services;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.ViewModels;
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
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Test for MajorIndexService
            //new MajorIndexService().GetMajorIndex(Domain.Models.MajorIndexType.DowJones).ContinueWith((task) =>
            //{
            //    var index = task.Result;
            //});

            IDataService<Account> accountService = new AccountDataService(new SimpleTracker.EntityFramework.SimpleTraderDbContextFactory());
            IStockPriceService stockService = new StockPriceService();
            IBuyStockService buyStockService = new BuyStockService(stockService, accountService);

            Account buyer = await accountService.Get(1);
            
            await buyStockService.BuyStock(buyer, "T", 50);

            //Window window = new MainWindow();
            //window.DataContext = new MainViewModel();
            //window.Show();



            base.OnStartup(e);
        }
    }
}
