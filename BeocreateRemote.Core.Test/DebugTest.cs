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