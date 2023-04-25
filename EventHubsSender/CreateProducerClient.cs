using Azure;
using Azure.Identity;
using Azure.Messaging.EventHubs.Producer;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

// https://learn.microsoft.com/en-us/dotnet/api/azure.messaging.eventhubs.producer.eventhubproducerclient.-ctor?view=azure-dotnet#azure-messaging-eventhubs-producer-eventhubproducerclient-ctor(system-string-system-string-azure-azurenamedkeycredential-azure-messaging-eventhubs-producer-eventhubproducerclientoptions) 

namespace EventHubsSender
{
    public enum AuthenticationMethod
    {
        PASSWORDLESS,
        CONNECTIONSTRING
    }

    public class ProducerClient
    {
        private HttpClient HttpClient;
        private SecretClient SecretClient;
        private EventHubConfig EventHubConfig { get; set; }
        public ProducerClient(string configPath)
        {
            // error handling
            HttpClient = new HttpClient();
            EventHubConfig = LoadConfig(configPath);
            SecretClient = new SecretClient(new Uri($"https://{EventHubConfig.KeyVaultDetails.VaultName}.{EventHubConfig.KeyVaultDetails.DnsSuffix}"),
                new DefaultAzureCredential(includeInteractiveCredentials: true));
        }

        private EventHubConfig LoadConfig(string configPath)
        {
            Console.WriteLine($"config path: {configPath}");
            return JsonConvert.DeserializeObject<EventHubConfig>(File.ReadAllText(configPath));
        }

        public EventHubProducerClient GetProducerClient(AuthenticationMethod authenticationMethod)
        {
            string eventHubName = EventHubConfig.EventHubDetails.EventHubName;

            if (authenticationMethod == AuthenticationMethod.PASSWORDLESS)
            {
                string @namespace = EventHubConfig.EventHubDetails.Namespace,
                dnsSuffix = EventHubConfig.KeyVaultDetails.DnsSuffix;

                return new EventHubProducerClient($"{@namespace}.{dnsSuffix}", eventHubName, new DefaultAzureCredential(true));
            } else
            {
                var connectionString = GetSecret(SecretClient, EventHubConfig.EventHubDetails.ConnectionStringName);
                
                try
                {
                    return new EventHubProducerClient(connectionString.Value, eventHubName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: could not fetch event hub secret: " + e);
                    return null;
                }
            
            }
        }

        private KeyVaultSecret? GetSecret(SecretClient secretClient, string connectionStringName)
        {
            return secretClient.GetSecret(connectionStringName);
        }
    }
}
