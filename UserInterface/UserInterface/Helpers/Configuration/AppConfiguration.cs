using System.Configuration;

namespace UserInterface.Helpers.Configuration
{
    public class AppConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("jwtIssuer")]
        public string JwtIssuer
        {
            get { return (string) this["jwtIssuer"]; }
            set { this["jwtIssuer"] = value; }
        }

        [ConfigurationProperty("jwtAudienceKey")]
        public string JwtAudienceKey
        {
            get { return (string) this["jwtAudienceKey"]; }
            set { this["jwtAudienceKey"] = value; }
        }

        [ConfigurationProperty("jwtSecretKey")]
        public string JwtSecretKey
        {
            get { return (string) this["jwtSecretKey"]; }
            set { this["jwtSecretKey"] = value; }
        }

        public static AppConfiguration Config
        {
            get { return ConfigurationManager.GetSection("appConfiguration") as AppConfiguration; }
        }
    }
}