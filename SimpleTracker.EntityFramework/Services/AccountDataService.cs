using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTracker.EntityFramework.Services.Common;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTracker.EntityFramework.Services
{
    public class AccountDataService : NonQueryDataService<Account>, IDataService<Account>
    {
        public AccountDataService(SimpleTraderDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<Account> Get(int id)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                Account entity = await context.Accounts.Include(x => x.AssertTransactions).FirstOrDefaultAsync((e) => e.Id == id);

                return entity;
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Account> entities = await context.Accounts.Include(x => x.AssertTransactions).ToListAsync();

                return entities;
            }
        }
    }
}
