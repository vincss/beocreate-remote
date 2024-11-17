namespace BeocreateRemote.Core.Test
{
    [Ignore]
    [TestClass]
    public class DebugTest
    {
        [TestMethod]
        public void TestSshController()
        {
            var controller = new SshController("192.168.0.4", "root", "hifiberry");
            var volume = controller.Volume;
        }
        
        [TestMethod]
        public async Task TestFanClient()
        {
            var remoteConfiguration = new RemoteConfiguration() { SigmaTcpAddress = "192.168.0.4", BeocreateRemoteServerAddress = "http://192.168.0.4:5000" };

            var httpClient = new HttpClient();
            var fanClient = new FanClient(httpClient, remoteConfiguration);
            var fanInformation = await fanClient.GetFanInformation();
        }

        [TestMethod]
        public void TestSigmaController()
        {
            var sigmaTcpController = new SigmaTcpController("192.168.0.4");
/*
            sigmaTcpController.SetVolume(0.02);
            sigmaTcpController.Volume = 7;

            var volume = sigmaTcpController.Volume;
            sigmaTcpController.Mute();

            sigmaTcpController.Unmute();
            */
        }
    }
}