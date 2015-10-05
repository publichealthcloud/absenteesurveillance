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
            // load site preferences
            string site_title = System.Configuration.ConfigurationManager.AppSettings["Site_Title"];
            string site_name = System.Configuration.ConfigurationManager.AppSettings["Site_ShortName"];
            string logo_url = System.Configuration.ConfigurationManager.AppSettings["Site_LogoUrl"];
            string nav_mode = System.Configuration.ConfigurationManager.AppSettings["Site_NavMode"];
            string join_group_type = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_JoinGroupType"]);
            string group_type = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Members_GroupType"]);               // either health or school
            string invitation_type = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Members_InvitationTypes"]);
            string site_explorer_active = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_ExplorersActive"]);
            litSiteName.Text = site_name;

            if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
                campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);

            string curr_url = Request.Url.ToString();

            if (!String.IsNullOrEmpty(Request.QueryString["schoolID"]))
            {
                qOrg_School school = new qOrg_School(Convert.ToInt32(Request.QueryString["schoolID"]));
                if (school.SchoolID > 0)
                    manage_page_title.Text = school.School;
            }
            else if (curr_url.Contains("school-district"))
                manage_page_title.Text = "District Absentee Data";
            else
                manage_page_title.Text = "Manage " + site_name;

            if (!String.IsNullOrEmpty(site_explorer_active))
            {
                if (site_explorer_active == "true")
                    plhExplorers.Visible = true;
            }

            qPtl_User user = new qPtl_User(user_id);
            lblUserName.Text = user.UserName;
            lblNotifications.Text = "";

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string img_url = string.Empty;
            if (!String.IsNullOrEmpty(user.ProfilePict))
                img_url = baseUrl + "user_data/" + user.UserName + "/" + user.ProfilePict + ".ashx?width=32&height=27&mode=crop";
            else
                img_url = baseUrl + "images/mylife_portrait_default.jpg.ashx?width=32&height=27&mode=crop";

            imgProfilePicture.ImageUrl = img_url;

            string roles = Convert.ToString(Context.Items["UserRoles"]);
            if (roles.Contains("Site Host") || roles.Contains("Member") || roles.Contains("Site Admin"))
                plhAccessSite.Visible = true;

            string curr_space_id = Convert.ToString(Session["manage_space_id"]);
            space_id = Convert.ToInt32(Session["manage_space_id"]);

            // hide nav options (tiles and breadcrumbs) and only show based one analysis below
            plhSiteLevelNavTiles.Visible = false;
            plhSiteHomeNav.Visible = false;
            plhSpaceHomeNav.Visible = false;
            plhBreadcrumbs.Visible = false;
            plhCampaignHomeNav.Visible = false;
            plhCurrentPageNav.Visible = false;
            plhSchoolDistrict.Visible = false;

            if (user.HighestRole == "Site Admin" || user.HighestRole == "Site Host" || user.HighestRole == "Site Reports")
            {
                plhSite.Visible = true;
                ddlSpaces.Visible = false;
                plhSiteLevelNavTiles.Visible = true;
                if (curr_url.Contains("/manage/site/"))
                {
                    if (!curr_url.Contains("/manage/site/default.aspx"))
                    {
                        plhSiteHomeNav.Visible = true;
                        plhBreadcrumbs.Visible = true;
                    }
                }
                else if (curr_url.Contains("/manage/spaces/"))
                {
                    plhSiteLevelNavTiles.Visible = false;
                    plhSiteHomeNav.Visible = true;
                    if (!curr_url.Contains("/manage/spaces/default.aspx"))
                        if (space_id > 0)
                            plhSpaceHomeNav.Visible = true;
                    plhBreadcrumbs.Visible = true;
                }
                else if (curr_url.Contains("/manage/campaigns/"))
                {
                    plhSiteLevelNavTiles.Visible = false;
                    plhSiteHomeNav.Visible = true;
                    if (space_id > 0)
                        plhSpaceHomeNav.Visible = true;
                    plhBreadcrumbs.Visible = true;
                    plhCampaignHomeNav.Visible = true;
                }
            }
            else if (user.HighestRole == "School District Admin" || user.HighestRole == "School District Reports")
            {
                plhSchoolDistrict.Visible = true;
                ddlSpaces.Visible = false;
            }
            else if (user.HighestRole == "Insurer Admin" || user.HighestRole == "Insurer Reports")
            {
                plhInsurer.Visible = true;
                ddlSpaces.Visible = false;
            }
            else if (user.HighestRole == "Advisor" || user.HighestRole == "Space Admin" || user.HighestRole == "Space Reports" || roles.Contains("Space Admin") || roles.Contains("Space Reports"))
            {
                plhSpace.Visible = true;
            }
            else if (user.HighestRole == "Campaign Admin" || user.HighestRole == "Campaign Reports")
                plhCampaign.Visible = true;

            if (roles.Contains("Site Admin") || roles.Contains("Host") || roles.Contains("Site Reports") || roles.Contains("Insurer Admin") || roles.Contains("Insurer Reports"))
            {
                populateSpaces("all");
            }
            else
                populateSpaces("limited");

            if (!String.IsNullOrEmpty(curr_space_id))
            {
                // load data for this space
                loadSpaceData(Convert.ToInt32(Session["manage_space_id"]));
                ddlSpaces.SelectedValue = Convert.ToString(curr_space_id);
            }
            else
            {
                // get the first option in the list and load information for that
                if (ddlSpaces.Items.Count > 0)
                {
                    string first_item = ddlSpaces.Items[0].Value;
                    if (!String.IsNullOrEmpty(first_item))
                    {
                        loadSpaceData(Convert.ToInt32(first_item));
                        Session["manage_space_id"] = Convert.ToString(ddlSpaces.SelectedValue);
                        curr_space_id = Convert.ToString(Session["manage_space_id"]);
                        ddlSpaces.SelectedValue = Convert.ToString(Session["manage_space_id"]);
                    }
                }
            }

            // menu optimizations
            plhGroupsHealthProviders.Visible = false;
            plhGroupsSchools.Visible = true;
            if (!String.IsNullOrEmpty(join_group_type))
            {
                if (join_group_type == "health")
                {
                    plhGroupsHealthProviders.Visible = true;
                    plhGroupsSchools.Visible = false;
                }
            }

            if (invitation_type == "family")
                plhFamilyInvitations.Visible = true;
            else if (invitation_type == "moderated group")
                plhGroupInvitations.Visible = true;

            // check for back nav options
            if (curr_url.Contains("campaign-details.aspx"))
            {
                litPageTitle.Text = "Campaign Dashboard";
            }
            else if (curr_url.Contains("campaigns/enrolled-members.aspx"))
            {
                litPageTitle.Text = "Enrolled Members";
            }
            else if (curr_url.Contains("campaigns/health-kit-orders.aspx"))
            {
                litPageTitle.Text = "Health Kits Ordered";
            }
            else if (curr_url.Contains("campaign-pages.aspx"))
            {
                litPageTitle.Text = "Campaign Pages";
            }
            else if (curr_url.Contains("campaign-emails.aspx"))
            {
                litPageTitle.Text = "Campaign Emails";
            }
            else if (curr_url.Contains("campaign-messages.aspx"))
            {
                litPageTitle.Text = "Campaign Messages";
            }
            else if (curr_url.Contains("campaign-assessments.aspx"))
            {
                litPageTitle.Text = "Campaign Assessments";
            }
            else if (curr_url.Contains("campaign-invited.aspx"))
            {
                litPageTitle.Text = "Campaign Invited Contacts";
            }
            else if (curr_url.Contains("campaign-members.aspx"))
            {
                litPageTitle.Text = "Campaign Enrolled Members";
            }
            else if (curr_url.Contains("campaign-reports.aspx"))
            {
                litPageTitle.Text = "Campaign Reports";
            }
            else if (curr_url.Contains("campaign-library.aspx"))
            {
                litPageTitle.Text = "Campaign Library";
            }

            if (!String.IsNullOrEmpty(manage_health))
                if (manage_health == "true")
                    plhHealthMenu.Visible = true;

            plhGroupTypeSchool.Visible = true;
            plhGroupTypeHealth.Visible = false;
            if (!String.IsNullOrEmpty(group_type))
            {
                if (group_type == "health")
                {
                    plhGroupTypeHealth.Visible = true;
                    plhGroupTypeSchool.Visible = false;
                }
            }
        }
    }

    protected void loadSpaceData(int space_id)
    {
        //litMessage.Text = "current space id = " + space_id;
    }

    protected void btnLogoff_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/logout.aspx", true);
    }

    protected void populateSpaces(string mode)
    {
        if (mode == "all")
            ddlSpaces.DataSource = qSoc_Space.GetSpaces();
        else
            ddlSpaces.DataSource = qPtl_SpaceAdmin_View.GetSpaceAdminsByUser(Convert.ToInt32(Context.Items["UserID"]));
        ddlSpaces.DataTextField = "SpaceShortName";
        ddlSpaces.DataValueField = "SpaceID";
        ddlSpaces.DataBind();
        if (mode == "all")
            ddlSpaces.Items.Insert(0, new ListItem("All Members", "0"));
        else
        {
            if (ddlSpaces.Items.Count > 0)
            {
                string first_item = ddlSpaces.Items[0].Value;
                if (!String.IsNullOrEmpty(first_item))
                {
                    loadSpaceData(Convert.ToInt32(first_item));
                    Session["manage_space_id"] = Convert.ToString(ddlSpaces.SelectedValue);
                    ddlSpaces.SelectedValue = Convert.ToString(Session["manage_space_id"]);
                }
            }
        }
    }

    protected void ddlSpaces_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(ddlSpaces.SelectedValue))
        {
            int space_id = Convert.ToInt32(ddlSpaces.SelectedValue);
            Session["manage_space_id"] = space_id;
            loadSpaceData(space_id);
        }
    }
}