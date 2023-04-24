namespace EventHubsSender
{
    public class EventHubConfig
    {
        public class EventHub
        {
            string Namespace { get; set; }
            string Name { get; set; }
            string DnsSuffix { get; set; }
            string ConnectionStringName { get; set; }

        }
        public class StorageAcct
        {
            string Name { get; set; }
            string ContainerName { get; set; }
            string ConnectionStringName { get; set; }
        }
        public class KeyVault
        {
            string Name { get; set; }
            string DnsSuffix { get; set; }
        }
    }
}

