using BeocreateRemote.Helper;
using System.Xml.Serialization;

namespace BeocreateRemote.Model
{
    public enum RemoteType
    {
        SshController = 0,

        MockController = -1
    }

    [XmlInclude(typeof(MockConfiguration)), XmlInclude(typeof(SshConfiguration)),]
    public abstract class Configuration
    {
        const string ConfigurationKey = "BeoRemoteConfiguration";
        public RemoteType RemoteType { get; set; }


        public static void Save(Configuration configuration)
        {
            var configToSave = configuration.SerializeXml();
            Preferences.Set(ConfigurationKey, configToSave);
        }

        public static Configuration Load()
        {
            var configLoaded = Preferences.Get(ConfigurationKey, null);
            if (configLoaded != null)
            {
                try
                {
                    var config = configLoaded.DeserializeXml<Configuration>();
                    return config;
                }
                catch { }
            }
            return null;
        }
    }

    public class MockConfiguration : Configuration;
    public class SshConfiguration : Configuration
    {

        public string Address;
        public string User;
        public string Password;
    }
}
