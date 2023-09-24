using System.ComponentModel;

namespace BililiveRecorder.Core.Config
{
    public enum DanmakuTransportMode
    {
        Random = 0,
        Tcp = 1,
        Ws = 2,
        Wss = 3,
    }
}

namespace BililiveRecorder.Core.Config.V3
{
    public class GlobalConfig
    {
        public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
        public event PropertyChangedEventHandler? PropertyChanged;

        public double TimingApiTimeout = 10000;
        public string Cookie = "";
        public string LiveApiHost = "https://api.live.bilibili.com";
    }
}