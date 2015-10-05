using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Report;
using Quartz.Social;
using Quartz.Portal;

public partial class manage_spaces_controls_sidebar : System.Web.UI.UserControl
{
    protected int space_id;

    public int SpaceID
    {
        get { return space_id; }
        set { space_id = value; }
    }   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            List<SpaceReport> report = new List<SpaceReport>();
            report = qRpt_SpaceReport.GenerateSpaceReport(space_id);

            //qSoc_Space space = new qSoc_Space(space_id);
            //litSpaceTitle.Text = space.SpaceShortName;
            litSpaceTitle.Text = "Program Overview";

            if (report != null)
            {
                litNumCampaigns.Text = Convert.ToString(report[0].num_active_campaigns);

                int num_enrolled = 0;
                int num_web_enrolled = 0;
                int num_sms_enrolled = 0;
                int num_mobile_enrolled = 0;
                int num_email_enrolled = 0;

                string cam_details = string.Empty;
                int total_enrolled = 0;

                foreach (var c in report[0].campaigns)
                {
                    num_web_enrolled = num_web_enrolled + c.num_enrolled_web;
                    num_sms_enrolled = num_sms_enrolled + c.num_enrolled_sms;
                    num_mobile_enrolled = num_mobile_enrolled + c.num_enrolled_mobile;
                    num_email_enrolled = num_email_enrolled + c.num_enrolled_email;

                    int c_enrolled = c.num_enrolled;
                    total_enrolled = total_enrolled + (num_web_enrolled + num_sms_enrolled + num_mobile_enrolled + num_email_enrolled);

                    decimal total_this_cat = Convert.ToDecimal(c_enrolled);
                    decimal percent_this_camp = 0;

                    percent_this_camp = total_this_cat / Convert.ToDecimal(report[0].num_enrolled);
                    percent_this_camp = percent_this_camp * 100;

                    cam_details += "<div class=\"pagestats bar\">";
                    cam_details += "<span><a href=\"/manage/campaigns/campaign-details.aspx?campaignID=" + c.campaign_id + "\">" + c.campaign_name + " (" + c_enrolled + ") <i class=\"icon-circle-arrow-right\"></i></a></span>";
                    cam_details += "<div class=\"progress small\"><div class=\"bar bar-teal\" style=\"width:" + percent_this_camp + "%\"></div></div>";
                    cam_details += "</div>";
                }

                if (!String.IsNullOrEmpty(cam_details))
                    litCampaignDetails.Text = cam_details;

                num_enrolled = num_web_enrolled + num_sms_enrolled + num_mobile_enrolled + num_email_enrolled;
                litEnrolled.Text = Convert.ToString(report[0].num_enrolled);

                litWebEnrolled.Text = Convert.ToString(num_web_enrolled);
                litMobileAppEnrolled.Text = Convert.ToString(num_mobile_enrolled);
                litSMSEnrolled.Text = Convert.ToString(num_sms_enrolled);
                litEmailEnrolled.Text = Convert.ToString(num_email_enrolled);

                // calculate percentages
                decimal num_total_enrolled = Convert.ToDecimal(num_enrolled);
                decimal num_web = Convert.ToDecimal(num_web_enrolled);
                decimal num_app = Convert.ToDecimal(num_mobile_enrolled);
                decimal num_sms = Convert.ToDecimal(num_sms_enrolled);
                decimal num_email = Convert.ToDecimal(num_email_enrolled);

                decimal percent_web = 0;
                decimal percent_app = 0;
                decimal percent_sms = 0;
                decimal percent_email = 0;

                if (num_web > 0)
                {
                    percent_web = num_web / num_total_enrolled;
                    percent_web = percent_web * 100;
                }
                if (num_app > 0)
                {
                    percent_app = num_app / num_total_enrolled;
                    percent_app = percent_app * 100;
                }
                if (num_sms > 0)
                {
                    percent_sms = num_sms / num_total_enrolled;
                    percent_sms = percent_sms * 100;
                }
                if (num_email > 0)
                {
                    percent_email = num_email / num_total_enrolled;
                    percent_email = percent_email * 100;
                }
                litWebEnrolledPercent.Text = "style=\"width:" + percent_web + "%\"";
                litMobileAppEnrolledPercent.Text = "style=\"width:" + percent_app + "%\"";
                litSMSEnrolledPercent.Text = "style=\"width:" + percent_sms + "%\"";
                litEmailEnrolledPercent.Text = "style=\"width:" + percent_email + "%\"";
            }
        }
    }
}