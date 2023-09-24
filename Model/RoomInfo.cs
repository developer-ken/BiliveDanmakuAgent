using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliveDanmakuAgent.Model
{
    public class RoomInfo
    {
        /// <summary>
        /// <see cref="DanmakuMsgType.LiveStart"/>,<see cref="DanmakuMsgType.LiveEnd"/> 事件对应的房间号
        /// </summary>
        public string? RoomID { get; set; }

        /// <summary>
        /// 房间标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 大分区
        /// </summary>
        public string? ParentAreaName { get; set; }

        /// <summary>
        /// 子分区
        /// </summary>
        public string? AreaName { get; set; }
    }
}
