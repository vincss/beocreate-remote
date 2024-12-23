﻿namespace BeocreateRemote.Core.Test
{
    [TestClass]
    public class ConvertersTest
    {

        [TestMethod]
        public void TestSigmaTcpConverter()
        {
            var dico = new Dictionary<double, byte[]>() {
                { 0.1, new byte[] { 0, 25, 153, 153 } },
                { 0.05, new byte[] { 0x00, 0x0C, 0xCC, 0xCC }},
                { 0.01, new byte[] { 0x00, 0x02, 0x8F, 0x5C }},

            };

            foreach (var kvp in dico)
            {
                var actual = SigmaTcpController.DecimalVal(kvp.Value);
                Assert.AreEqual(kvp.Key, actual);
            }

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
