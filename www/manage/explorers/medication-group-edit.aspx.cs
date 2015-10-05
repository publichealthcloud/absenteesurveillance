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
using Quartz.Portal;
using Quartz.Social;

public partial class medication_group_edit : System.Web.UI.Page
{
    public int medication_group_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_ExplorersFolder"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["medicationGroupID"]))
            {
                medication_group_id = Convert.ToInt32(Request.QueryString["medicationGroupID"]);
                ViewState.Add("vsMedicationGroupID", medication_group_id);
                reWhatIdDoes.ImageManager.MaxUploadFileSize = 4194304;
                reDescription.ImageManager.MaxUploadFileSize = 4194304;
                reSideEffects.ImageManager.MaxUploadFileSize = 4194304;

                qSoc_MedicationGroup group = new qSoc_MedicationGroup(medication_group_id);

                lblTitle.Text = "Edit Medication Group - " + group.MedicationGroupName + " (ID: " + group.MedicationGroupID + ")";
                txtMedicationGroupName.Text = group.MedicationGroupName;
                reWhatIdDoes.Content = group.WhatItDoes;
                reDescription.Content = group.Description;
                reSideEffects.Content = group.SideEffectsDiscussion;
                rblAvailable.SelectedValue = group.Available;

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }

                // populate pull downs
                populateUnusedLinks();
                populateSideEffects();

                // display list of selected items
                populateLinks(medication_group_id);
                populateSideEffects(medication_group_id);
                populateMedications(medication_group_id);
            }

            else
            {
                lblTitle.Text = "New Medication Group";
                rblAvailable.SelectedValue = "Yes";
            }
        }

        if (String.IsNullOrEmpty(Convert.ToString(medication_group_id)))
            medication_group_id = (Int32)ViewState["vsMedicationGroupID"];

    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);

        if (!String.IsNullOrEmpty(Request.QueryString["medicationGroupID"]))
        {
            medication_group_id = Convert.ToInt32(Request.QueryString["medicationGroupID"]);

            qSoc_MedicationGroup group = new qSoc_MedicationGroup(medication_group_id);
            group.MedicationGroupName = txtMedicationGroupName.Text;
            group.WhatItDoes = reWhatIdDoes.Content;
            group.Description = reDescription.Content;
            group.SideEffectsDiscussion = reSideEffects.Content;
            group.Available = rblAvailable.SelectedValue;
            group.Update();
        }
        else
        {
            qSoc_MedicationGroup group = new qSoc_MedicationGroup();
            group.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            group.Created = DateTime.Now;
            group.CreatedBy = user_id;
            group.LastModified = DateTime.Now;
            group.LastModifiedBy = user_id;
            group.MarkAsDelete = 0;
            group.WhatItDoes = reWhatIdDoes.Content;
            group.Description = reDescription.Content;
            group.SideEffectsDiscussion = reSideEffects.Content;
            group.Available = rblAvailable.SelectedValue;
            group.Insert();

            medication_group_id = group.MedicationGroupID;
        }

        if (!String.IsNullOrEmpty(Request.QueryString["medicationGroupID"]))
        {
            Response.Redirect("medication-groups.aspx");
        }
        else
        {
            Response.Redirect(Request.Url.ToString() + "?mode=add-successful&medicationGroupID=" + medication_group_id);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("medication-groups.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        Response.Redirect("medication-groups.aspx");
    }

    protected void populateLinks(int medication_group_id)
    {
        bool available_links = false;
        
        if (medication_group_id > 0)
        {
            var links = qSoc_MedicationLink.GetMedicationLinks(medication_group_id, "group");

            if (links != null)
            {
                if (links.Count > 0)
                {
                    available_links = true;
                    int i = 1;
                    foreach (var l in links)
                    {
                        qPtl_Link link = new qPtl_Link(l.LinkID);
                        
                        Literal curr_link = new Literal();

                        string link_html = "<tr>";

                        link_html += "<td><font color=\"gray\">" + link.Title + "</font></td>";
                        link_html += "<td class=\"hidden-1024\"><a target=\"_blank\" href=\"" + link.URL + "\">" + link.URL + " <i class=\"glyphicon-new_window\"></i></a></td>";
                        link_html += "<td class=\"hidden-480\"><a href=\"#\" onclick=\"RemoveMedicationGroupLink('" + link.LinkID + "', '" + medication_group_id + "'); return false;\" class=\"btn\" rel=\"tooltip\" data-original-title=\"Remove Link\"><i class=\"icon-remove\"></i></a></td>";
                        link_html += "</tr>";

                        curr_link.Text = link_html;

                        pnlLinks.Controls.Add(curr_link);
                        i++;

                        // try and remove link from the unused list
                        string curr_title = link.Title;
                        ListItem removeItem = ddlLinks.Items.FindByText(curr_title);
                        ddlLinks.Items.Remove(removeItem);
                    }
                }
            }
        }

        // if no actions then add empty table
        if (available_links == false)
        {
            Literal no_action = new Literal();
            no_action.Text = "<tr><td>No links available</td><td></td><td></td></tr>";
            pnlLinks.Controls.Add(no_action);
        }
    }

    protected void populateSideEffects(int medication_group_id)
    {
        bool available_side_effects = false;

        if (medication_group_id > 0)
        {
            var effects = qSoc_MedicationGroupSideEffect_View.GetSideEffectsByMedicationGroup(medication_group_id);

            if (effects != null)
            {
                if (effects.Count > 0)
                {
                    available_side_effects = true;
                    int i = 1;
                    foreach (var e in effects)
                    {
                        string effect_html = "<tr>";

                        Literal curr_effect = new Literal();

                        if (e.Seriousness == "Serious")
                        {
                            effect_html += "<td><div style=\"background-color: red; padding: 3px; align: center;\"><font color=\"white\">" + e.Seriousness + "</font></div></td>";
                        }
                        else if (e.Seriousness == "Moderate")
                            effect_html += "<td><div style=\"background-color: yellow; padding: 3px; align: center;\"><font color=\"black\">" + e.Seriousness + "</font></div></td>";
                        else
                            effect_html += "<td><div style=\"background-color: white; padding: 3px; align: center;\"><font color=\"black\">" + e.Seriousness + "</font></div></td>";

                        effect_html += "<td class=\"hidden-1024\"><strong>" + e.Name + "</strong><br><div class=\"alert alert-success\">Comment: " + e.SeriousnessComment  + "</div></td>";
                        effect_html += "<td class=\"hidden-480\"><a href=\"#\" onclick=\"RemoveMedicationGroupSideEffect('" + e.SideEffectID + "', '" + medication_group_id + "'); return false;\" class=\"btn\" rel=\"tooltip\" data-original-title=\"Remove Link\"><i class=\"icon-remove\"></i></a></td>";
                        effect_html += "</tr>";

                        curr_effect.Text = effect_html;

                        pnlSideEffects.Controls.Add(curr_effect);
                        i++;

                        // try and remove link from the unused list
                        string curr_title = e.Name;
                        ListItem removeItem = ddlSideEffects.Items.FindByText(curr_title);
                        ddlSideEffects.Items.Remove(removeItem);
                    }
                }
            }
        }

        // if no actions then add empty table
        if (available_side_effects == false)
        {
            Literal no_action = new Literal();
            no_action.Text = "<tr><td>No side effects available</td><td></td><td></td></tr>";
            pnlSideEffects.Controls.Add(no_action);
        }
    }

    protected void populateMedications(int medication_group_id)
    {
        bool available_meds = false;

        if (medication_group_id > 0)
        {
            var meds = qSoc_Medication.GetMedicationsByGroup(medication_group_id);

            if (meds != null)
            {
                if (meds.Count > 0)
                {
                    available_meds = true;
                    int i = 1;
                    foreach (var m in meds)
                    {
                        string med_html = "<tr>";

                        Literal curr_med = new Literal();

                        med_html += "<td><strong>" + m.Name + "</strong></td>";
                        med_html += "<td class=\"hidden-1024\">" + m.Description + "</td>";
                        med_html += "<td class=\"hidden-1024\">" + m.SpecialInstructions + "</td>";
                        med_html += "<td class=\"hidden-480\"><a href=\"#\" onclick=\"RemoveMedication('" + m.MedicationID + "'); return false;\" class=\"btn\" rel=\"tooltip\" data-original-title=\"Remove Medication\"><i class=\"icon-remove\"></i></a></td>";
                        med_html += "</tr>";

                        curr_med.Text = med_html;

                        pnlMedications.Controls.Add(curr_med);
                        i++;
                    }
                }
            }
        }

        // if no actions then add empty table
        if (available_meds == false)
        {
            Literal no_action = new Literal();
            no_action.Text = "<tr><td>No medications available</td><td></td><td></td></tr>";
            pnlMedications.Controls.Add(no_action);
        }
    }

    protected void populateUnusedLinks()
    {
        ddlLinks.DataSource = qPtl_Link.GetLinks();
        ddlLinks.DataTextField = "Title";
        ddlLinks.DataValueField = "LinkID";
        ddlLinks.DataBind();
        ddlLinks.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateSideEffects()
    {
        ddlSideEffects.DataSource = qSoc_SideEffect.GetSideEffects();
        ddlSideEffects.DataTextField = "Name";
        ddlSideEffects.DataValueField = "SideEffectID";
        ddlSideEffects.DataBind();
        ddlSideEffects.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void btnManageLinks_Click(object sender, EventArgs e)
    {
        int link_id = 0;
        medication_group_id = Convert.ToInt32(Request.QueryString["medicationGroupID"]);

        if (!String.IsNullOrEmpty(Convert.ToString(ddlLinks.SelectedValue)))
        {
            link_id = Convert.ToInt32(ddlLinks.SelectedValue);

            qSoc_MedicationLink link = new qSoc_MedicationLink();
            link.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            link.Created = DateTime.Now;
            link.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
            link.LastModified = DateTime.Now;
            link.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
            link.Available = "Yes";
            link.MarkAsDelete = 0;
            link.LinkID = link_id;
            link.MedicationGroupID = medication_group_id;
            link.MedicationType = "group";
            link.OrderNum = 1;
            link.Insert();

            Response.Redirect("medication-group-edit.aspx?medicationGroupID=" + medication_group_id);
        }
        else
        {
            lblMedicationLinksMessage.Text = " *** WARNING *** You must first select a link from the pull down list.";
        }
    }

    protected void btnManageSideEffects_Click(object sender, EventArgs e)
    {
        int side_effect_id = 0;
        string severity = string.Empty;
        medication_group_id = Convert.ToInt32(Request.QueryString["medicationGroupID"]);

        if (!String.IsNullOrEmpty(Convert.ToString(ddlSideEffects.SelectedValue)) && !String.IsNullOrEmpty(Convert.ToString(ddlSeverity.SelectedValue)) && !String.IsNullOrEmpty(Convert.ToString(txtSeverityComment.Text)))
        {
            side_effect_id = Convert.ToInt32(ddlSideEffects.SelectedValue);
            severity = Convert.ToString(ddlSeverity.SelectedValue);

            qSoc_SideEffect effect = new qSoc_SideEffect(side_effect_id);

            qSoc_MedicationGroupSideEffect med_effect = new qSoc_MedicationGroupSideEffect();
            med_effect.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            med_effect.Created = DateTime.Now;
            med_effect.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
            med_effect.LastModified = DateTime.Now;
            med_effect.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
            med_effect.Available = "Yes";
            med_effect.MarkAsDelete = 0;
            med_effect.SideEffectID = side_effect_id;
            med_effect.MedicationGroupID = medication_group_id;
            med_effect.Seriousness = severity;
            med_effect.SeriousnessComment = txtSeverityComment.Text;
            med_effect.Insert();

            Response.Redirect("medication-group-edit.aspx?medicationGroupID=" + medication_group_id);
        }
        else
        {
            lblMedicationLinksMessage.Text = " *** WARNING *** You must first select a severity level and side effect.";
        }
    }

    protected void btnAddMedication_Click(object sender, EventArgs e)
    {
        medication_group_id = Convert.ToInt32(Request.QueryString["medicationGroupID"]);

        if (!String.IsNullOrEmpty(Convert.ToString(txtMedicationName.Text)))
        {
            string med_name = txtMedicationName.Text;
            string med_description = reMedicationDescription.Content;
            string med_special_instructions = reMedicationSpecialInstructions.Content;

            qSoc_Medication med = new qSoc_Medication();
            med.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            med.Created = DateTime.Now;
            med.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
            med.LastModified = DateTime.Now;
            med.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
            med.Available = "Yes";
            med.MarkAsDelete = 0;
            med.MedicationGroupID = medication_group_id;
            med.Name = med_name;
            med.Description = med_description;
            med.SpecialInstructions = med_special_instructions;
            med.Insert();

            Response.Redirect("medication-group-edit.aspx?medicationGroupID=" + medication_group_id);
        }
        else
        {
            lblMedicationLinksMessage.Text = " *** WARNING *** You must include a medication name.";
        }
    }
}

