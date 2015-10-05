<%@ Page Title="" Language="C#" MasterPageFile="~/manage/communications/messaging/test-message.master" AutoEventWireup="true" CodeFile="calendar.aspx.cs" Inherits="text_messages_calendar" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Message Calendar</h1>
    <div style="background-color:lightgray; padding:4px 4px 4px 4px; text-align:left;">
        <asp:Button ID="btnNewRecord" runat="server" Text="New Event"></asp:Button>
    </div>
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="scheduler">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="scheduler" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

    <telerik:RadScheduler Skin="Metro" runat="server" ID="scheduler"
                            Height="100%" Width="100%" ShowFooter="false"
                            DayStartTime="08:00:00" DayEndTime="21:00:00"                        
                            FirstDayOfWeek="Monday" LastDayOfWeek="Friday"
                            EnableDescriptionField="true" 
                            RowHeight="27px"
                            AppointmentStyleMode="Default" SelectedView="MonthView">
        <AdvancedForm Modal="false" />
        <TimelineView UserSelectable="false" />

        <AppointmentTemplate>
            <div>
                <a href="<%# Eval ("Attributes['Mode']") %>#" onclick="openRecord('14', '18', '<%# Eval("ID") %>'); return false;"><%# Eval("Subject") %></a>
            </div>
        </AppointmentTemplate>

        <ResourceStyles>
            <telerik:ResourceStyleMapping Type="Calendar" Text="Event" BackColor="#D0ECBB" BorderColor="#B0CC9B" />
            <telerik:ResourceStyleMapping Type="Calendar" Text="Training" ApplyCssClass="rsCategoryRed" />
            <telerik:ResourceStyleMapping Type="Calendar" Text="Meeting" BackColor="yellow" ApplyCssClass="rsCategoryOrange" />
        </ResourceStyles>

        <TimeSlotContextMenuSettings EnableDefault="true" />
        <AppointmentContextMenuSettings EnableDefault="true" /> 

    </telerik:RadScheduler>
</asp:Content>

