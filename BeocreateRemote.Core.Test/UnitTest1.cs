namespace BeocreateRemote.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new SshController("192.168.0.4", "root", "hifiberry");
            controller.mute();
        }
    }
}