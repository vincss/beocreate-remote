namespace BeocreateRemote.Core.Test
{
   
    [TestClass]
    public class FanHelperTest
    {
        [TestMethod]
        public void GetSpeedFromTemperature()
        {
            var speed = FanHelper.GetSpeedFromTemperature(FanConfiguration.MinTemp);
            Assert.AreEqual(speed, FanConfiguration.MinSpeed);

            speed = FanHelper.GetSpeedFromTemperature(FanConfiguration.MaxTemp);
            Assert.AreEqual(speed, FanConfiguration.MaxSpeed);

            speed = (int)(FanHelper.GetSpeedFromTemperature(60) * 100);
            Assert.AreEqual(speed, 73);

            speed = (int)(FanHelper.GetSpeedFromTemperature(65) * 100);
            Assert.AreEqual(speed, 86);
        }

    }
}