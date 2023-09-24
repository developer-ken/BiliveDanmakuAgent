using BililiveRecorder.Core.Api.Danmaku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliveDanmakuAgent.Model
{
    public class DanmakuReceivedEventArgs : EventArgs
    {
        public readonly Danmaku Danmaku;
        public DanmakuReceivedEventArgs(Danmaku danmaku)
        {
            this.Danmaku = danmaku ?? throw new ArgumentNullException(nameof(danmaku));
        }
    }
}
