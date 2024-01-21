using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeocreateRemote.Core.mock
{
    public class OsmcMock : IRemoteController
    {
        public OsmcMock()
        {
            Volume = 0.33;
            _mute = false;
        }

        private bool _mute;
        public double Volume { get; set; }

        public int GetTemperature()
        {
            return new Random().Next(40, 60);
        }

        public void Mute()
        {
            _mute = true;
        }

        public void Unmute()
        {
            _mute = false;
        }
    }
}
