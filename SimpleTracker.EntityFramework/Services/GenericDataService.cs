using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SimpleTracker.EntityFramework.Services.Common;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTracker.EntityFramework.Services
{
    public class GenericDataService<T> : NonQueryDataService<T>, IDataService<T> where T : DomainObject
    {
        public GenericDataService(SimpleTraderDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<T> Get(int id)
        {
            using(SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);

                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using(SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();

                return entities;
            }
        }
    }
}
