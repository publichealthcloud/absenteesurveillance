using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;

using Telerik.Web.UI;

using Quartz;
using Quartz.Data;
using Quartz.GIS;
using Quartz.Health;
using Quartz.Social;

public partial class school_edit : System.Web.UI.Page
{
    public int health_provider_id;
    public string provider_name;
    public string latitude;
    public string longitude;
    public string address1;
    public string address2;
    public string city;
    public string state_province;
    public string postal_code;
    public string country;
    public string phone;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["healthProviderID"]))
            {
                health_provider_id = Convert.ToInt32(Request.QueryString["healthProviderID"]);

                qHtl_HealthProvider provider = new qHtl_HealthProvider(health_provider_id);

                lblTitle.Text = "Edit Health Provider (ID: " + provider.HealthProviderID + ")";
                txtName.Text = provider.Name;
                txtSummary.Text = provider.Description;
                ddlType.SelectedValue = provider.HealthProviderType;
                txtAddress1.Text = provider.Address1;
                txtAddress2.Text = provider.Address2;
                txtCity.Text = provider.City;
                ddlCountry.SelectedValue = provider.Country;
                ddlState.SelectedValue = provider.StateProvince;
                txtPostalCode.Text = provider.PostalCode;
                txtPhone.Text = provider.Phone;
                rblAvailable.SelectedValue = provider.Available;

                qGis_Object map = new qGis_Object();
                map = qGis_Object.GetGISObjectByContentTypeAndReference((int)qSoc_ContentType.Types.HealthProvider, health_provider_id);

                if (map != null)
                {
                    if (map.GISObjectID > 0)
                    {
                        plhMap.Visible = true;
                        provider_name = provider.Name;
                        address1 = provider.Address1;
                        address2 = provider.Address2;
                        city = provider.City;
                        state_province = provider.StateProvince;
                        postal_code = provider.PostalCode;
                        phone = provider.Phone;
                        longitude = Convert.ToString(map.Longitude);
                        latitude = Convert.ToString(map.Latitude);
                    }
                }
            }
            else
            {
                lblTitle.Text = "New Health Provider";
                plhTools.Visible = false;
                rblAvailable.SelectedValue = "Yes";
            }

            if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
            {
                lblMessage.Text = "*** Record Successfully Added ***";
                lblMessageBottom.Text = "*** Record Successfully Added ***";
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["healthProviderID"]))
            {
                health_provider_id = Convert.ToInt32(Request.QueryString["healthProviderID"]);

                qHtl_HealthProvider provider = new qHtl_HealthProvider(health_provider_id);

                provider.Name = txtName.Text;
                provider.Description = txtSummary.Text;
                provider.HealthProviderType = ddlType.SelectedValue;
                provider.Address1 = txtAddress1.Text;
                provider.Address2 = txtAddress2.Text;
                provider.City = txtCity.Text;
                provider.StateProvince = ddlState.SelectedValue;
                provider.PostalCode = txtPostalCode.Text;
                provider.Country = ddlCountry.SelectedValue;
                provider.Phone = txtPhone.Text;

                provider.Update();
            }
            else
            {
                qHtl_HealthProvider provider = new qHtl_HealthProvider();

                provider.ScopeID = 1;
                provider.Created = DateTime.Now;
                provider.CreatedBy = user_id;
                provider.LastModified = DateTime.Now;
                provider.LastModifiedBy = user_id;
                provider.Available = "Yes";
                provider.MarkAsDelete = 0;
                provider.Name = txtName.Text;
                provider.Description = txtSummary.Text;
                provider.HealthProviderType = ddlType.SelectedValue;
                provider.Available = rblAvailable.SelectedValue;
                provider.Address1 = txtAddress1.Text;
                provider.Address2 = txtAddress2.Text;
                provider.City = txtCity.Text;
                provider.StateProvince = ddlState.SelectedValue;
                provider.PostalCode = txtPostalCode.Text;
                provider.Country = ddlCountry.SelectedValue;
                provider.Phone = txtPhone.Text;

                provider.Insert();

                health_provider_id = provider.HealthProviderID;
            }

            bool geo_coding_success;
            try
            {
                Quartz.Data.qDbs_CustomWorkflows qFlow = new Quartz.Data.qDbs_CustomWorkflows();
                int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);
                string workflowSuccess = qFlow.RunWorkflow(107, 101, health_provider_id, curr_user_id);
                geo_coding_success = true;
            }
            catch
            {
                geo_coding_success = false;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["healthProviderID"]))
            {
                if (geo_coding_success == true)
                {
                    lblMessage.Text = "*** Record Successfully Updated ***";
                    lblMessageBottom.Text = "*** Record Successfully Updated ***";
                }
                else
                {
                    lblMessage.Text = "*** Record Successfully Updated BUT GeoCoding of the Address Failed ***";
                    lblMessageBottom.Text = "*** Record Successfully Updated BUT GeoCoding of the Address Failed ***";
                }
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&healthProviderID=" + health_provider_id);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        health_provider_id = Convert.ToInt32(Request.QueryString["healthProviderID"]);

        qHtl_HealthProvider provider = new qHtl_HealthProvider(health_provider_id);
        provider.Available = "No";
        provider.MarkAsDelete = 1;
        provider.Update();

        Response.Redirect("health-providers-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("health-providers-list.aspx");
    }
}

