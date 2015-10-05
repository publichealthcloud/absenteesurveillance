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
            if (report != null)
            {
                if (report.CampaignReportOverviewID > 0)
                {
                    decimal total_contacted = report.Invite_NumEmail + report.Invite_NumMail + report.Invite_NumSMS;
                    decimal total_enrolled = report.Enroll_NumWeb + report.Enroll_NumApp + report.Enroll_NumSMS;
                    decimal conversion_rate = 0;
                    if (total_contacted > 0)
                        conversion_rate = (total_enrolled / total_contacted) * 100;

                    conversion_rate = Math.Round(conversion_rate, 1);
                    litNumContactsAvailable.Text = Convert.ToString(report.Invite_NumContactsAvailable);
                    litNumUniqueInvites.Text = Convert.ToString(report.Invite_NumUniqueInvites);
                    litNumContacted.Text = Convert.ToString(total_contacted);
                    litVisitedEnrollment.Text = Convert.ToString(report.Enroll_VisitedEnrollment);
                    litEnrolledMembers.Text = Convert.ToString(total_enrolled);
                    litConversionRate.Text = Convert.ToString(conversion_rate) + "%";
                    litInProgress.Text = Convert.ToString(report.Learn_NumStarted);
                    litFinished.Text = Convert.ToString(report.Learn_NumFinished);
                    litNumFlyers.Text = Convert.ToString(report.Invite_NumFlyers);

                    litEnrolledWeb.Text = Convert.ToString(report.Enroll_NumWeb);
                    litEnrolledMobileApp.Text = Convert.ToString(report.Enroll_NumApp);
                    litEnrolledSMS.Text = Convert.ToString(report.Enroll_NumSMS);

                    litCancelled.Text = Convert.ToString(report.Learn_NumCancelled);

                    if (report.Display_EmailSummary == true)
                        plhEmailSideBarSummary.Visible = true;
                    else
                        plhEmailSideBarSummary.Visible = false;

                    litLastCompiled.Text = Convert.ToString(report.LastTimeCompiled) + "<br>&nbsp;";

                    decimal num_total_enrolled = total_enrolled;
                    decimal num_web = Convert.ToDecimal(report.Enroll_NumWeb);
                    decimal num_app = Convert.ToDecimal(report.Enroll_NumApp);
                    decimal num_sms = Convert.ToDecimal(report.Enroll_NumSMS);

                    decimal percent_web = 0;
                    decimal percent_app = 0;
                    decimal percent_sms = 0;

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
                    litWebPercent.Text = "style=\"width:" + percent_web + "%\"";
                    litAppPercent.Text = "style=\"width:" + percent_app + "%\"";
                    litSMSPercent.Text = "style=\"width:" + percent_sms + "%\"";

                    litEmailSent.Text = Convert.ToString(report.Email_NumSent);
                    litEmailBounce.Text = Convert.ToString(report.Email_NumBounce);
                    litEmailUnsubscribe.Text = Convert.ToString(report.Email_NumUnsubscribe);

                    decimal num_sent = Convert.ToDecimal(report.Email_NumSent);
                    decimal num_read = Convert.ToDecimal(report.Email_NumRead);
                    decimal num_click = Convert.ToDecimal(report.Email_NumClick);
                    decimal read_rate = 0;
                    decimal adjusted_read_rate = 0;
                    if (num_sent > 0)
                    {
                        read_rate = Math.Round(Convert.ToDecimal(num_read / num_sent) * 100, 2);
                        adjusted_read_rate = Math.Round(Convert.ToDecimal((num_read * report.Email_NumReadMultiplier) / num_sent) * 100, 2);
                    }

                    decimal adjusted_num_read = Math.Round(Convert.ToDecimal(num_read * report.Email_NumReadMultiplier), 0);

                    litEmailReads.Text = Convert.ToString(adjusted_num_read);
                    litEmailClicks.Text = Convert.ToString(report.Email_NumClick);

                    decimal percent_click = 0;
                    decimal num_emails_sent = Convert.ToDecimal(report.Email_NumSent);
                    decimal num_email_clicks = Convert.ToDecimal(report.Email_NumClick);

                    if (num_emails_sent > 0)
                    {
                        percent_click = num_email_clicks / num_emails_sent;
                        percent_click = percent_click * 100;
                    }
                    litPercentRead.Text = "style=\"width:" + adjusted_read_rate + "%\"";
                    litPercentClicked.Text = "style=\"width:" + percent_click + "%\"";
                    litEmailReadRate.Text = Convert.ToString(adjusted_read_rate) + "%";
                    litEmailClickRate.Text = Convert.ToString(Math.Round(percent_click, 2)) + "%";

                    string start_available = string.Empty;
                    string end_available = string.Empty;
                    if (report.StartAvailable.HasValue)
                        start_available = report.StartAvailable.Value.ToString("d");
                    else
                        start_available = "not set";
                    if (report.EndAvailable.HasValue)
                        end_available = report.EndAvailable.Value.ToString("d");
                    else
                        end_available = "not set";

                    litAvailableRange.Text = "<strong>Start:</strong> " + start_available + "<br><strong>End:</strong> " + end_available;
                }
                else
                {
                    plhCampaignInfo.Visible = false;
                }
            }
            else
            {
                // load typical campaign information
                plhCampaignInfo.Visible = false;
            }
        }
    }
}