using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;
using Quartz.Organization;

public partial class manage_manage : System.Web.UI.MasterPage
{
    protected string manage_url = System.Configuration.ConfigurationManager.AppSettings["Site_ManageURL"];
    protected string key = System.Configuration.ConfigurationManager.AppSettings["Site_AutomationKey"];
    protected string final_manage_url;
    protected string page_title;
    protected string manage_health = System.Configuration.ConfigurationManager.AppSettings["Manager_HealthMenu"];
    protected int curr_user_id, insurer_id, space_id, campaign_id;

    public int UserID
    {
        get { return curr_user_id; }
        set { curr_user_id = value; }
    }
    public int InsurerID
    {
        get { return insurer_id; }
        set { insurer_id = value; }
    }
    public int SpaceID
    {
        get { return space_id; }
        set { space_id = value; }
    }
    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }
    public string PageTitle
    {
        get { return page_title; }
        set { page_title = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        int user_id = Convert.ToInt32(Context.Items["UserID"]);
        curr_user_id = user_id;

        litUserID.Text = Convert.ToString(user_id); 

        final_manage_url = manage_url + "/public/launch-as-user.aspx?key=" + key + "&userID=" + curr_user_id;

        if (!Page.IsPostBack)
        {
        }
    }
}