using System;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace Barista.ReadModels.Service
{
    class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<JourneyDesignerService>(s =>
                {
                    s.ConstructUsing(name => new JourneyDesignerService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Barista - Read Models Service");
                x.SetDisplayName("Barista - Read Models Service");
                x.SetServiceName("Barista.ReadModels.Service");
            });
        }
    }
    public class JourneyDesignerService
    {
        private IDisposable webApp;
        public void Start()
        {
            const string baseUri = "http://localhost:8181";
            Console.WriteLine("Starting Barista Read Model Service...");
            webApp = WebApp.Start<Startup>(baseUri);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
        }

        public void Stop()
        {
            webApp.Dispose();
        }
    }
}
