
using EventHubsSender;
using Newtonsoft.Json;
using System.Text;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace Tests
{
    [TestClass]
    public class TestLoadConfig
    {
        EventHubConfig deserialized;
        [TestMethod]
        public void TestPath()
        {
            // https://stackoverflow.com/questions/19001423/getting-path-to-the-parent-folder-of-the-solution-file-using-c-sharp
            string configPath = $"{Environment.CurrentDirectory}";
            var directory = new DirectoryInfo(configPath);
            Console.WriteLine(directory.FullName);
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                Console.WriteLine(directory.FullName);
                directory = directory.Parent;
            }
            Console.WriteLine(directory.FullName);
            configPath = directory.FullName + "\\eventHubsDetails.json";
            string realPath = "C:\\Users\\vikra\\Development\\Repos\\GitHub\\test-event-hub-qs\\EventHubsQS\\eventHubsDetails.json";
            Assert.IsTrue(configPath == realPath);
        }

        [TestMethod]
        public void TestJSONRead()
        {
            string currConfig = "C:\\Users\\vikra\\Development\\Repos\\GitHub\\test-event-hub-qs\\EventHubsQS\\eventHubsDetails.json";
            // test each obj in deserialized
            string readJson = File.ReadAllText(currConfig);
            deserialized = JsonConvert.DeserializeObject<EventHubConfig>(readJson);
            Console.WriteLine(readJson);
            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(deserialized.EventHub);
            Assert.IsTrue(deserialized.EventHub.Namespace == "testnsva");
            Assert.IsTrue(deserialized.StorageAcct.StorageAcctName == "testcheckpointva");
            Assert.IsTrue(deserialized.KeyVault.VaultName == "eventhubkvva");
        }
    }

    [TestClass]
    public class TestGetSecret
    {
        [TestMethod]
        public void GetSecret()
        {
            string realPath = "C:\\Users\\vikra\\Development\\Repos\\GitHub\\test-event-hub-qs\\EventHubsQS\\eventHubsDetails.json";
            EventHubConfig deserialized = JsonConvert.DeserializeObject<EventHubConfig>(File.ReadAllText(realPath));
            SecretClient sc = new(new Uri($"https://{deserialized.KeyVault.VaultName}.{deserialized.KeyVault.DnsSuffix}"),
                new DefaultAzureCredential(includeInteractiveCredentials: true));
            Assert.IsNotNull(sc);
            Assert.IsTrue(sc.VaultUri.ToString() == "https://eventhubkvva.vault.azure.net/");
            var response = sc.GetSecret(deserialized.EventHub.ConnectionStringName).Value;
            Assert.IsNotNull(response);
        }
    }
}