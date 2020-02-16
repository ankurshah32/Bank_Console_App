using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Thoughtworks.BusinessLogic;
using Thoughtworks.BusinessLogic.Interface;
using Thoughtworks.Model;

namespace Thoughtworks
{
    class Program
    {
        public static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();
            BankUI ui = new BankUI(serviceProvider.GetService<BusinessLogic.Interface.IBankOperation>());
            ui.WelcomeScreen();
            //Console.WriteLine(optionSelected);
        }
        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IBankOperation, BankOperation>();
            collection.AddTransient<Transcation>();
            collection.AddTransient<Customer>();
            collection.AddTransient<Account>();
            collection.AddSingleton<IModel, Bank>();
            serviceProvider = collection.BuildServiceProvider();
        }

    }
}
