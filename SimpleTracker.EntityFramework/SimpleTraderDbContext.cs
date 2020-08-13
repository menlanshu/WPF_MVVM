using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTracker.EntityFramework
{
    public class SimpleTraderDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AssertTransaction> AssertTransactions { get; set; }

        public SimpleTraderDbContext(DbContextOptions options) : base(options) {}

        //migration
        //connection string must start with DataSource for SQLite
        //Generate data base
        //Add-Migration Initial
        //Update-Database => create database
        //Add-Migration Add-Dates
        //Update-Datebase => add several columns

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite(@"DataSource=.\SimpleTrader.db");
        //    base.OnConfiguring(optionsBuilder);
        //}

        //The entity type 'Stock' requires a primary key to be defined. If you intended to use a keyless entity type call 'HasNoKey()'.
        //We got up errors so we need define the relationship in onmodel configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Stock wants to be embedded in AssertTransction table
            modelBuilder.Entity<AssertTransaction>().OwnsOne(a => a.Assert);

            base.OnModelCreating(modelBuilder);
        }

    }
}
