using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Quartz.Portal;
using Quartz.Social;

public partial class utilities_manage_user_access : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // ALL USER ACTIONS MANAGED BY THE qPtl_UserActions table
        // PRIORITY #1 - ACCOUNT FINALIZATION TASKS
        // has support for same session opt out, priority, skip options
            // finalize account - #1 - upload profile picture (or select default), what did i join (first login), select health insurer (could also be set via space code/invitation) -- > do NOT allow user to skip
            // finalize account - #2 - add mobile phone allows to use mobile services + account protection --> verify mobile phone second screen --> sign up for mobile programs --> can be skipped
            // finalize account - #3 - add social integration (Facebook, Twitter, etc) 
            // finalize account - #4 - complete about me section in their profile

        // PRIORITY #2 - USER/ROLE ACTIONS
            // used for assessments/surveys/etc.
            // tasks added for users

        // PRIORITY #3 - MISC TASKS
            // #1 why are you a member? (if nothing else and hasn't been updated in 1 month plus, every 1 month --> at least 1 week between misc requests)
            // #2 mobile integration (if nothing else and haven't signed up yet, every 1 month since last asked --> at least 1 week between misc requests)
            // #3 social integration (if nothing else and haven't completed social integration, every 1 month --> at least 1 week between misc requests)
            // #4 sign up for services (if nothing else and haven't ever signed up for services, every 1 month --> at least 1 week between misc requests)
            // #5 invite friends (if nothing else and haven't ever invited friends, every 1 month --> at least 1 week between misc requests)

        // USER MANAGEMENT CODE 
            // IF NO OTHER PENDING ACTIONS (#1-#3), the redirect to appropriate user level starting locations
            // if manager only --> /manage/
            // if social primary --> /social/frontpage/ (NOTE: a link to the manager will appear in the black header)
            // if adult basic --> /basic/
            // *** need to prepare for 3 other types of users --> adult social, parent basic, parent social) ***
            
            // get highest role and redirect accordingly

        // short-term hacks -- if logging into a site like T2X for the first time; we want the user to complete their health info -- ask what insurance provider they have
        // this will create a health record which will also become the basis for their health assessment information

        qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
        int num_user_logins = user.NumberLogins;

        // check to see if highest role is as a Reports viewer
        string roles = Convert.ToString(Context.Items["UserRoles"]);
        if (String.IsNullOrEmpty(roles))
            Response.Redirect("~/logout.aspx");

        if (user.HighestRole.Contains("Space Report") && (!roles.Contains("Teen") && !roles.Contains("Advisor") && !roles.Contains("Host") && !roles.Contains("Site Admin")))
            Response.Redirect("/manage/spaces/default.aspx");
        else if (user.HighestRole.Contains("Campaign Report") && (!roles.Contains("Teen") && !roles.Contains("Advisor") && !roles.Contains("Host") && !roles.Contains("Site Admin")))
            Response.Redirect("/manage/campaigns/default.aspx");
        else if (user.HighestRole.Contains("School District Report") && (!roles.Contains("Teen") && !roles.Contains("Advisor") && !roles.Contains("Host") && !roles.Contains("Site Admin")))
            Response.Redirect("/manage/school-districts/default.aspx");

        if (Convert.ToString(Session["PriorUserAction"]) != "Yes")
        {
            DateTime today = DateTime.Now;
            string action_url = string.Empty;
            int after_num_logins = 0;
            int action_id = 0;

            DataTable dt_actions = new DataTable();

            dt_actions = qPtl_UserAction_View.GetUserActionsByFilter(1, user.UserID, "Priority ASC", "Yes", 0, Convert.ToString(today), "", false, false, Convert.ToInt32(Context.Items["SessionID"]));
            
            if (dt_actions != null)
            {
                foreach (DataRow dr in dt_actions.Rows)
                { 
                    if (!String.IsNullOrEmpty(Convert.ToString(dr["AfterNumLogins"])))
                    {
                        after_num_logins = Convert.ToInt32(dr["AfterNumLogins"]);

                    }

                    if (after_num_logins > 0)
                    {
                        if (after_num_logins <= num_user_logins)
                        {
                            action_url = Convert.ToString(dr["URL"]);
                            action_id = Convert.ToInt32(dr["ActionID"]);
                        }
                    }
                    else
                    {
                        action_url = Convert.ToString(dr["URL"]);
                        action_id = Convert.ToInt32(dr["ActionID"]);
                    }
                }
            }            

            if (action_id > 0 && !String.IsNullOrEmpty(action_url))
            {
                Response.Redirect(action_url + "?actionID=" + action_id);
            }
        }

        // check to see if there is a redirect record for this user type
        qPtl_UserLevelRedirect redirect = new qPtl_UserLevelRedirect(Convert.ToInt32(Context.Items["ScopeID"]), user.HighestRank, (int)qSoc_ContentType.Types.Login);
        string social_default_url = "~/social/explore/frontpage.aspx";
        string social_default = string.Empty;
        if (!String.IsNullOrEmpty(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_SocialStart"])))
            social_default = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_SocialStart"]);

        switch (social_default)
        {
            case "frontpage":
                social_default_url = "~/social/explore/frontpage.aspx";
                break;
            case "feed":
                social_default_url = "~/social/explore/feed.aspx";
                break;
            case "campaign":
                social_default_url = "~/social/learning/campaigns/user-campaigns-list.aspx";
                break;
            case "lms":
                social_default_url = "~/social/lms/default.aspx";
                break;
            case "learning":
                social_default_url = "~/social/learning/learning-resources.aspx";
                break;
        }

        if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["spaceID"])))
        {
            try
            {
                Response.Redirect("~/social/spaces/space-details.aspx?spaceID=" + Request.QueryString["spaceID"]);
            }
            catch
            {
                // do nothing
            }
        }

        string reg_mode = string.Empty;
        if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
            reg_mode = Request.QueryString["mode"];

        if (reg_mode == "registration")
            Response.Redirect("~/");

        if (redirect != null)
        {
            if (redirect.UserLevelRedirectID > 0)
                Response.Redirect(redirect.URL);
            else
                Response.Redirect(social_default_url);
        }
        else
            Response.Redirect(social_default_url);
    }
}