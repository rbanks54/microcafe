using System;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace Admin.ReadModels.Service
{
    class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<AdminReadModelService>(s =>
                {
                    s.ConstructUsing(name => new AdminReadModelService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Admin - Read Models Service");
                x.SetDisplayName("Admin - Read Models Service");
                x.SetServiceName("Admin.ReadModels.Service");
            });
        }
    }
    public class AdminReadModelService
    {
        private IDisposable webApp;
        public void Start()
        {
            const string baseUri = "http://localhost:8181";
            Console.WriteLine("Starting Admin Read Model Service...");
            webApp = WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
        }

        public void Stop()
        {
            webApp.Dispose();
        }
    }
}
