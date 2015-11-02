using System;
using Microsoft.Owin.Hosting;
using Cashier.Service.Config;
using Topshelf;

namespace Cashier.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<CashierService>(s =>
                {
                    s.ConstructUsing(name => new CashierService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Cashier - Domain Service");
                x.SetDisplayName("Cashier - Domain Service");
                x.SetServiceName("Cashier.Service");
            });
        }
    }

    public class CashierService
    {
        private IDisposable webApp;

        public void Start()
        {
            var baseUri = "http://localhost:8183";

            Console.WriteLine("Starting Cashier Domain Service...");
            webApp = WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
        }

        public void Stop()
        {
            webApp.Dispose();
        }
    }
}