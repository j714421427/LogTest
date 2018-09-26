using LogSys.Models;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogCon
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("[1]WriteLog[2]ReadLog");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("WriteLog");
                        WriteLog(10);
                        break;
                    case "2":
                        Console.WriteLine("ReadLog");
                        ReadLog();
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                } 
            }
            
        }

        public static void WriteLog(int count)
        {
            for (int i = 0; i <= count; i++)
            {
                Thread.Sleep(30);
                var type = (TransType)new Random().Next(0, 5);

                var level = (LogEventLevel)new Random().Next(0, 5);

                

                var content = "test";

                SeriLogger.WriteLog(level, type, content);
            }

        }

        public static void ReadLog()
        {
           var logList =  SeriLogger.ReadLog();

            foreach(var item in logList)
            {
                Console.WriteLine($"level = {item.Level}, type = {item.Type}, status = {item.Status},content = {item.Content}");
            }
        }
    }
}
