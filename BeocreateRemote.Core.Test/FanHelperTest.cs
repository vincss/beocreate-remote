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

            speed = FanHelper.GetSpeedFromTemperature(60);
            Assert.AreEqual(speed, FanConfiguration.MaxSpeed);
        }

    }
}