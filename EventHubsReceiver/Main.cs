using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Storage.Blobs;
using Azure.Identity;
using EventHubsReceiver;
using System.Runtime.InteropServices;
using System.Text;

string eventHubNamespace = "testnsva";
string cloudEnvt = "servicebus.windows.net";
string eventHubName = "testhub";
string storageAccName = "testcheckpointva";
string containerName = "test-container";
string connectionString = "DefaultEndpointsProtocol=https;AccountName=testcheckpointva;AccountKey=gQ9yVu5Y9G1yUjCHvTJF+K5iq0Ch2rwz72oevVSJjm4tOoP4wt5wtfOrliOAUe8ZIqmAe7janAH2+AStCipsow==;EndpointSuffix=core.windows.net";

var processor = ProcessorClient.GetEventProcessorClient(eventHubNamespace, cloudEnvt, eventHubName, storageAccName, containerName, connectionString);