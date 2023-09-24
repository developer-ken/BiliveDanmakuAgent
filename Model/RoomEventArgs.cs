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
        public RoomEventArgs(RoomInfo roomInfo)
        {
            this.RoomInfo = roomInfo ?? throw new ArgumentNullException(nameof(roomInfo));
        }
    }
}
