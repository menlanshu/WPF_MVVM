using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public class BuyStockService : IBuyStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;

        /// <summary>
        /// We need to get the price of the stock
        /// We need save trasaction to db
        /// We need re-calculate the balance that remained of the buyer
        /// So we need IDataService and IStockPriceService
        /// </summary>
        /// <param name="stockPriceService"></param>
        /// <param name="accountService"></param>
        public BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
        {
            //get the price of our stock
            double stockPrice = await _stockPriceService.GetPrice(symbol);

            double transactionPrice = stockPrice * shares;

            // Check balance is sufficient or not
            if(transactionPrice > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionPrice);
            }

            //Create a new assert transaction
            //Id will be generated automatically when we insert into database
            AssertTransaction transaction = new AssertTransaction()
            {
                Account = buyer,
                Assert = new Assert()
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.Now,
                Shares = shares,
                IsPurchase = true
            };

            //Add transaction to our buyer
            buyer.AssertTransactions.Add(transaction);
            buyer.Balance -= transactionPrice;

            // Update account info to DB
            await _accountService.Update(buyer.Id, buyer);

            return buyer;

        }
    }
}
