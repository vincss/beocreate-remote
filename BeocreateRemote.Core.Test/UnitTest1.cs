namespace BeocreateRemote.Core.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var controller = new SshController("192.168.0.4", "root", "hifiberry");
            //var temp = controller.GetTemperature();
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void SshControllerConvert()
        {
            var result = SshController.ConvertVolume("Volume: 0.0138 / 38% / -37db");
            Assert.AreEqual(1, result);

            result = SshController.ConvertVolume("Volume: 0.66 / 66% / -xxdb");
            Assert.AreEqual(66, result);
        }

        [TestMethod]
        public void SshControllerConvertBack()
        {
            var result = SshController.ConvertBackVolume(33);
            Assert.AreEqual("0.33", result);
        }
    }
}