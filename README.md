# BiliveDanmakuAgent
Bilibili直播快速开发平台  引入即用，快速上手  
弹幕通讯相关解析摘录自[Bililive/BililiveRecorder](https://github.com/Bililive/BililiveRecorder)，感谢原作者。  
示例代码：
```csharp
using BiliveDanmakuAgent;

namespace Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DanmakuApi dc = new DanmakuApi(2064239,  //直播间号
                ""                                   //登录Cookie
                );
            dc.ConnectAsync().Wait();                //连接到弹幕服务器
            dc.DanmakuMsgReceivedEvent += Dc_DanmakuMsgReceivedEvent; //此事件在收到任何服务器下发消息时触发
            dc.CommentReceived += Dc_CommentReceived;//此事件在收到普通弹幕时触发
            while (true) ;                           //阻塞主线程，不让程序直接退出。实际编程时此处可以编写其它逻辑
        }

        private static void Dc_CommentReceived(object sender, BiliveDanmakuAgent.Model.DanmakuReceivedEventArgs e)
        {
            Console.WriteLine(e.Danmaku.UserName + ": " + e.Danmaku.CommentText); //输出弹幕发送者和弹幕内容
        }

        private static void Dc_DanmakuMsgReceivedEvent(object sender, BiliveDanmakuAgent.Model.DanmakuReceivedEventArgs e)
        {
            Console.WriteLine(e.Danmaku.RawString); //输出收到的原始Json消息
        }
    }
}
```