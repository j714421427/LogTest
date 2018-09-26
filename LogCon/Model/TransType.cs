using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogSys.Models
{
    public enum TransType
    {
        Unknow = 0,
        DepositRequest = 1,
        DepositResponse = 2,
        DepositQueryRequest = 3,
        DepositQueryResponse = 4,
        DepositCallback = 5,
    }

}