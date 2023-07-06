using BiliveDanmakuAgent.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

namespace BiliveDanmakuAgent
{
    public class LiveRoom
    {
        public long rid;
        public StreamMonitor sm;
        public LiveRoom(int roomid, string cookiestr = null)
        {
            rid = roomid;
            sm = new StreamMonitor(roomid, new Func<TcpClient>(Tcpcli), new BililiveAPI(cookiestr));
        }

        public bool init_connection()
        {
            return sm.Start();
        }

        public TcpClient Tcpcli()
        {
            return new TcpClient();
        }
    }
}
