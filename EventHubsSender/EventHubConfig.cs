using Newtonsoft.Json;

namespace EventHubsSender
{
    public class EventHubConfig
    {
        public EventHub EventHubDetails { get; set; }
        public StorageAcct StorageAcctDetails { get; set; }
        public KeyVault KeyVaultDetails { get; set; }

        public class EventHub
        {
            public string Namespace { get; set; }
            public string EventHubName { get; set; }
            public string DnsSuffix { get; set; }
            public string ConnectionStringName { get; set; }
        }
        public class StorageAcct
        {
            public string StorageAcctName { get; set; }
            public string ContainerName { get; set; }
            public string ConnectionStringName { get; set; }
        }
        public class KeyVault
        {
            public string VaultName { get; set; }
            public string DnsSuffix { get; set; }
        }
    }

    
}

