using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.WebSockets;
using BililiveRecorder.Core;
using System.Net.Sockets;

namespace BiliveDanmakuCli
{
    public class LiveRoom
    {
        public int rid;
        ClientWebSocket sock;
        public StreamMonitor sm;
        public LiveRoom(int roomid)
        {
            rid = roomid;
            sm = new StreamMonitor(roomid, new Func<TcpClient>(Tcpcli));
            sock = new ClientWebSocket();
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
