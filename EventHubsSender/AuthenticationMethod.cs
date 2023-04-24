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
        private HttpClient HttpClient { get; set; }
        private SecretClient SecretClient { get; set; }
        private EventHubConfig Config { get; set; }
        public ProducerClient()
        {
            HttpClient = new HttpClient();
            Config = LoadConfig("../config.json");
            SecretClient = new SecretClient(new Uri())
        }

        private EventHubConfig LoadConfig()
        {

        }
        public EventHubProducerClient GetProducerClient(string fullyQualifiedNamespace, string cloudEnvt, string eventHubName, string connectionString, AuthenticationMethod authenticationMethod)
        {

            return authenticationMethod == AuthenticationMethod.PASSWORDLESS ?
                new EventHubProducerClient($"{fullyQualifiedNamespace}.${cloudEnvt}", eventHubName, new DefaultAzureCredential(true)) :
                new EventHubProducerClient(connectionString, eventHubName);
        }

        public void GetSecret()
        {
            // https://{vault-name}.vault.azure.net/{object-type}/{object-name}/{object-version}
            string kvUri = "https://eventhubkvva.vault.azure.net";
            var secretClient = new SecretClient(new Uri("https://eventhubkvva.vault.azure.net"), new DefaultAzureCredential(true));

        }
    }
}
