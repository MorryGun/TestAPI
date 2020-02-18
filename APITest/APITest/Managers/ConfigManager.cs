using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace APITest.Managers
{
    public class ConfigManager
    {
        private const string ConfigFile = "config.json";
        
        protected IConfigurationRoot Config => new ConfigurationBuilder()
                                                        .AddJsonFile(ConfigFile)
                                                        .Build();
    }
}
