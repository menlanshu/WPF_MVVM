using SimpleTracker.EntityFramework;
using SimpleTracker.EntityFramework.Services;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Linq;
using System.Reflection.Metadata;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataService<User> userService = new GenericDataService<User>(new SimpleTraderDbContextFactory());
            Console.WriteLine($"Current count is {userService.GetAll().Result.Count()}");
            Console.WriteLine($"Current count is {userService.Get(1).Result.UserName}");

            userService.Create(new User { UserName = "Test2" }).Wait();
            userService.Update(1, new User { UserName = "TestUpdate" }).Wait();
            userService.Delete(1).Wait();

            //userService.Create(new User { UserName = "Test" }).Wait();

            Console.ReadLine();
        }
    }
}
