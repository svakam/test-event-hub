
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
            string configPath = $"{Environment.CurrentDirectory}";
            var directory = new DirectoryInfo(configPath);
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                Console.WriteLine(directory.FullName);
                directory = directory.Parent;
            }
            configPath = directory.FullName + "\\config.json";
            string realPath = "C:\\Users\\vikra\\Development\\Repos\\GitHub\\test-event-hub-qs\\EventHubsQS\\config.json";
            Assert.IsTrue(configPath == realPath);
        }

        [TestMethod]
        public void TestJSONRead()
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