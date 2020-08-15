using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public interface IBuyStockService
    {
        /// <summary>
        /// We define a method to buy stock
        /// We need a buyer and what stock you want to buy and how many you want to buy
        /// All those methods and such kind of definition should be defined in the design layer
        /// Then everything should be easier to set up
        /// Because you just need to see the design document, and copy all the method/field/property from it
        /// </summary>
        /// <param name="buyer"></param>
        /// <param name="stock"></param>
        /// <param name="shares"></param>
        /// <returns></returns>
        Task<Account> BuyStock(Account buyer, string stock, int shares);
    }
}
