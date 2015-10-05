<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="health-provider-edit.aspx.cs" Inherits="school_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="padding-left:10px;">
    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Health Provider</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="health-providers-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Health Providers</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="health-provider-list-edit.aspx?healthProviderID=<%= health_provider_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE HEALTH PROVIDER" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>
    <div style="height:10px;"></div>
    <table border="0" cellpadding="5">
        <asp:PlaceHolder ID="plhTools" runat="server">
        <tr>
            <td colspan="2" class="NormalBold" bgcolor="#EEE">
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this school? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Health Provider"></asp:LinkButton>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="NormalBold">
            <br /><br /><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label>
            <asp:PlaceHolder ID="plhValidation" runat="server">
                <blockquote>
                <asp:ValidationSummary runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="form" />
                </blockquote>
            </asp:PlaceHolder>
            </td>
        </tr>
   <tr>
        <td width="150" class="NormalBold" valign="top">
        <strong>Name *</strong>
        </td>
        <td>
            <telerik:RadTextBox ID="txtName" runat="server" Width="500px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtName"
                ErrorMessage="Name required">
            </asp:RequiredFieldValidator>       
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Description
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtSummary" TextMode="MultiLine" runat="server" Width="500px" Height="80px"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td width="100" class="NormalBold" valign="top">
        <strong>Provider Type *</strong>
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlType" runat="server">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="Clinic" Text="Clinic"></asp:ListItem>
                <asp:ListItem Value="Hospital" Text="Hospital"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvType" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlType"
                ErrorMessage="Provider type is required">
            </asp:RequiredFieldValidator> 
        </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Address 1 *</strong>
            </td>
            <td class="Normal">
                <telerik:RadTextBox ID="txtAddress1" runat="server" Width="200px"></telerik:RadTextBox>
                <asp:RequiredFieldValidator 
                    runat="server" 
                    ID="rfvAddress" 
                    ValidationGroup="form"
                    ControlToValidate="txtAddress1" 
                    Text="*" 
                    ErrorMessage="Address required" />
            </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">
            Address 2
            </td>
            <td class="Normal">
                <telerik:RadTextBox ID="txtAddress2" runat="server" Width="200px"></telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">
            <strong>City *</strong>
            </td>
            <td class="Normal">
                <telerik:RadTextBox ID="txtCity" runat="server" Width="200px"></telerik:RadTextBox>
                <asp:RequiredFieldValidator 
                    runat="server" 
                    ID="rfvCity" 
                    ValidationGroup="form"
                    ControlToValidate="txtCity" 
                    Text="*" 
                    ErrorMessage="City required" />
            </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">
            <strong>State/Province *</strong>
            </td>
            <td class="Normal">
                <asp:DropDownList ID="ddlState" runat="server">
                    <asp:ListItem />
                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DC">District of Columbia</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="FL">Florida</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico</asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">North Dakota</asp:ListItem>
                    <asp:ListItem Value="OH">Ohio</asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia</asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                    <asp:ListItem Value="AB">Alberta</asp:ListItem>
                    <asp:ListItem Value="BC">British Columbia</asp:ListItem>
                    <asp:ListItem Value="MB">Manitoba</asp:ListItem>
                    <asp:ListItem Value="NB">New Brunswick</asp:ListItem>
                    <asp:ListItem Value="NL">Newfoundland and Labrador</asp:ListItem>
                    <asp:ListItem Value="NT">Northwest Territories</asp:ListItem>
                    <asp:ListItem Value="NS">Nova Scotia</asp:ListItem>
                    <asp:ListItem Value="NU">Nunavit</asp:ListItem>
                    <asp:ListItem Value="ON">Ontario</asp:ListItem>
                    <asp:ListItem Value="PE">Prince Edward Island</asp:ListItem>
                    <asp:ListItem Value="QC">Quebec</asp:ListItem>
                    <asp:ListItem Value="SK">Saskatchewan</asp:ListItem>
                    <asp:ListItem Value="YT">Yukon</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator 
                    runat="server" 
                    ID="rfvStateProvince" 
                    ValidationGroup="form"
                    ControlToValidate="ddlState" 
                    Text="*" 
                    ErrorMessage="State/Province required" />
            </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Postal Code *</strong>
            </td>
            <td class="Normal">
                <telerik:RadTextBox ID="txtPostalCode" Width="100" runat="server" Mask="######" ResetCaretOnFocus="True"
                    SelectionOnFocus="SelectAll" MaxLength="10" />
                <asp:RequiredFieldValidator 
                    runat="server" 
                    ID="rfvPostalCode" 
                    ValidationGroup="form"
                    ControlToValidate="txtPostalCode" 
                    Text="*" 
                    ErrorMessage="Postal code required" />
            </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">
            Country
            </td>
            <td class="Normal">
                <asp:DropDownList ID="ddlCountry" runat="server">
                    <asp:ListItem Value="US" Text="US"></asp:ListItem>
                    <asp:ListItem Value="CA" Text="Canada"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">
            Phone
            </td>
            <td class="Normal">
                <telerik:RadMaskedTextBox ID="txtPhone" Width="100" runat="server" Mask="###-###-####"
                    ResetCaretOnFocus="True" RoundNumericRanges="False" EmptyMessage="required" SelectionOnFocus="SelectAll" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plhMap" runat="server" Visible="false">
        <tr>
            <td width="200" class="Normal" valign="top">
                <div id="buttonDisplay">
                    <input id="btnMap" type="button" onclick="GetMap()" value="Show Map" />
                </div>
            </td>
            <td>
                <script type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.3"></script>
    
                <script type="text/javascript">
                    var map = null;
                    var shape = null;
                    var ID = '<%= health_provider_id %>';
                    var Lat = '<%= latitude %>';
                    var Long = '<%= longitude %>';
                    var Name = '<%= provider_name %>';
                    var Address1 = '<%= address1 %>';
                    var Address2 = '<%= address2 %>';
                    var City = '<%= city %>';
                    var State = '<%= state_province %>';
                    var Zip = '<%= postal_code %>';
                    var Phone = '<%= phone %>';

                    function GetMap() {
                        e = document.getElementById("myMap");
                        e.style.height = 400 + 'px';
                        m = document.getElementById("buttonDisplay");
                        m.style.visibility = 'hidden';

                        map = new VEMap('myMap');
                        map.LoadMap();
                        map.SetCenterAndZoom(new VELatLong(Lat, Long), 9);

                        var curr_point = new VELatLong(Lat, Long)
                        var curr_shape = new VEShape(VEShapeType.Pushpin, curr_point);
                        curr_shape.SetTitle(Name);
                        curr_shape.SetDescription(Address1 + ',' + Address2 + '<br>' + City + ', ' + State + ' ' + Zip + '<br>' + Phone);
                        map.AddShape(curr_shape);
                    }

                </script>
                <div id='myMap' style="position:relative; width:550px; height:5px;"></div>
            </td>
        </tr>
        </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Available *
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="lblApprovedBy" CssClass="NormalItalics" runat="server"></asp:Label>
            <asp:RequiredFieldValidator 
                ID="rfvAvailable" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rblAvailable"
                ErrorMessage="Available required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
</table>
<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="health-providers-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Health Providers</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="edit-health-provider.aspx?healthproviderID=<%= health_provider_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE HEALTH PROVIDER" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


