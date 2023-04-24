using Azure.Identity;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHubsSender;
using System.Runtime.InteropServices;
using System.Text;

int numEvents = 7;

// ideally these are fetched as secrets in prod
string eventHubNamespace = "testnsva";
string cloudEnvt = "servicebus.windows.net";
string eventHubName = "testhub";

// create producer client instance
EventHubProducerClient producerClient = new();
producerClient.GetProducerClient(eventHubNamespace, cloudEnvt, eventHubName, connectionString, AuthenticationMethod.CONNECTIONSTRING);

// create batch instance
using EventDataBatch eventDataBatch = await producerClient.CreateBatchAsync();

for (int i = 1; i <= numEvents; i++)
{
    if (!eventDataBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event {i}"))))
    {
        // if batch size exceeds batch limit
        throw new Exception($"Event {i} is too large for the batch and can't be sent");
    }
}

try
{
    // send the batch via producer client to event hub
    await producerClient.SendAsync(eventDataBatch);
    Console.WriteLine($"Batch of {numEvents} was published");
}
finally
{
    // ensure resources associated with client, and client are removed and closed
    await producerClient.DisposeAsync();
}