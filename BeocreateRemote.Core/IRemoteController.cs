﻿namespace BeocreateRemote.Core
{
    public interface IRemoteController
    {
        public bool Mute { get; set; }
        public int Volume { get; set; }
        public bool IsConnected { get; }
    }
}