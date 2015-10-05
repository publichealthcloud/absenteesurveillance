<%@ Application Language="C#" %>

<%@ Import Namespace="Quartz.Portal" %>
<%@ Import Namespace="Quartz.Social" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.ServiceModel.Activation" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Threading" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles
                    string[] userData = ticket.UserData.Split(';');
                    string[] roles = userData[1].Split(',');
                    HttpContext.Current.User = new GenericPrincipal(id, roles);

                    Context.Items["UserID"] = Convert.ToInt32(ticket.Name);
                }
            }
        }
    }
    
    protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
        {
            if (!Context.SkipAuthorization)
            {
                if (Context.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(Context.Request.Cookies[FormsAuthentication.FormsCookieName].Value);

                    string[] userData = ticket.UserData.Split(';');
                    int sessionID = Convert.ToInt32(userData[0]);
                    string roles = Convert.ToString(userData[1]);
                    int scopeID = Convert.ToInt32(userData[2]);
                    int userID = Convert.ToInt32(ticket.Name);

                    Context.Items.Add("SessionID", sessionID);
                    Context.Items["UserID"] = userID;
                    Context.Items["ScopeID"] = scopeID;
                    Context.Items["UserRoles"] = roles;
                    
                    // update session
                    qPtl_Sessions session = new qPtl_Sessions(sessionID);
                    if (session.SessionID > 0)
                    {
                        session.LastTimeSeen = DateTime.Now;
                        session.Update();
                    }
                }
            }
        }
    }
        
</script>
