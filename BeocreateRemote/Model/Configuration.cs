using BeocreateRemote.Helper;
using System.Xml.Serialization;

namespace BeocreateRemote.Model
{
    public enum RemoteType
    {
        SigmaTcpController = 0,
        SshController = 1,
        MockController = -1
    }

    [XmlInclude(typeof(MockConfiguration)), XmlInclude(typeof(SshConfiguration)), XmlInclude(typeof(SigmaTcpConfiguration))]
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
                    switch (config.RemoteType)
                    {
                        case RemoteType.SshController:
                            return (SshConfiguration)config;
                        case RemoteType.MockController:
                            return (MockConfiguration)config;
                        case RemoteType.SigmaTcpController:
                            return (SigmaTcpConfiguration)config;
                        default:
                            return config;
                    }
                }
                catch { }
            }
            return null;
        }
    }
}
