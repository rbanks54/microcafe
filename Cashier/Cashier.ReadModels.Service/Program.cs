using System;
using Microsoft.Owin.Hosting;
using Cashier.ReadModels.Service.Config;
using Topshelf;

namespace Cashier.ReadModels.Service
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<CashierReadModelsService>(s =>
                {
                    s.ConstructUsing(name => new CashierReadModelsService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Cashier - Read Models Service");
                x.SetDisplayName("Cashier - Read Models Service");
                x.SetServiceName("Cashier.ReadModels.Service");
            });
        }
    }

    public class CashierReadModelsService
    {
        private IDisposable webApp;

        public void Start()
        {
            var baseUri = "http://localhost:8182";

            Console.WriteLine("Starting Cashier Read Model Service...");
            webApp = WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
        }

        public void Stop()
        {
            webApp.Dispose();
        }
    }
}