using Newtonsoft.Json;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogSys.Models
{
    public class LogContent
    {
        public LogContent(Guid? guid) {
            this.Id = guid ?? Guid.NewGuid();
        }

        [JsonProperty("Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [JsonProperty("Level")]
        public LogEventLevel Level { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        [JsonProperty("Type")]
        public TransType Type { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [JsonProperty("Content")]
        public string Content { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        [JsonProperty("Status")]
        public LogStatus Status { get; set; }
    
        //public DateTime CreatedTime { get; set; }
    }
}