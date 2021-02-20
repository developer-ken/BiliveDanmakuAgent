using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace BiliveDanmakuAgent.Core
{
    public static class Utils
    {
        public static event ExceptionHappenedEvt ExceptionHappened;
        public static event LogOutputEvent LogOutput;

        internal static byte[] ToBE(this byte[] b)
        {
            if (BitConverter.IsLittleEndian)
            {
                return b.Reverse().ToArray();
            }
            else
            {
                return b;
            }
        }

        internal static void ReadB(this NetworkStream stream, byte[] buffer, int offset, int count)
        {
            if (offset + count > buffer.Length)
            {
                throw new ArgumentException();
            }

            int read = 0;
            while (read < count)
            {
                var available = stream.Read(buffer, offset, count - read);
                if (available == 0)
                {
                    throw new ObjectDisposedException(null);
                }
                read += available;
                offset += available;
            }
        }

        internal static string RemoveInvalidFileName(this string name, bool ignore_slash = false)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (ignore_slash && (c == '\\' || c == '/'))
                    continue;
                name = name.Replace(c, '_');
            }
            return name;
        }

        public static bool CopyPropertiesTo<T>(this T source, T target) where T : class
        {
            if (source == null || target == null || source == target) { return false; }
            foreach (var p in source.GetType().GetProperties())
            {
                if (Attribute.IsDefined(p, typeof(DoNotCopyProperty)))
                {
                    continue;
                }

                var val = p.GetValue(source);
                if (val == null || !val.Equals(p.GetValue(target)))
                {
                    p.SetValue(target, val);
                }
            }
            return true;
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public class DoNotCopyProperty : Attribute { }

        internal static void Log(int id, string message, Exception exception = null)
        {
            if (exception == null)
            {
                LogOutput?.Invoke(null, message);
            }
            else
            {
                ExceptionHappened?.Invoke(null, exception, message);
            }
        }

        private static string _useragent;
        internal static string UserAgent
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_useragent))
                {
                    string version = typeof(Utils).Assembly.GetName().Version.ToString();
                    _useragent = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36 BililiveRecorder/{version} (+https://github.com/Bililive/BililiveRecorder;bliverec@danmuji.org)";
                }
                return _useragent;
            }
        }

    }
}
