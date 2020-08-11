using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTracker.EntityFramework
{
    public class SimpleTraderDbContextFactory : IDesignTimeDbContextFactory<SimpleTraderDbContext>
    {
        public SimpleTraderDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();
            options.UseSqlite(@"DataSource=C:\Users\lfu163986\Documents\2-Dev\Test\Csharp\SimpleTrader\SimpleTracker.EntityFramework\SimpleTrader.db");

            return new SimpleTraderDbContext(options.Options);
        }
    }
}
