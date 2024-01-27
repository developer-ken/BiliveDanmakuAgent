using BiliApi.Modules;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliveDanmakuAgent.Model
{
    public enum DanmakuMsgType
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
    public class Danmaku
    {
        public RoomInfo RoomInfo { get; set; } = new RoomInfo();

        /// <summary>
        /// 消息類型
        /// </summary>
        public DanmakuMsgType MsgType { get; set; }

        /// <summary>
        /// 用户当前佩戴的牌子
        /// </summary>
        public Medal? UserMedal { get; set; }

        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string? AvatarUrl { get; set; }

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
        /// SC 价格 / 礼物单价
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
        /// 礼物Id
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
        /// 礼物是否是金瓜子礼物
        /// </summary>
        public bool GiftGoldcoin { get; set; }

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
        /// 原始数据
        /// </summary>
        public string RawString { get; set; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public JObject? RawObject { get; set; }
    }
}
