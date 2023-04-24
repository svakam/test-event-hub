using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Storage.Blobs;
using Azure.Identity;

namespace EventHubsReceiver
{
    public enum AuthenticationMethod
    {
        PASSWORDLESS,
        CONNECTIONSTRING
    }

    public class ProcessorClient
    {
        public static EventProcessorClient GetEventProcessorClient(string eventHubNamespace, string cloudEnvt, string eventHubName, string storageAccName, string containerName, string connectionString)
        {

        }
    }
}
