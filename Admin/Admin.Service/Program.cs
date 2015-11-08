using System;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace Admin.Service
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            HostFactory.Run(x => 
            {
                x.Service<AdminService>(s => 
                {
                    s.ConstructUsing(name => new AdminService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Admin - Domain Service");
                x.SetDisplayName("Admin - Domain Service");
                x.SetServiceName("Admin.Service");
            });
        }
    }

    public class AdminService
    {
        private IDisposable webApp;
        public void Start()
        {
            const string baseUri = "http://localhost:8180";
            Console.WriteLine("Starting web Server...");
            webApp = WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
        }

        public void Stop()
        {
            webApp.Dispose();
        }
    }

}
