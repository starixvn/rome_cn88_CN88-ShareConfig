namespace Alpha.ConfigurationSharing.Tests
{
    using System.Configuration;
    using System.IO;

    public class ConfigurationLoader
    {
        public Configuration LoadConfigurations(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = filePath;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            return config;
        }
    }
}
