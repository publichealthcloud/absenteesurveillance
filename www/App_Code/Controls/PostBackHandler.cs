using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

/// <summary>
/// Summary description for PostBackHandler
/// </summary>
/// 
namespace Quartz
{    
    public class PostBackHandler3 : WebControl, IPostBackEventHandler
    {
        public delegate void EventHandler(Object sender, string args);

        public event EventHandler PostBack;        

        public void RaisePostBackEvent(string eventArgument)
        {
            PostBack (this, eventArgument);
        }
    }
}