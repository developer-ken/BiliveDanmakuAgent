using System;

namespace BiliveDanmakuAgent.Core
{
    public delegate void DisconnectEvt(object sender, DisconnectEvtArgs e);
    public class DisconnectEvtArgs
    {
        public Exception Error;
    }

    public delegate void ReceivedRoomCountEvt(object sender, ReceivedRoomCountArgs e);
    public class ReceivedRoomCountArgs
    {
        public uint UserCount;
    }

    public delegate void ReceivedDanmakuEvt(object sender, ReceivedDanmakuArgs e);
    public delegate void ExceptionHappenedEvt(object sender, Exception e,string desc="");
    public delegate void LogOutputEvent(object sender, string text);
    public class ReceivedDanmakuArgs
    {
        public DanmakuModel Danmaku;
    }
}
