using BililiveRecorder.Core.Api;
using BililiveRecorder.Core.Api.Danmaku;
using BililiveRecorder.Core.Api.Http;
using BililiveRecorder.Core.Config;
using BiliveDanmakuAgent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanmakuMsgType = BililiveRecorder.Core.Api.Danmaku.DanmakuMsgType;

namespace BiliveDanmakuAgent
{
    public class DanmakuApi
    {
        HttpApiClient hc;
        DanmakuClient dc;
        public readonly int RoomId;
        public delegate void DanmakuReceivedEventHandler(object sender, BiliveDanmakuAgent.Model.DanmakuReceivedEventArgs e);
        public delegate void RoomEventHandler(object sender, BiliveDanmakuAgent.Model.RoomEventArgs e);

        public event DanmakuReceivedEventHandler DanmakuMsgReceivedEvent, CommentReceived, Gift, GuardBuy, Superchat;
        public event RoomEventHandler LiveStartEvent, LiveEndEvent, RoomLockEvent, RoomCutOffEvent, RoomChange;

        public DanmakuApi(int roomid, string cookiestr = "", string server = "https://api.live.bilibili.com", double timeout = 10000)
        {
            hc = new HttpApiClient(new BililiveRecorder.Core.Config.V3.GlobalConfig() { Cookie = cookiestr, LiveApiHost = server, TimingApiTimeout = timeout });
            dc = new DanmakuClient(hc);
            RoomId = roomid;
            dc.DanmakuReceived += Dc_DanmakuReceived;
        }

        public async Task ConnectAsync()
        {
            await dc.ConnectAsync(RoomId, DanmakuTransportMode.Random, default);
        }

        private void Dc_DanmakuReceived(object sender, BililiveRecorder.Core.Api.Danmaku.DanmakuReceivedEventArgs e)
        {
            Danmaku danmaku = new Danmaku();
            danmaku.UserMedal = e.Danmaku.UserMedal;
            danmaku.RoomInfo.AreaName = e.Danmaku.AreaName;
            danmaku.RoomInfo.ParentAreaName = e.Danmaku.ParentAreaName;
            danmaku.RoomInfo.RoomID = e.Danmaku.RoomID;
            danmaku.RoomInfo.Title = e.Danmaku.Title;
            danmaku.MsgType = (Model.DanmakuMsgType)e.Danmaku.MsgType;
            danmaku.GiftName = e.Danmaku.GiftName;
            danmaku.GiftCount = e.Danmaku.GiftCount;
            danmaku.CommentText = e.Danmaku.CommentText;
            danmaku.UserGuardLevel = e.Danmaku.UserGuardLevel;
            danmaku.UserName = e.Danmaku.UserName;
            danmaku.UserID = e.Danmaku.UserID;
            danmaku.Price = e.Danmaku.Price;
            danmaku.SCKeepTime = e.Danmaku.SCKeepTime;
            danmaku.RawString = e.Danmaku.RawString;
            danmaku.RawObject = e.Danmaku.RawObject;
            danmaku.IsAdmin = e.Danmaku.IsAdmin;
            danmaku.IsVIP = e.Danmaku.IsVIP;
            danmaku.AvatarUrl = e.Danmaku.AvatarUrl;
            danmaku.GiftId = e.Danmaku.GiftId;

            BiliveDanmakuAgent.Model.DanmakuReceivedEventArgs args = new BiliveDanmakuAgent.Model.DanmakuReceivedEventArgs(danmaku);
            DanmakuMsgReceivedEvent?.Invoke(this, args);

            switch (e.Danmaku.MsgType)
            {
                case DanmakuMsgType.Comment:
                    CommentReceived?.Invoke(this, args);
                    break;
                case DanmakuMsgType.GiftSend:
                    Gift?.Invoke(this, args);
                    break;
                case DanmakuMsgType.LiveStart:
                    LiveStartEvent?.Invoke(sender, new RoomEventArgs(danmaku.RoomInfo, danmaku));
                    break;
                case DanmakuMsgType.LiveEnd:
                    LiveEndEvent?.Invoke(sender, new RoomEventArgs(danmaku.RoomInfo, danmaku));
                    break;
                case DanmakuMsgType.Unknown:
                    break;
                case DanmakuMsgType.GuardBuy:
                    GuardBuy?.Invoke(this, args);
                    break;
                case DanmakuMsgType.SuperChat:
                    Superchat?.Invoke(this, args);
                    break;
                case DanmakuMsgType.RoomChange:
                    RoomChange?.Invoke(sender, new RoomEventArgs(danmaku.RoomInfo, danmaku));
                    break;
                case DanmakuMsgType.RoomLock:
                    RoomLockEvent?.Invoke(sender, new RoomEventArgs(danmaku.RoomInfo, danmaku));
                    break;
                case DanmakuMsgType.CutOff:
                    RoomCutOffEvent?.Invoke(sender, new RoomEventArgs(danmaku.RoomInfo, danmaku));
                    break;
            }
        }
    }
}
