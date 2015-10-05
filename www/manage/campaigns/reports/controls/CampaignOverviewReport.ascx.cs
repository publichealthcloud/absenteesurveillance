using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Report;

public partial class manage_campaigns_reports_controls_CampaignOverviewReport : System.Web.UI.UserControl
{
    protected int campaign_id;
    protected bool report_mode;
    protected DateTime start_date, end_date;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }
    public bool ReportMode
    {
        get { return report_mode; }
        set { report_mode = value; }
    }
    public DateTime StartDate
    {
        get { return start_date; }
        set { start_date = value; }
    }
    public DateTime EndDate
    {
        get { return end_date; }
        set { end_date = value; }
    } 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (campaign_id == 0)
                campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);
            var report = qRpt_CampaignOverviewReport.GetCampaignOverviewByCampaignID(campaign_id);

            litCampaignName.Text = campaign.CampaignName;
            litDateReportGenerated.Text = "<br /><strong>Report Generated:</strong> " + Convert.ToString(DateTime.Now) + " based on data last compiled on " + report.LastTimeCompiled;

            if (Request.QueryString["mode"] == "print")
            {
                datStartDate.Visible = false;
                datEndDate.Visible = false;

                start_date = Convert.ToDateTime(Request.QueryString["startDate"]);
                end_date = Convert.ToDateTime(Request.QueryString["endDate"]);
                litStartDate.Text = start_date.ToString("d");
                litEndDate.Text = end_date.ToString("d");
                btnDownloadPDF.Visible = false;

                reformatTablesForPrinting();
                displayInlineHelp();
            }
            else
            {
                if (!String.IsNullOrEmpty(Convert.ToString(report.StartAvailable)))
                    datStartDate.SelectedDate = report.StartAvailable;
                else
                    datStartDate.SelectedDate = DateTime.Now.AddDays(-7);

                if (!String.IsNullOrEmpty(Convert.ToString(report.EndAvailable)))
                    datStartDate.SelectedDate = report.EndAvailable;
                else
                    datEndDate.SelectedDate = DateTime.Now;

                // short-term hack -- do not allow date evaluations
                datStartDate.Visible = false;
                datEndDate.Visible = false;

                start_date = Convert.ToDateTime(datStartDate.SelectedDate);
                end_date = Convert.ToDateTime(datEndDate.SelectedDate);
                litStartDate.Text = start_date.ToString("d");
                litEndDate.Text = end_date.ToString("d");
            }

            displayData(report, campaign);
        }
    }

    protected void btnDownloadPDF_Click(object sender, EventArgs e)
    {
        string baseURL = Request.Url.Host.ToLower() + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
        string rawURL = string.Empty;

        string curr_start_date = Convert.ToString(datStartDate.SelectedDate);
        string curr_end_date = Convert.ToString(datEndDate.SelectedDate);

        rawURL = baseURL + "/manage/campaigns/reports/campaign-overview-report_print.aspx?campaignID=" + Request.QueryString["campaignID"] + "&startDate=" + curr_start_date + "&endDate=" + curr_end_date + "&mode=print";

        string passURL = Server.UrlEncode(rawURL);
        string timeStamp = Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Year);
        Response.Redirect("~/reports/process/GeneratePDF.aspx?PageOrientation=landscape&htmlSource=" + passURL + "&mode=read&pdfOutput=campaign_" + Request.QueryString["campaignID"] + "_overivew_userID_" + Context.Items["UserID"] + "_" + timeStamp + ".pdf");
    }

    protected void displayData(qRpt_CampaignOverviewReport report, qSoc_Campaign campaign)
    {
        // summary
        litSummaryTotalEnrolled.Text = Convert.ToString(report.Enroll_NumWeb + report.Enroll_NumSMS + report.Enroll_NumApp);
        litSummaryTotalDays.Text = Convert.ToString(campaign.TotalDays) + " days";
        litSummaryCancelled.Text = Convert.ToString(report.Learn_NumCancelled);
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
        litSummaryAvailableDates.Text = start_available + " - " + end_available;
        litSummaryWaitingToStart.Text = Convert.ToString(report.Enroll_NumWaiting);
        litSummaryInProgress.Text = Convert.ToString(report.Enroll_NumInProgress);
        litSummaryFinished.Text = Convert.ToString(report.Enroll_NumFinished);
        
        // enrollment
        litEnrollmentContactsAvailable.Text = Convert.ToString(report.Invite_NumContactsAvailable);
        litEnrollmentContactsInvited.Text = Convert.ToString(report.Invite_NumUniqueInvites);
        decimal total_enrolled = (report.Enroll_NumWeb + report.Enroll_NumSMS + report.Enroll_NumApp);
        litEnrollmentNumInvitationsSent.Text = Convert.ToString(report.Invite_NumEmail + report.Invite_NumMail + report.Invite_NumSMS); ;
        litEnrollmentNumVisitsEnrollment.Text = Convert.ToString(report.Enroll_VisitedEnrollment);
        litEnrollmentWeb.Text = Convert.ToString(report.Enroll_NumWeb);
        litEnrollmentSMS.Text = Convert.ToString(report.Enroll_NumSMS);
        litEnrollmentMobileApp.Text = Convert.ToString(report.Enroll_NumApp);
        decimal conversion_rate_invited = 0;
        if (report.Invite_NumUniqueInvites > 0)
        {
            conversion_rate_invited = (total_enrolled / report.Invite_NumUniqueInvites );
            litEnrollmentConversionRateInvites.Text = Convert.ToString(Math.Round(conversion_rate_invited * 100, 2)) + "%";
        }
        else
            litEnrollmentConversionRateInvites.Text = "0";
        decimal conversion_rate_visits = 0;
        if (report.Enroll_VisitedEnrollment > 0)
        {
            conversion_rate_visits = (total_enrolled / report.Enroll_VisitedEnrollment);
            litEnrollmentConversionRateVisits.Text = Convert.ToString(Math.Round(conversion_rate_visits * 100, 2)) + "%";
        }
        else
            litEnrollmentConversionRateVisits.Text = "0";

        // description
        litCampaignDescription.Text = campaign.Description;

        // email
        litEmailNumSent.Text = Convert.ToString(report.Email_NumSent);
        litEmailUniqueContacts.Text = Convert.ToString(report.Invite_NumUniqueInvites);
        litEmailBounce.Text = Convert.ToString(report.Email_NumBounce);
        litEmailNumRead.Text = Convert.ToString(report.Email_NumRead);
        decimal num_sent = Convert.ToDecimal(report.Email_NumSent);
        decimal num_read = Convert.ToDecimal(report.Email_NumRead);
        decimal num_click = Convert.ToDecimal(report.Email_NumClick);
        decimal read_rate = Math.Round(Convert.ToDecimal(num_read / num_sent) * 100, 2);
        decimal adjusted_num_read = Math.Round(Convert.ToDecimal(num_read * report.Email_NumReadMultiplier), 0);
        decimal adjusted_read_rate = Math.Round(Convert.ToDecimal((num_read * report.Email_NumReadMultiplier) / num_sent) * 100, 2);
        litEmailReadRate.Text = Convert.ToString(read_rate) + "%";
        litEmailReadRateMultiplier.Text = Convert.ToString(report.Email_NumReadMultiplier);
        litEmailReadRateAdjusted.Text = Convert.ToString(adjusted_read_rate) + "%";
        litEmailsReadAdjusted.Text = Convert.ToString(adjusted_num_read);
        litEmailClicks.Text = Convert.ToString(report.Email_NumClick);
        litEmailClickRate.Text = Convert.ToString(Math.Round(Convert.ToDecimal(num_click / adjusted_num_read) * 100, 2)) + "%";
        litEmailUnsubscribes.Text = Convert.ToString(report.Email_NumUnsubscribe);
        litEmailSpam.Text = Convert.ToString(report.Email_NumSpam);

        // messaging
        litMessagingMessagesPossible.Text = Convert.ToString(report.Learn_NumMessagesPossible);
        litMessagingMessagesMandatory.Text = Convert.ToString(report.Learn_NumMessagesMandatory);
        litMessagingNumSent.Text = Convert.ToString(report.Learn_NumMessagesSent);
        litMessagingNumReceived.Text = Convert.ToString(report.Learn_NumMessagesReceived);
        decimal avg_message_per_user = Decimal.Add(report.Learn_NumMessagesSent, report.Learn_NumMessagesReceived) / total_enrolled;
        litMessagingAvgNumberPerMember.Text = Convert.ToString(Math.Round(avg_message_per_user, 0));

        // stop section
        litSTOPNum.Text = Convert.ToString(report.Learn_NumCancelled);
        litSTOPAvgDay.Text = "day " + Convert.ToString(report.Learn_CancelledAvgDay) + " out of " + campaign.TotalDays;
        litSTOPEarliestDay.Text = "day " + Convert.ToString(report.Learn_CancelledEarliestDay);
        litSTOPLatestDay.Text = "day " + Convert.ToString(report.Learn_CancelledLatestDay);


        // learning section
        litLearningNumInfo.Text = Convert.ToString(report.Learn_NumInfoQuestions);
        litLearningNumAttitude.Text = Convert.ToString(report.Learn_NumAttitudeQuestions);
        litLearningNumBehavior.Text = Convert.ToString(report.Learn_NumBehaviorQuestions);
        litLearningPreTestInfo.Text = Convert.ToString(report.Learn_PreAssInfo);
        litLearningPostTestInfo.Text = Convert.ToString(report.Learn_PostAssInfo);
        decimal avg_info = 0;
        if (report.Learn_NumInfoQuestions > 0)
        {
            avg_info = Decimal.Subtract(report.Learn_PostAssInfo, report.Learn_PreAssInfo);
            if (avg_info > 0)
                litLearningAvgInfo.Text = "<strong>+ " + Convert.ToString(avg_info) + " or " + Convert.ToString(Math.Round(avg_info*100, 2)) + "% <i class=\"icon-circle-arrow-up\"></i></strong>";
            else
                litLearningAvgInfo.Text = "- " + Convert.ToString(avg_info) + " or " + Convert.ToString(Math.Round(avg_info*100, 2)) + "% <i class=\"icon-circle-arrow-down\"></i>";
        }
        else
        {
            litLearningPreTestInfo.Text = "--";
            litLearningPostTestInfo.Text = "--";
            litLearningAvgInfo.Text = "--";
        }
        decimal avg_attitude = 0;
        if (report.Learn_NumAttitudeQuestions > 0)
        {
            avg_attitude = Decimal.Subtract(report.Learn_PostAssAttitude, report.Learn_PreAssAttitude);
            if (avg_attitude > 0)
                litLearningAvgAttitude.Text = "<strong>+ " + Convert.ToString(avg_attitude) + " or " + Convert.ToString(Math.Round(avg_attitude * 100, 2)) + "% <i class=\"icon-circle-arrow-up\"></i></strong>";
            else
                litLearningAvgAttitude.Text = "- " + Convert.ToString(avg_attitude) + " or " + Convert.ToString(Math.Round(avg_attitude * 100, 2)) + "% <i class=\"icon-circle-arrow-down\"></i>";
        }
        else
        {
            litLearningPreTestAttitude.Text = "--";
            litLearningPostTestAttitude.Text = "--";
            litLearningAvgAttitude.Text = "--";
        }
        decimal avg_behavior = 0;
        if (report.Learn_NumBehaviorQuestions > 0)
        {
            avg_behavior = Decimal.Subtract(report.Learn_PostAssBehavior, report.Learn_PreAssBehavior);
            if (avg_behavior > 0)
                litLearningAvgBehavior.Text = "<strong>+ " + Convert.ToString(avg_behavior) + " or " + Convert.ToString(Math.Round(avg_behavior * 100, 2)) + "% <i class=\"icon-circle-arrow-up\"></i></strong>";
            else
                litLearningAvgBehavior.Text = "- " + Convert.ToString(avg_behavior) + " or " + Convert.ToString(Math.Round(avg_behavior * 100, 2)) + "% <i class=\"icon-circle-arrow-down\"></i>";
        }
        else
        {
            litLearningPreTestBehavior.Text = "--";
            litLearningPostTestBehavior.Text = "--";
            litLearningAvgBehavior.Text = "--";
        }

    }

    protected void displayInlineHelp()
    {
        plhEmailAdjustedReadRateHelp.Visible = true;
        plhEmailInvitationsSentHelp.Visible = true;
        plhEnrollmentContactAvailableHelp.Visible = true;
        plhMessagingAvailableHelp.Visible = true;
        plhSTOPRequestsHelp.Visible = true;
        plhEmailAdjustedReadRateTip.Visible = false;
        plhEmailInvitationsSentTip.Visible = false;
        plhEnrollmentContactsAvailableTip.Visible = false;
        plhMessagingModeTip.Visible = false;
        plhSTOPRequestsTip.Visible = false;
    }

    protected void reformatTablesForPrinting()
    {
        litTableDescriptionSpacer.Text = "<br>&nbsp;<br>";
        litTableEnrollmentSpacer.Text = "<br>&nbsp;<br>";
        litTableEmailSpacer.Text = "<br>&nbsp;<br>";
        litTableMessagingSpacer.Text = "<br>&nbsp;<br>";
        litTableStopSpacer.Text = "<br>&nbsp;<br>";
        litTableLearningSpacer.Text = "<br>&nbsp;<br>";

        litTableSummaryWidth.Text = "width=\"850\"";
        litTableDescriptionWidth.Text = "width=\"850\"";
        litTableEmailWidth.Text = "width=\"850\"";
        litTableEnrollmentWidth.Text = "width=\"850\"";
        litTablelearningWidth.Text = "width=\"850\"";
        litTableMessagingWidth.Text = "width=\"850\"";
        litTableSTOPWidth.Text = "width=\"850\""; 
    }
}