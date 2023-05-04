using EventHubsReceiver;
using EventHubsSender;
using System.Text;

namespace Tests
{
    [TestClass]
    public class TestLoadConfig
    {
        [TestMethod]
        public void TestPath()
        {
            string configPath = $"{Environment.CurrentDirectory}\\config.json";
            string realPath = "C:\\Users\\vikra\\Development\\Repos\\GitHub\\test-event-hub-qs\\EventHubsQS\\config.json";
            Assert.IsTrue(configPath == realPath);
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }

    [TestClass]
    public class TestGetSecret
    {
        [TestMethod]
        public void GetSecret()
        {

        }
    }
}