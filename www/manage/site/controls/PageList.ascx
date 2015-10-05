<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageList.ascx.cs" Inherits="manage_site_controls_PageList" %>


    <div class="row-fluid">                           
        <div class="span9">
            <div class="box">
	            <div class="box-title">
		        <h3>
			        <i class="icon-th-large"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Manage Pages"></asp:Label>
		        </h3>
	            </div>
	            <div class="box-content nopadding">
                    <h5>Click on page name to edit (page will open in another window)</h5>
                    <br />
                    <ul>
                      <asp:Repeater ID="repPages" runat="server">
                        <ItemTemplate>
                        <li><a href="<%# cms_url %>/public/launch-as-user.aspx?userID=<%# user_id %>&key=<%# key%>&redirectURL=<%# DataBinder.Eval(Container.DataItem, "URL") %>" target="_blank"><%# DataBinder.Eval(Container.DataItem, "URL") %></a></li>
                        </ItemTemplate>
                      </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>