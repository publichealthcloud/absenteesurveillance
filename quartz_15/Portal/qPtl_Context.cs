using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public interface ISessionContext
    {
        qPtl_User User { get; set; }
        int UserID { get; set; }
        int SessionID { get; set; }
        int TempSessionID { get; set; }
    }
}
