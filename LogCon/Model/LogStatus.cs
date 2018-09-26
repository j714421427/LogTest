using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace LogSys.Models
{
    public enum  LogStatus
    {
        /// <summary>
        /// 不处理
        /// </summary>
        None = 0,

        /// <summary>
        /// 完成
        /// </summary>
        Done = 1,

        /// <summary>
        /// 处理中
        /// </summary>
        Pending = 2,
    }
}