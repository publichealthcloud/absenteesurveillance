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
using Quartz.Social;
using Quartz.Organization;

public partial class school_edit : System.Web.UI.Page
{
    public int school_id;
    public string school_name;
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
            populateSchoolDistricts();

            if (!String.IsNullOrEmpty(Request.QueryString["schoolID"]))
            {
                school_id = Convert.ToInt32(Request.QueryString["schoolID"]);

                qOrg_School school = new qOrg_School(school_id);

                lblTitle.Text = "Edit School (ID: " + school.SchoolID + ")";
                txtName.Text = school.School;
                ddlType.SelectedValue = school.SchoolType;
                txtAddress1.Text = school.Address1;
                txtAddress2.Text = school.Address2;
                txtCity.Text = school.City;
                ddlState.SelectedValue = school.StateProvince;
                txtPostalCode.Text = school.PostalCode;
                txtPhone.Text = school.SchoolPhone;
                rblAvailable.SelectedValue = school.Available;
                ddlSchoolDistrict.SelectedValue = Convert.ToString(school.SchoolDistrictID);

                qGis_Object map = new qGis_Object();
                map = qGis_Object.GetGISObjectByContentTypeAndReference((int)qSoc_ContentType.Types.School, school_id);

                if (map != null)
                {
                    if (map.GISObjectID > 0)
                    {
                        plhMap.Visible = true;
                        school_name = school.School;
                        address1 = school.Address1;
                        address2 = school.Address2;
                        city = school.City;
                        state_province = school.StateProvince;
                        postal_code = school.PostalCode;
                        phone = school.SchoolPhone;
                        longitude = Convert.ToString(map.Longitude);
                        latitude = Convert.ToString(map.Latitude);
                    }
                }
            }
            else
            {
                lblTitle.Text = "New School";
                plhTools.Visible = false;
                rblAvailable.SelectedValue = "Yes";
            }

            if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
            {
                lblMessage.Text = "*** Record Successfully Added ***";
                lblMessageBottom.Text = "*** Record Successfully Added ***";
            }
            else if (Convert.ToString(Request.QueryString["mode"]) == "update-successful")
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["schoolID"]))
            {
                school_id = Convert.ToInt32(Request.QueryString["schoolID"]);

                qOrg_School school = new qOrg_School(school_id);

                school.School = txtName.Text;
                school.SchoolType = ddlType.SelectedValue;
                school.Address1 = txtAddress1.Text;
                school.Address2 = txtAddress2.Text;
                school.City = txtCity.Text;
                school.StateProvince = ddlState.SelectedValue;
                school.PostalCode = txtPostalCode.Text;
                school.Country = ddlCountry.SelectedValue;
                school.SchoolPhone = txtPhone.Text;
                if (!String.IsNullOrEmpty(ddlSchoolDistrict.SelectedValue))
                    school.SchoolDistrictID = Convert.ToInt32(ddlSchoolDistrict.SelectedValue);

                school.Update();
            }
            else
            {
                qOrg_School school = new qOrg_School();

                school.ScopeID = 1;
                school.Created = DateTime.Now;
                school.CreatedBy = user_id;
                school.LastModified = DateTime.Now;
                school.LastModifiedBy = user_id;
                school.Available = "Yes";
                school.MarkAsDelete = 0;
                school.School = txtName.Text;
                school.SchoolType = ddlType.SelectedValue;
                school.Available = rblAvailable.SelectedValue;
                school.Address1 = txtAddress1.Text;
                school.Address2 = txtAddress2.Text;
                school.City = txtCity.Text;
                school.StateProvince = ddlState.SelectedValue;
                school.Country = ddlCountry.SelectedValue;
                school.PostalCode = txtPostalCode.Text;
                school.SchoolPhone = txtPhone.Text;
                if (!String.IsNullOrEmpty(ddlSchoolDistrict.SelectedValue))
                    school.SchoolDistrictID = Convert.ToInt32(ddlSchoolDistrict.SelectedValue);

                school.Insert();

                school_id = school.SchoolID;
            }

            bool geo_coding_success = true;

            /*
            bool geo_coding_success;
            try
            {
                Quartz.Data.qDbs_CustomWorkflows qFlow = new Quartz.Data.qDbs_CustomWorkflows();
                int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);
                string workflowSuccess = qFlow.RunWorkflow(108, 102, school_id, curr_user_id);
                geo_coding_success = true;
            }
            catch
            {
                geo_coding_success = false;
            }
             */

            if (!String.IsNullOrEmpty(Request.QueryString["schoolID"]))
            {
                if (geo_coding_success == true)
                {
                    lblMessage.Text = "*** Record Successfully Updated ***";
                    lblMessageBottom.Text = "*** Record Successfully Updated ***";
                    Response.Redirect(Request.Url.ToString() + "&mode=update-successful");
                }
                else
                {
                    //lblMessage.Text = "*** Record Successfully Updated BUT GeoCoding of the Address Failed ***";
                    //lblMessageBottom.Text = "*** Record Successfully Updated BUT GeoCoding of the Address Failed ***";
                    lblMessage.Text = "*** Record Successfully Updated ***";
                    lblMessageBottom.Text = "*** Record Successfully Updated ***";
                    Response.Redirect(Request.Url.ToString() + "?mode=add-successful&schoolID=" + school_id);
                }
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&schoolID=" + school_id);
            }
        }
    }

    protected void populateSchoolDistricts()
    {
        ddlSchoolDistrict.DataSource = qOrg_SchoolDistrict.GetSchoolDistricts();
        ddlSchoolDistrict.DataTextField = "DistrictName";
        ddlSchoolDistrict.DataValueField = "SchoolDistrictID";
        ddlSchoolDistrict.DataBind();
        ddlSchoolDistrict.Items.Insert(0, new ListItem("", string.Empty));
        int num_school_districts = ddlSchoolDistrict.Items.Count;
        ddlSchoolDistrict.Items.Insert(num_school_districts, new ListItem("Other", "-1"));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        school_id = Convert.ToInt32(Request.QueryString["schoolID"]);

        qOrg_School school = new qOrg_School(school_id);
        school.Available = "No";
        school.MarkAsDelete = 1;
        school.Update();

        Response.Redirect("schools-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("schools-list.aspx");
    }
}

