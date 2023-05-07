using Newtonsoft.Json;

namespace EventHubsSender
{
    public class EventHubConfig
    {
        public EventHubDetails EventHub { get; set; }
        public StorageAcctDetails StorageAcct { get; set; }
        public KeyVaultDetails KeyVault { get; set; }

        public class EventHubDetails
        {
            public string Namespace { get; set; }
            public string EventHubName { get; set; }
            public string DnsSuffix { get; set; }
            public string ConnectionStringName { get; set; }
        }
        public class StorageAcctDetails
        {
            public string StorageAcctName { get; set; }
            public string ContainerName { get; set; }
            public string ConnectionStringName { get; set; }
        }
        public class KeyVaultDetails
        {
            public string VaultName { get; set; }
            public string DnsSuffix { get; set; }
        }
    }

    
}

