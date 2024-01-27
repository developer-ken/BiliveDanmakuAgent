using BiliApi.Modules;
using Newtonsoft.Json.Linq;
using System.Linq;

#nullable enable
namespace BililiveRecorder.Core.Api.Danmaku
{
    internal enum DanmakuMsgType
    {
        /// <summary>
        /// 彈幕
        /// </summary>
        Comment,
        /// <summary>
        /// 禮物
        /// </summary>
        GiftSend,
        /// <summary>
        /// 直播開始
        /// </summary>
        LiveStart,
        /// <summary>
        /// 直播結束
        /// </summary>
        LiveEnd,
        /// <summary>
        /// 其他
        /// </summary>
        Unknown,
        /// <summary>
        /// 购买船票（上船）
        /// </summary>
        GuardBuy,
        /// <summary>
        /// SuperChat
        /// </summary>
        SuperChat,
        /// <summary>
        /// 房间信息更新
        /// </summary>
        RoomChange,
        /// <summary>
        /// 房间被锁定
        /// </summary>
        RoomLock,
        /// <summary>
        /// 直播被切断
        /// </summary>
        CutOff,
    }

    internal class DanmakuModel
    {
        /// <summary>
        /// 消息類型
        /// </summary>
        public DanmakuMsgType MsgType { get; set; }

        /// <summary>
        /// 用户当前佩戴的牌子
        /// </summary>
        public Medal? UserMedal { get; set; }

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

        /// <summary>
        /// 彈幕內容
        /// <para>此项有值的消息类型：<list type="bullet">
        /// <item><see cref="DanmakuMsgType.Comment"/></item>
        /// </list></para>
        /// </summary>
        public string? CommentText { get; set; }

        /// <summary>
        /// 消息触发者用户名
        /// <para>此项有值的消息类型：<list type="bullet">
        /// <item><see cref="DanmakuMsgType.Comment"/></item>
        /// <item><see cref="DanmakuMsgType.GiftSend"/></item>
        /// <item><see cref="DanmakuMsgType.Welcome"/></item>
        /// <item><see cref="DanmakuMsgType.WelcomeGuard"/></item>
        /// <item><see cref="DanmakuMsgType.GuardBuy"/></item>
        /// </list></para>
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// SC 价格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// SC 保持时间
        /// </summary>
        public int SCKeepTime { get; set; }

        /// <summary>
        /// 消息触发者用户ID
        /// <para>此项有值的消息类型：<list type="bullet">
        /// <item><see cref="DanmakuMsgType.Comment"/></item>
        /// <item><see cref="DanmakuMsgType.GiftSend"/></item>
        /// <item><see cref="DanmakuMsgType.Welcome"/></item>
        /// <item><see cref="DanmakuMsgType.WelcomeGuard"/></item>
        /// <item><see cref="DanmakuMsgType.GuardBuy"/></item>
        /// </list></para>
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 用户舰队等级
        /// <para>0 为非船员 1 为总督 2 为提督 3 为舰长</para>
        /// <para>此项有值的消息类型：<list type="bullet">
        /// <item><see cref="DanmakuMsgType.Comment"/></item>
        /// <item><see cref="DanmakuMsgType.WelcomeGuard"/></item>
        /// <item><see cref="DanmakuMsgType.GuardBuy"/></item>
        /// </list></para>
        /// </summary>
        public int UserGuardLevel { get; set; }

        /// <summary>
        /// 禮物名稱
        /// </summary>
        public string? GiftName { get; set; }

        /// <summary>
        /// 礼物ID
        /// </summary>
        public int? GiftId { get; set; }

        /// <summary>
        /// 礼物数量
        /// <para>此项有值的消息类型：<list type="bullet">
        /// <item><see cref="DanmakuMsgType.GiftSend"/></item>
        /// <item><see cref="DanmakuMsgType.GuardBuy"/></item>
        /// </list></para>
        /// <para>此字段也用于标识上船 <see cref="DanmakuMsgType.GuardBuy"/> 的数量（月数）</para>
        /// </summary>
        public int GiftCount { get; set; }

        /// <summary>
        /// 该用户是否为房管（包括主播）
        /// <para>此项有值的消息类型：<list type="bullet">
        /// <item><see cref="DanmakuMsgType.Comment"/></item>
        /// <item><see cref="DanmakuMsgType.GiftSend"/></item>
        /// </list></para>
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否VIP用戶(老爺)
        /// <para>此项有值的消息类型：<list type="bullet">
        /// <item><see cref="DanmakuMsgType.Comment"/></item>
        /// <item><see cref="DanmakuMsgType.Welcome"/></item>
        /// </list></para>
        /// </summary>
        public bool IsVIP { get; set; }

        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// <see cref="DanmakuMsgType.LiveStart"/>,<see cref="DanmakuMsgType.LiveEnd"/> 事件对应的房间号
        /// </summary>
        public string? RoomID { get; set; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public string RawString { get; set; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public JObject? RawObject { get; set; }

        private DanmakuModel()
        {
            this.RawString = string.Empty;
        }

        public DanmakuModel(string json)
        {
            this.RawString = json;

            var obj = JObject.Parse(json);
            this.RawObject = obj;

            var cmd = obj["cmd"]?.ToObject<string>();

            if (cmd?.StartsWith("DANMU_MSG:") ?? false)
                cmd = "DANMU_MSG";

            switch (cmd)
            {
                case "LIVE": // 开播
                    this.MsgType = DanmakuMsgType.LiveStart;
                    this.RoomID = obj["roomid"]?.ToObject<string>();
                    break;
                case "PREPARING": // 下播
                    this.MsgType = DanmakuMsgType.LiveEnd;
                    this.RoomID = obj["roomid"]?.ToObject<string>();
                    break;
                case "DANMU_MSG": // 弹幕
                    this.MsgType = DanmakuMsgType.Comment;
                    this.CommentText = obj["info"]?[1]?.ToObject<string>();
                    this.UserID = obj["info"]?[2]?[0]?.ToObject<long>() ?? 0;
                    this.UserName = obj["info"]?[2]?[1]?.ToObject<string>();
                    this.IsAdmin = obj["info"]?[2]?[2]?.ToObject<string>() == "1";
                    this.IsVIP = obj["info"]?[2]?[3]?.ToObject<string>() == "1";
                    this.UserGuardLevel = obj["info"]?[7]?.ToObject<int>() ?? 0;
                    this.AvatarUrl = obj["info"]?[0]?[15]?["user"]?["base"]?["face"]?.ToObject<string>();
                    this.UserMedal = obj["info"]?[3]?.Count() <1 ? null :
                        new Medal()
                        {
                            Name = obj["info"]?[3]?[1]?.ToString(),
                            Level = obj["info"]?[3]?[0]?.ToObject<int>() ?? 0,
                            TargetName = obj["info"]?[3]?[2]?.ToString(),
                            TargetId = obj["info"]?[3]?[12]?.ToObject<long>() ?? 0,
                            MedalId = -1,
                            Intimacy = -1,
                            GuardLevel = -1
                        };
                    break;
                case "SEND_GIFT": // 送礼物
                    this.MsgType = DanmakuMsgType.GiftSend;
                    this.GiftName = obj["data"]?["giftName"]?.ToObject<string>();
                    this.GiftId = obj["data"]?["giftId"]?.ToObject<int>();
                    this.UserName = obj["data"]?["uname"]?.ToObject<string>();
                    this.UserID = obj["data"]?["uid"]?.ToObject<long>() ?? 0;
                    this.GiftCount = obj["data"]?["num"]?.ToObject<int>() ?? 0;
                    this.Price = obj["data"]?["price"]?.ToObject<int>() ?? 0;
                    this.AvatarUrl = obj["data"]?["face"]?.ToObject<string>();
                    this.UserMedal = obj["data"]?["medal_info"]?.Count() < 1 ? null :
                        new Medal()
                        {
                            Name = obj["data"]?["medal_info"]?["medal_name"]?.ToString(),
                            Level = obj["data"]?["medal_info"]?["medal_level"]?.ToObject<int>() ?? 0,
                            TargetName = null,
                            TargetId = obj["data"]?["medal_info"]?["target_id"]?.ToObject<long>() ?? 0,
                            MedalId = -1,
                            Intimacy = -1,
                            GuardLevel = obj["data"]?["medal_info"]?["guard_level"]?.ToObject<int>() ?? 0,
                        };
                    break;
                case "GUARD_BUY": // 购买舰长
                    {
                        this.MsgType = DanmakuMsgType.GuardBuy;
                        this.UserID = obj["data"]?["uid"]?.ToObject<long>() ?? 0;
                        this.UserName = obj["data"]?["username"]?.ToObject<string>();
                        this.UserGuardLevel = obj["data"]?["guard_level"]?.ToObject<int>() ?? 0;
                        this.GiftName = this.UserGuardLevel == 3 ? "舰长" : this.UserGuardLevel == 2 ? "提督" : this.UserGuardLevel == 1 ? "总督" : "";
                        this.GiftCount = obj["data"]?["num"]?.ToObject<int>() ?? 0;
                        break;
                    }
                case "SUPER_CHAT_MESSAGE": // SC
                    {
                        this.MsgType = DanmakuMsgType.SuperChat;
                        this.CommentText = obj["data"]?["message"]?.ToString();
                        this.UserID = obj["data"]?["uid"]?.ToObject<long>() ?? 0;
                        this.UserName = obj["data"]?["user_info"]?["uname"]?.ToString();
                        this.Price = obj["data"]?["price"]?.ToObject<double>() ?? 0;
                        this.SCKeepTime = obj["data"]?["time"]?.ToObject<int>() ?? 0;
                        break;
                    }
                case "ROOM_CHANGE": // 房间信息变更
                    {
                        this.MsgType = DanmakuMsgType.RoomChange;
                        this.Title = obj["data"]?["title"]?.ToObject<string>();
                        this.AreaName = obj["data"]?["area_name"]?.ToObject<string>();
                        this.ParentAreaName = obj["data"]?["parent_area_name"]?.ToObject<string>();
                        break;
                    }
                case "ROOM_LOCK": // 房间被锁定
                    {
                        this.MsgType = DanmakuMsgType.RoomLock;
                        break;
                    }
                case "CUT_OFF": // 直播被切断
                    {
                        this.MsgType = DanmakuMsgType.CutOff;
                        break;
                    }
                default:
                    {
                        this.MsgType = DanmakuMsgType.Unknown;
                        break;
                    }
            }
        }
    }
}
