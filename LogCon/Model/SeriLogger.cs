using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace LogSys.Models
{
    public class SeriLogger
    {
        //public static Logger logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public static string filePath = "C:\\\\temp\\log.txt";
        public static string seriLogFormat = "{Timestamp:yyyy/MM/dd HH:mm:ss} {Message:lj}{NewLine}{Exception}";
        //直接建立logger会lock档案无法读取
        //public static Logger logger = new LoggerConfiguration().WriteTo.RollingFile(filePath, outputTemplate: seriLogFormat).CreateLogger();
        //public static Logger logger = new LoggerConfiguration().CreateLogger();

        public static void WriteLog(LogEventLevel level, TransType type, string content)
        {
            string result = string.Empty;

            var log = new LogContent(null)
            {
                Level = level,
                Status = level == LogEventLevel.Error ? LogStatus.Pending : LogStatus.None,
                Type = type,
                Content = content,
            };


            result = CreateLogStr(log);
            var logger = new LoggerConfiguration().WriteTo.RollingFile(filePath, outputTemplate: seriLogFormat).CreateLogger();
            logger.Write(level, result);
            logger.Dispose();

        }

        public static void WriteLog(MethodBase method, Exception ex, string message)
        {
            string result = string.Empty;

            string methodNmae = GetCurrentMethodInfo(method);

            var log = new LogContent(null)
            {
                Level = LogEventLevel.Error,
                Status = LogStatus.Pending,
                Type = TransType.Unknow,
                Content = message,
            };

            result = CreateLogStr(log);

            var logger = new LoggerConfiguration().WriteTo.RollingFile(filePath, outputTemplate: seriLogFormat).CreateLogger();
            logger.Write(log.Level, result);
            logger.Dispose();
        }

        public static List<ViewLogContent> ReadLog()
        {
            var fileName = $"C:\\\\temp\\log-{DateTime.Now.ToString("yyyyMMdd")}.txt";
            List<ViewLogContent> result = new List<ViewLogContent>();

            var rawStr = string.Empty;

            using (StreamReader sr = new StreamReader(fileName))
            {
                int i = 0;
                JObject obj = new JObject();
                while ((rawStr = sr.ReadLine()) != null)
                {
                    obj = new JObject();

                    var sp = rawStr.Split('[');
                    obj.Add(new JProperty("CreatedTime", rawStr.Substring(0, 19)));
                    obj.Add(new JProperty("Raw", i.ToString()));
                    foreach (var item in sp)
                    {
                        var index = item.IndexOf("]");
                        if (index < 1) continue;
                        var front = item.Substring(0, index);
                        var back = item.Substring(index + 1, item.Length - index - 1);
                        obj.Add(new JProperty(front, back));
                    }

                    string jsonString = JsonConvert.SerializeObject(obj);
                    result.Add(JsonConvert.DeserializeObject<ViewLogContent>(jsonString));
                    i++;
                }
                sr.Close();
            }

            return result;

        }

        private static string GetCurrentMethodInfo(MethodBase method)
        {
            //取得當前方法類別命名空間名稱 取得當前類別名稱 取得當前所使用的方法
            return $"[Method]{method.DeclaringType.Namespace}.{method.DeclaringType.FullName}.{method.Name}";
        }

        private static string CreateLogStr(object content)
        {

            var props = content.GetType().GetProperties()
                .Where(o => o.GetValue(content) != null);

            string[] signArray = props
                .Select(o => $"[{o.CustomAttributes.Single().ConstructorArguments.Single().Value}]{o.GetValue(content)}").ToArray();

            return string.Join("", signArray);
        }
    }
}