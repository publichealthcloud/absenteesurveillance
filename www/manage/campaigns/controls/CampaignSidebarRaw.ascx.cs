using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Report;

public partial class manage_programs_controls_campaign_sidebar : System.Web.UI.UserControl
{
    protected int campaign_id;
    protected string campaign_name;
    protected qRpt_CampaignOverviewReport report;
    protected qRpt_CampaignReportPreference pref;
    protected qSoc_Campaign campaign;
    protected List<CampaignReport> s_report;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }
    public string CampaignName
    {
        get { return campaign_name; }
        set { campaign_name = value; }
    }
    public qRpt_CampaignOverviewReport Report
    {
        get { return report; }
        set { report = value; }
    }
    public List<CampaignReport> S_Report
    {
        get { return s_report; }
        set { s_report = value; }
    }
    public qRpt_CampaignReportPreference Pref
    {
        get { return pref; }
        set { pref = value; }
    }
    public qSoc_Campaign Campaign
    {
        get { return campaign; }
        set { campaign = value; }
    }  
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int num_enrolled = 0;
            
            if (s_report != null)
            {
                num_enrolled = s_report[0].num_enrolled_mobile + s_report[0].num_enrolled_sms + s_report[0].num_enrolled_web + s_report[0].num_enrolled_email;
                litEnrolledMembers.Text = Convert.ToString(num_enrolled);

                litInvitations.Text = Convert.ToString(report.Invite_NumUniqueInvites);
                litVisitedEnrollment.Text = "na";
                litVisitorConversionRate.Text = "na";

                decimal num_invited = Convert.ToDecimal(report.Invite_NumUniqueInvites);
                decimal percent_invited = 0;

                if (num_invited > 0 && num_enrolled > 0)
                {
                    percent_invited = num_enrolled / num_invited;
                    percent_invited = percent_invited * 100;
                    litInvitationConversionRate.Text = Math.Round(percent_invited, 2) + "%";
                }
                else
                    litInvitationConversionRate.Text = "na";

                decimal num_total_enrolled = Convert.ToDecimal(num_enrolled);
                litWebEnrolled.Text = Convert.ToString(s_report[0].num_enrolled_web);
                litMobileAppEnrolled.Text = Convert.ToString(s_report[0].num_enrolled_mobile);
                litSMSEnrolled.Text = Convert.ToString(s_report[0].num_enrolled_sms);
                litEmailEnrolled.Text = Convert.ToString(s_report[0].num_enrolled_email);
                litCancelled.Text = Convert.ToString(s_report[0].num_stopped);

                // calculate percentages
                decimal num_web = Convert.ToDecimal(s_report[0].num_enrolled_web);
                decimal num_app = Convert.ToDecimal(s_report[0].num_enrolled_mobile);
                decimal num_sms = Convert.ToDecimal(s_report[0].num_enrolled_sms);
                decimal num_email = Convert.ToDecimal(s_report[0].num_enrolled_email);

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

                litWebEnrolled.Text = Convert.ToString(s_report[0].num_enrolled_web);
                litMobileAppEnrolled.Text = Convert.ToString(s_report[0].num_enrolled_mobile);
                litSMSEnrolled.Text = Convert.ToString(s_report[0].num_enrolled_sms);
                litEmailEnrolled.Text = Convert.ToString(s_report[0].num_enrolled_email);

                // calculate percentages
                decimal num_waiting = Convert.ToDecimal(s_report[0].num_waiting);
                decimal num_in_progress = Convert.ToDecimal(s_report[0].num_in_progress);
                decimal num_finished = Convert.ToDecimal(s_report[0].num_finished);

                decimal percent_waiting = 0;
                decimal percent_in_progress = 0;
                decimal percent_finished = 0;

                if (num_waiting > 0)
                {
                    percent_waiting = num_waiting / num_total_enrolled;
                    percent_waiting = percent_waiting * 100;
                }
                if (num_in_progress > 0)
                {
                    percent_in_progress = num_in_progress / num_total_enrolled;
                    percent_in_progress = percent_in_progress * 100;
                }
                if (num_finished > 0)
                {
                    percent_finished = num_finished / num_total_enrolled;
                    percent_finished = percent_finished * 100;
                }
                litWaitingPercent.Text = "style=\"width:" + percent_waiting + "%\"";
                litInProgressPercent.Text = "style=\"width:" + percent_in_progress + "%\"";
                litFinishedPercent.Text = "style=\"width:" + percent_finished + "%\"";

                litWaiting.Text = Convert.ToString(num_waiting);
                litInProgress.Text = Convert.ToString(num_in_progress);
                litFinished.Text = Convert.ToString(num_finished);
            }
            else
            {
                // load typical campaign information
                plhCampaignInfo.Visible = false;
            }
        }
    }
}