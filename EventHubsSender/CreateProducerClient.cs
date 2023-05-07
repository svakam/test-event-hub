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
// https://www.newtonsoft.com/json/help/html/DeserializeWithJsonSerializerFromFile.htm#!

namespace EventHubsSender
{
    public enum AuthenticationMethod
    {
        PASSWORDLESS,
        CONNECTIONSTRING
    }

    public class CreateProducerClient
    {
        private SecretClient SecretClient;
        internal EventHubConfig EventHubConfig { get; set; }
        public CreateProducerClient(string configPath)
        {
            // error handling
            EventHubConfig = LoadConfig(configPath);
            SecretClient = new SecretClient(new Uri($"https://{EventHubConfig.KeyVault.VaultName}.{EventHubConfig.KeyVault.DnsSuffix}"),
                new DefaultAzureCredential(includeInteractiveCredentials: true));
        }

        private EventHubConfig LoadConfig(string configPath)
        {
            Console.WriteLine($"config path: {configPath}");
            return JsonConvert.DeserializeObject<EventHubConfig>(File.ReadAllText(configPath));
        }

        public EventHubProducerClient GetProducerClient(AuthenticationMethod authenticationMethod)
        {
            string eventHubName = EventHubConfig.EventHub.EventHubName;

            if (authenticationMethod == AuthenticationMethod.PASSWORDLESS)
            {
                string @namespace = EventHubConfig.EventHub.Namespace,
                    dnsSuffix = EventHubConfig.KeyVault.DnsSuffix;

                return new EventHubProducerClient($"{@namespace}.{dnsSuffix}", eventHubName, new DefaultAzureCredential(true));
            } 
            else
            {
                var responseObj = GetSecretObjResponse(SecretClient, EventHubConfig.EventHub.ConnectionStringName);
                
                try
                {
                    KeyVaultSecret secretObj = responseObj.Value;
                    return new EventHubProducerClient(secretObj.Value, eventHubName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: could not fetch event hub secret: " + e);
                    return null;
                }
            
            }
        }

        private Response<KeyVaultSecret> GetSecretObjResponse(SecretClient secretClient, string connectionStringName)
        {
            return secretClient.GetSecret(connectionStringName);
        }
    }
}
