using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;

namespace Quartz.Controls
{
    public partial class CampaignMemberListView : System.Web.UI.UserControl
    {
        protected int user_id, campaign_id;

        public int UserID
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public int CampaignID
        {
            get { return campaign_id; }
            set { campaign_id = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                qPtl_User user = new qPtl_User(user_id);
                lblUserID.Text = Convert.ToString(user_id);
                litEmail.Text = user.Email;
                litUsername.Text = user.UserName;
                litFullName.Text = user.FirstName + " " + user.LastName;

                var u_campaign = qSoc_UserCampaign_View.GetUserCampaign(user_id, campaign_id);

                string enrolled_date = string.Empty;
                if (!String.IsNullOrEmpty(Convert.ToString(u_campaign.CampaignStart)))
                    litEnrolled.Text = String.Format("{0:d}", u_campaign.CampaignStart);
                else
                    litEnrolled.Text = "";

                if (u_campaign.CampaignStatus == "Completed")
                    litGroupRole.Text = "<span class=\"label label-lightred\">Finished</span>";
                if (u_campaign.CampaignStatus == "Not Started")
                    litGroupRole.Text = "<span class=\"label label-lightred\">Not Started</span>";
                else
                    litGroupRole.Text = "<span class=\"label label-satgreen\">In Progress</span>";
            }
        }
    }
}