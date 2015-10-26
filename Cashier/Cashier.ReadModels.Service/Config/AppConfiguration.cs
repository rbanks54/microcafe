using System.Configuration;

namespace Cashier.ReadModels.Service.Config
{
    public class AppConfiguration : ConfigurationSection
    {
        public static AppConfiguration Config
        {
            get { return ConfigurationManager.GetSection("appConfiguration") as AppConfiguration; }
        }

        [ConfigurationProperty("serviceDescription")]
        public string ServiceDescription
        {
            get { return (string) this["serviceDescription"]; }
            set { this["serviceDescription"] = value; }
        }

        [ConfigurationProperty("serviceDisplayName")]
        public string ServiceDisplayName
        {
            get { return (string) this["serviceDisplayName"]; }
            set { this["serviceDisplayName"] = value; }
        }

        [ConfigurationProperty("serviceName")]
        public string ServiceName
        {
            get { return (string) this["serviceName"]; }
            set { this["serviceName"] = value; }
        }

        [ConfigurationProperty("serviceUri")]
        public string ServiceUri
        {
            get { return (string) this["serviceUri"]; }
            set { this["serviceUri"] = value; }
        }

        [ConfigurationProperty("messageBusEndPoint")]
        public string MessageBusEndPoint
        {
            get { return (string) this["messageBusEndPoint"]; }
            set { this["messageBusEndPoint"] = value; }
        }

        [ConfigurationProperty("eventStorePort")]
        public int EventStorePort
        {
            get { return (int) this["eventStorePort"]; }
            set { this["eventStorePort"] = value; }
        }
    }
}