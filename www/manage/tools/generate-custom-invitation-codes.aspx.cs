using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Portal;
using Quartz.Social;

public partial class manage_tools_generate_custom_invitation_codes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }

    protected void btnGenerateCodes_Click(object sender, EventArgs e)
    {
        // load parameters
        string code_mode = string.Empty;
        int num_codes = 0;
        num_codes = Convert.ToInt32(txtNumCodes.Text);
        int space_id = 0;
        if (!String.IsNullOrEmpty(txtSpaceID.Text))
            space_id = Convert.ToInt32(txtSpaceID.Text);
        int campaign_id = 0;
        if (!String.IsNullOrEmpty(txtCampaignID.Text))
            campaign_id = Convert.ToInt32(txtCampaignID.Text);
        DateTime start_time = new DateTime();
        DateTime end_time = new DateTime();
        start_time = DateTime.Now;
        end_time = DateTime.Now.AddYears(10);
        int invite_length = 4;

        if (ddlCodeType.SelectedValue == "4char_guid")
            invite_length = 4;
        else if (ddlCodeType.SelectedValue == "5char_guid")
            invite_length = 5;

        string invitation_type = string.Empty;
        int member_role_id = 0;
        member_role_id = 11;
        int functional_role_id = 0;
        string mobile_number = string.Empty;

        // TO DO -- parse the text field into the appropriate number of phone numbers into an array
        string raw_numbers = txtPhoneNumbers.Text;
        string [] numbers = null;
        if (!String.IsNullOrEmpty(raw_numbers))
            numbers = raw_numbers.Split(',');

        int j = 0;
        int k = numbers.Length;  // should be the number of phone numbers in the array

        for (int i = 1; i <= num_codes+1; i++)
        {
            if (j == k)
                j = 0;
            
            var invite_t = qPtl_Invitation.GenerateInvite(0, start_time, end_time, Convert.ToInt32(Context.Items["UserID"]), 0, invite_length, invitation_type, member_role_id, functional_role_id);
            invite_t.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            invite_t.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
            invite_t.LastModified = DateTime.Now;
            invite_t.MarkAsDelete = 0;
            invite_t.Available = "Yes";
            invite_t.InvitationStatus = "Redeemable";
            invite_t.CurrRedemptions = 0;
            invite_t.MaxRedemptions = 1;
            invite_t.SpaceID = space_id;
            invite_t.CampaignID = campaign_id;

            if (!String.IsNullOrEmpty(numbers[j]))
                mobile_number = numbers[j];

            invite_t.PreassignedMobileNumber = mobile_number;
            invite_t.Update();

            j++;
        }
    }
}