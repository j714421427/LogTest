using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogSys.Models
{
    public class ViewLogContent
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        public LogEventLevel Level { get; set; }
        
        public string Type { get; set; }
        
        public string Content { get; set; }
        
        public string Status { get; set; }

        public DateTime CreatedTime { get; set; }

        public int Raw { get; set; }
    }
}