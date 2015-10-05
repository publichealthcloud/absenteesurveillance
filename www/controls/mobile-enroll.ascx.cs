using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using Quartz.Portal;
using Quartz.Social;
using Quartz.Communication;

public partial class controls_mobile_enroll : System.Web.UI.UserControl
{
    protected int campaign_id;
    protected string return_url;
    protected bool mobile_verification_required = false;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }

    public string ReturnURL
    {
        get { return return_url; }
        set { return_url = value; }
    }

    public bool MobileVerificationRequired
    {
        get { return mobile_verification_required; }
        set { mobile_verification_required = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // see if user already has a number signed up and that he user preference is set to mobile ok
            int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);
            bool mobile_exists = false;
            bool mobile_active = false;
            lblCampaignID.Text = Convert.ToString(campaign_id);
            lblReturnURL.Text = return_url;
            qPtl_UserProfile profile = new qPtl_UserProfile(curr_user_id);

            string mobile_number = string.Empty;

            if (profile.Phone1Type == "Mobile")
                mobile_number = profile.Phone1;
            else if (profile.Phone2Type == "Mobile")
                mobile_number = profile.Phone2;

            plhCurrentlyVerified.Visible = false;
            plhNotYetVerified.Visible = true;
            if (!String.IsNullOrEmpty(mobile_number))
            {
                mobile_exists = true;
                litStatus.Text = "Use this form to change the number you want to use.<br><br>";
                btnEnroll.Text = "Update Number";
                plhCurrentlyVerified.Visible = true;
                plhNotYetVerified.Visible = false;
            }

            qCom_UserPreference pref = new qCom_UserPreference(curr_user_id);
            if (!String.IsNullOrEmpty(Convert.ToString(pref.MobilePINverified)) && pref.OkSms == "Yes")
                mobile_active = true;

            if (mobile_exists == true)
                txtMobileNumber.Text = mobile_number;

            if (mobile_active == true)
                btnEnroll.Text = "Turn Off Text Messages";

            if (mobile_active == false && mobile_verification_required == true)
            {
                // mobile is required and has not been completed

            }
        }
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
       // run check for mobile text
        if (!String.IsNullOrEmpty(lblCampaignID.Text))
        {
            campaign_id = Convert.ToInt32(lblCampaignID.Text);
        }
        return_url = lblReturnURL.Text;
        string mobile_number = txtMobileNumber.Text;
        bool error_occurred = false;
        bool phone_belongs_to_another_user = false;

        // check to see if this number is already being used by another user
        int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);
        int scope_id = Convert.ToInt32(Context.Items["ScopeID"]);

        var check_user = qPtl_User.GetUserByPhone(txtMobileNumber.Text, scope_id);

        if (check_user.UserID > 0)
        {
            if (check_user.UserID != curr_user_id)
            {
                error_occurred = true;
                phone_belongs_to_another_user = true;
            }
        }

        // replace characters
        if (mobile_number.Contains("-"))
            mobile_number = mobile_number.Replace("-", "");
       
        if (mobile_number.Contains("."))
            mobile_number = mobile_number.Replace(".", "");

        if (mobile_number.Contains("/"))
            mobile_number = mobile_number.Replace("/", "");

        if (mobile_number.Contains("("))
            mobile_number = mobile_number.Replace("(", "");

        if (mobile_number.Contains(")"))
            mobile_number = mobile_number.Replace(")", "");

        if (mobile_number.Contains("*"))
            mobile_number = mobile_number.Replace("*", "");

        if (mobile_number.Contains(" "))
            mobile_number = mobile_number.Replace(" ", "");

        try
        {
            string first_char = mobile_number.Substring(0, 1);
            if (mobile_number.Length == 11 && first_char == "1")
                mobile_number = mobile_number.Substring(1, 10);
        }
        catch
        {
            litMsg.Text = "<br><br>* Make sure to enter a 10 digit phone number";
            error_occurred = true;
        }

        if (String.IsNullOrEmpty(mobile_number))
        {
            litMsg.Text = "<br><br>* Make sure to enter a 10 digit phone number";
            error_occurred = true;
        }

        string pat_m = @"^[0-9]{10}$";
        Regex r_m = new Regex(pat_m, RegexOptions.IgnoreCase);
        Match m_m = r_m.Match(mobile_number);
        if (!m_m.Success)
        {
            error_occurred = true;
            litMsg.Text = "<br><br>* Make sure to enter a 10 digit phone number";
        }

        if (error_occurred == false)
        {

            if (btnEnroll.Text == "Turn Off Text Messages")
            {
                qCom_UserPreference pref = new qCom_UserPreference(curr_user_id);
                pref.OkSms = "No";
                pref.Update();
            }
            else
            {
                // save phone number to profile
                qPtl_UserProfile profile = new qPtl_UserProfile(curr_user_id);
                if (profile.Phone1Type == "Mobile")
                {
                    profile.Phone1 = string.Empty;
                    profile.Phone1Type = string.Empty;
                }
                if (profile.Phone2Type == "Mobile")
                {
                    profile.Phone2 = string.Empty;
                    profile.Phone2Type = string.Empty;
                }
                profile.Phone1 = txtMobileNumber.Text;
                profile.Phone1Type = "Mobile";

                profile.Update();

                // add new mobile verification code to qCom_UserPreferences
                var pref = qCom_UserPreference.GetUserPreference(curr_user_id);
                int new_pin = qCom_UserPreference.GenerateMobilePIN();

                if (pref != null)
                {
                    if (pref.UserID > 0)
                    {
                        pref.MobilePIN = Convert.ToString(new_pin);
                        pref.Update();
                    }
                }
                else
                {
                    qCom_UserPreference pref2 = new qCom_UserPreference();
                    pref2.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                    pref2.Available = "Yes";
                    pref2.Created = DateTime.Now;
                    pref2.CreatedBy = curr_user_id;
                    pref2.LastModified = DateTime.Now;
                    pref2.LastModifiedBy = curr_user_id;
                    pref2.UserID = curr_user_id;
                    pref2.OkBulkEmail = "Yes";
                    pref2.OkSms = "Yes";
                    pref2.OkEmail = "Yes";
                    pref2.MobilePIN = Convert.ToString(new_pin);
                    pref2.Insert();
                }

                // get correct DID
                string alt_did = string.Empty;
                // see if user has custom record
                var camp_pref = qCom_UserCampaignPreference.GetUserCampaignPreferences(campaign_id, curr_user_id);
                if (camp_pref != null)
                    alt_did = camp_pref.DID;
                else
                {
                    // see if campaign has available dedicated DIDs
                    qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);
                    qPtl_User user = new qPtl_User(curr_user_id);
                    alt_did = AddCampaignUserPreference(campaign, user, scope_id);

                    if (String.IsNullOrEmpty(alt_did))
                        alt_did = System.Configuration.ConfigurationManager.AppSettings["SMSDid"];
                }

                string alt_pin_message_uri = string.Empty;
                var c_pref = qCom_CampaignPreference.GetCampaignPreferences(campaign_id);
                if (c_pref != null)
                {
                    if (c_pref.CampaignPreferenceID > 0)
                    {
                        alt_pin_message_uri = c_pref.MobileVerifySMSURI;
                    }
                }

                // send mobile pin
                qCom_UserPreference.SendMobilePIN(Convert.ToString(new_pin), curr_user_id, alt_did, alt_pin_message_uri);

                plhManage.Visible = false;
                plhVerify.Visible = true;
            }
        }
        else
        {
            if (phone_belongs_to_another_user == true)
                litMsg.Text = "<br><br>* This phone number belongs to another user.";
        }
    }

    protected string AddCampaignUserPreference(qSoc_Campaign campaign, qPtl_User user, int scope_id)
    {
        string return_did = string.Empty;
        int campaign_did = qCom_CampaignDID.GetNextAvailableCampaignPhoneNumber(campaign.CampaignID);

        if (campaign_did > 0)
        {
            qCom_CampaignDID did = new qCom_CampaignDID(campaign_did);
            if (did != null)
            {
                if (did.CampaignDID > 0)
                {
                    qCom_UserCampaignPreference u_pref = new qCom_UserCampaignPreference();
                    u_pref.DID = did.DID;
                    u_pref.UserID = user.UserID;
                    u_pref.Available = "Yes";
                    u_pref.ScopeID = scope_id;
                    u_pref.Created = DateTime.Now;
                    u_pref.CreatedBy = user.UserID;
                    u_pref.LastModified = DateTime.Now;
                    u_pref.LastModifiedBy = user.UserID;
                    u_pref.MarkAsDelete = 0;
                    u_pref.CampaignID = campaign.CampaignID;
                    u_pref.Insert();

                    return_did = did.DID;
                }
            }
        }
        return return_did;
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);
        string pin = txtMobileVerify.Text;
        string return_url = string.Empty;
        if (!String.IsNullOrEmpty(lblReturnURL.Text))
            return_url = lblReturnURL.Text;
        
        qCom_UserPreference comm = qCom_UserPreference.GetUserPreference(curr_user_id);
        qPtl_User user = new qPtl_User(curr_user_id);

        string dbPIN = Convert.ToString(comm.MobilePIN);
                
        if (dbPIN == pin)
        {
            comm.OkSms = "Yes";
            comm.ConfirmSms = "Yes";
            comm.MobilePINverified = DateTime.Now;
            comm.Update();

            if (!String.IsNullOrEmpty(return_url))
                Response.Redirect(lblReturnURL.Text);
            else
                litMsg.Text = "* PIN successfully validated. You will start receiving messages shortly";
        }
        else
        {
            comm.OkSms = "No";
            comm.Update();

            litMsg.Text = "* WARNING: the PIN your entered does not match the one we sent to your phone. Please try again. If this problem continues, please contact support using the link below.<br><br>";
        }
    }
}