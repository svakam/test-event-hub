using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Storage.Blobs;
using Azure.Identity;
using EventHubsReceiver;
using System.Runtime.InteropServices;
using System.Text;

//var processor = ProcessorClient.GetEventProcessorClient(eventHubNamespace, cloudEnvt, eventHubName, storageAccName, containerName, connectionString);