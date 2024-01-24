namespace BeocreateRemote.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new SshController("192.168.0.4", "root", "hifiberry");
            var temp = controller.GetTemperature();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var result = SshController.ConvertVolume("Volume: 0.0138 / 38% / -37db");
            Assert.AreEqual(0.0138, result);
        }
    }
}