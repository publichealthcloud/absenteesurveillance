<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignTopNav.ascx.cs" Inherits="manage_campaigns_controls_campaign_top_navigation" %>

    <ul class="tabs">
        <li>
            <div class="btn-group">
			    <a href="/manage/campaigns/campaign-details.aspx?campaignID=<%=CampaignID %>" class="btn"><i class="icon-dashboard"></i> Campaign Dashboard</a>
		    </div>
        </li>
        <li>
            <div class="btn-group">
			    <a href="/manage/campaigns/enrolled-members.aspx?campaignID=<%=CampaignID %>" class="btn"><i class="icon-group"></i> Enrolled Members</a>
		    </div>
        </li>
        <!--
        <li>
            <div class="btn-group">
                <a href="#" data-toggle="dropdown" class="btn dropdown-toggle">Elements <span class="caret"></span></a>
		        <ul class="dropdown-menu">
				    <li>
                        <a href="campaign-pages.aspx?campaignID=<%=CampaignID %>">Pages</a>
				    </li>
                    
				    <li>
                        <a href="campaign-emails.aspx?campaignID=<%=CampaignID %>">Emails</a>
				    </li>
				    <li>
                        <a href="campaign-messages.aspx?campaignID=<%=CampaignID %>">Messages</a>
				    </li>
				    <li>
                        <a href="camapign-assessments.aspx?campaignID=<%=CampaignID %>">Assessments</a>
				    </li>
				    <li>
                        <a href="campaign-feedback.aspx?campaignID=<%=CampaignID %>">Feedback</a>
				    </li>
		    </div>
        </li>
        <li>
            <div class="btn-group">
                <a href="#" data-toggle="dropdown" class="btn dropdown-toggle">Members <span class="caret"></span></a>
		        <ul class="dropdown-menu">
				    <li>
                        <a href="campaign-contacts.aspx?campaignID=<%=CampaignID %>">Contacted</a>
				    </li>
				    <li>
                        <a href="campaign-members.aspx?campaignID=<%=CampaignID %>">Enrolled</a>
				    </li>
		    </div>
        </li>
        
        <li>
            <div class="btn-group">
                <a href="#" data-toggle="dropdown" class="btn dropdown-toggle">Reports <span class="caret"></span></a>
		        <ul class="dropdown-menu">
				    <li>
                        <a href="/manage/campaigns/reports/campaign-reports.aspx?campaignID=<%=CampaignID %>">Available Reports</a>
				    </li>
				    <li>
                        <a href="/manage/campaigns/campaign-library.aspx?campaignID=<%=CampaignID %>">Library</a>
				    </li>
		    </div>
        </li>
        <li>
            <div class="btn-group">
			    <a href="#" class="btn dropdown-toggle"><i class="icon-question-sign"></i> Help</a>
		    </div>
        </li>
        -->
	</ul>