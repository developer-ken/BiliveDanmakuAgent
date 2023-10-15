using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliveDanmakuAgent.Model
{
    public class RoomEventArgs : EventArgs
    {
        public readonly RoomInfo RoomInfo;
        public readonly Danmaku Danmaku;
        public RoomEventArgs(RoomInfo roomInfo, Danmaku danmaku)
        {
            this.RoomInfo = roomInfo ?? throw new ArgumentNullException(nameof(roomInfo));
            this.Danmaku = danmaku;
        }
    }
}
