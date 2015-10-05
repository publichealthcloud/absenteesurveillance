<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignOverviewReport.ascx.cs" Inherits="manage_campaigns_reports_controls_CampaignOverviewReport" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="row-fluid">                  
    <div class="span12">
        <h3>Campaign Overview</h3>
        <h2><asp:Literal ID="litCampaignName" runat="server"></asp:Literal></h2>

        <strong>Report Start Date:</strong> <telerik:RadDatePicker ID="datStartDate" runat="server"></telerik:RadDatePicker><asp:Literal ID="litStartDate" runat="server"></asp:Literal>
        <br />
        <strong>Report End Date:</strong> <telerik:RadDatePicker ID="datEndDate" runat="server"></telerik:RadDatePicker><asp:Literal ID="litEndDate" runat="server"></asp:Literal>
        <br />
        <asp:Literal ID="litDateReportGenerated" runat="server"></asp:Literal><p>&nbsp;</p>
        <asp:LinkButton CssClass="btn" ID="btnDownloadPDF" runat="server" OnClick="btnDownloadPDF_Click"><i class="glyphicon-download_alt"></i> Download PDF</asp:LinkButton>
        <div style="height:10px;"></div>
    </div>
</div>
<div class="row-fluid">                  
    <div class="span6">
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-dashboard"></i>
                        Summary
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTableSummaryWidth" runat="server"/>>
		        <tbody>
			        <tr>
				        <td>Dates Available</td>
				        <td><asp:Literal ID="litSummaryAvailableDates" runat="server">9/29/2013 - open</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Enrolled Members </td>
				        <td><asp:Literal ID="litSummaryTotalEnrolled" runat="server">439</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Members Waiting to Start</td>
				        <td><asp:Literal ID="litSummaryWaitingToStart" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Members In Progress</td>
				        <td><asp:Literal ID="litSummaryInProgress" runat="server">16</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Members Finished</td>
				        <td><asp:Literal ID="litSummaryFinished" runat="server">423</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Mid-Campaign STOP Requests</td>
				        <td><asp:Literal ID="litSummaryCancelled" runat="server">32</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Total Campaign Length</td>
				        <td><asp:Literal ID="litSummaryTotalDays" runat="server">70 days</asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>
    </div>
    <div class="span6">
        <asp:Literal ID="litTableDescriptionSpacer" runat="server"></asp:Literal>
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-info-sign"></i>
                        Description
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <table class="table table-hover table-nomargin" <asp:Literal id="litTableDescriptionWidth" runat="server"/>>
		            <tbody>
			            <tr>
				            <td><asp:Literal ID="litCampaignDescription" runat="server">description here.</asp:Literal></td>
			            </tr>
                    </tbody>
                </table>
        </div>
        </div>
</div>
<div class="row-fluid">                  
    <div class="span6">
        <asp:Literal ID="litTableEnrollmentSpacer" runat="server"></asp:Literal>
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-globe"></i>
                        Enrollment
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTableEnrollmentWidth" runat="server"/>>
		        <tbody>
			        <tr>
				        <td>Number of Contacts Available During the Campaign
                            <asp:PlaceHolder ID="plhEnrollmentContactsAvailableTip" runat="server">
                                &nbsp;<a href="#" rel="popover" data-trigger="hover" title="" 
                                    data-content="This is the number of contacts in the database which were available during the campaign -- these contacts may have had email and/or text messages sent to them inviting them to participate." 
                                    data-original-title="What is this?"><i class="icon-question-sign"></i></a>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plhEnrollmentContactAvailableHelp" Visible="false" runat="server"><br /><blockquote><i class="icon-question-sign"></i> This is the number of contacts in the database which were available during the campaign -- these contacts may have had email and/or text messages sent to them inviting them to participate.</blockquote></asp:PlaceHolder>
				        </td>
				        <td><asp:Literal ID="litEnrollmentContactsAvailable" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Numver of Contacts Invited to Participate in the Campaign</td>
				        <td><asp:Literal ID="litEnrollmentContactsInvited" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Number of Invitations Sent to Contacts</td>
				        <td><asp:Literal ID="litEnrollmentNumInvitationsSent" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Number of Flyers Distributed</td>
				        <td><asp:Literal ID="litEnrollmentNumFlyers" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Visits to the Enrollment Page</td>
				        <td><asp:Literal ID="litEnrollmentNumVisitsEnrollment" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Conversion Rate Based on Total Invited</td>
				        <td><asp:Literal ID="litEnrollmentConversionRateInvites" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Conversion Rate Based on Total Who Visited Enrollment Page</td>
				        <td><asp:Literal ID="litEnrollmentConversionRateVisits" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Enrolled via Web</td>
				        <td><asp:Literal ID="litEnrollmentWeb" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Enrolled via Mobile App</td>
				        <td><asp:Literal ID="litEnrollmentMobileApp" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Enrolled via Text Messaging (SMS)</td>
				        <td><asp:Literal ID="litEnrollmentSMS" runat="server">0</asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>
    </div>
    <div class="span6">
        <asp:Literal ID="litTableEmailSpacer" runat="server"></asp:Literal>
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-envelope-alt"></i>
                        Email
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTableEmailWidth" runat="server"/>>
		        <tbody>
			        <tr>
				        <td>Invitation Emails Sent
                            <asp:PlaceHolder ID="plhEmailInvitationsSentTip" runat="server">
                                &nbsp;<a href="#" rel="popover" data-trigger="hover" title="" 
                                    data-content="This is the number of contacts in the database which were available during the campaign -- these contacts may have had email and/or text messages sent to them inviting them to participate." 
                                    data-original-title="What is this?"><i class="icon-question-sign"></i></a>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plhEmailInvitationsSentHelp" Visible="false" runat="server"><br /><blockquote><i class="icon-question-sign"></i> This is the number of contacts in the database which were available during the campaign -- these contacts may have had email and/or text messages sent to them inviting them to participate.</blockquote></asp:PlaceHolder>
				        </td>
				        <td><asp:Literal ID="litEmailNumSent" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Number of Unique Contacts</td>
				        <td><asp:Literal ID="litEmailUniqueContacts" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Emails Read (Raw)</td>
				        <td><asp:Literal ID="litEmailNumRead" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Email Read Rate (Raw)</td>
				        <td><asp:Literal ID="litEmailReadRate" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Adjusted Read Rate Multiplier
                            <asp:PlaceHolder ID="plhEmailAdjustedReadRateTip" runat="server">
                                &nbsp;<a href="#" rel="popover" data-trigger="hover" title="" 
                                    data-content="The read rate multiplier is used to provide a more accurate understanding of the actual number of emails read. The read rate multiplier is a value greater than 1 which is muliplied to the number of emails read to come up with a more accurate read rate. Background - Only emails where the contact chooses to 'View Images' or where the contact's email client automatically displays images can be logged as read emails. Therefore, because a contact can read an email without viewing the images, the number of emails read is underreported.  When campaigns have a large enough sample size, an adjusted read rate is calculated by evaluating the number of emails clicked which were NOT marked as being read. Since the only way a contact can click on a link in the email is to read the email, we know that every click must equal a read. So for every 100 clicks, there might only be 60 clicks. This tells us that the read rate is being underreported by 40%. The adjusted read is the actual read rate PLUS this additional 40% which therefore provides a more accurate sense of what is happening with the campaign." 
                                    data-original-title="What is this?"><i class="icon-question-sign"></i></a>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plhEmailAdjustedReadRateHelp" Visible="false" runat="server"><br /><blockquote><i class="icon-question-sign"></i> The read rate multiplier is used to provide a more accurate understanding of the actual number of emails read. The read rate multiplier is a value greater than 1 which is muliplied to the number of emails read to come up with a more accurate read rate. Background - Only emails where the contact chooses to 'View Images' or where the contact's email client automatically displays images can be logged as read emails. Therefore, because a contact can read an email without viewing the images, the number of emails read is underreported.  When campaigns have a large enough sample size, an adjusted read rate is calculated by evaluating the number of emails clicked which were NOT marked as being read. Since the only way a contact can click on a link in the email is to read the email, we know that every click must equal a read. So for every 100 clicks, there might only be 60 clicks. This tells us that the read rate is being underreported by 40%. The adjusted read is the actual read rate PLUS this additional 40% which therefore provides a more accurate sense of what is happening with the campaign.</blockquote></asp:PlaceHolder>
				        </td>
				        <td><asp:Literal ID="litEmailReadRateMultiplier" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Emails Read (Adjusted)</td>
				        <td><asp:Literal ID="litEmailsReadAdjusted" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Email Read Rate (Adjusted)</td>
				        <td><asp:Literal ID="litEmailReadRateAdjusted" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Enrollment Links Clicked in Emails</td>
				        <td><asp:Literal ID="litEmailClicks" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Click Rate (a percentage of all emails read)</td>
				        <td><asp:Literal ID="litEmailClickRate" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Unsubscribes</td>
				        <td><asp:Literal ID="litEmailUnsubscribes" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Spam Reports</td>
				        <td><asp:Literal ID="litEmailSpam" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Bouncebacks</td>
				        <td><asp:Literal ID="litEmailBounce" runat="server">0</asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>
    </div>
</div>
<div class="row-fluid">                  
    <div class="span6">
        <asp:Literal ID="litTableMessagingSpacer" runat="server"></asp:Literal>
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-comments"></i>
                        Messaging
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTableMessagingWidth" runat="server"/>>
		        <tbody>
			        <tr>
				        <td>Message Mode
                            <asp:PlaceHolder ID="plhMessagingModeTip" runat="server">
                                &nbsp;<a href="#" rel="popover" data-trigger="hover" title="" 
                                    data-content="Multiple mode describes the way the learning messages were delivered. Modes include: sms, mobile app, web and email. Multiple modes can also be supported in a single campaign." 
                                    data-original-title="What is this?"><i class="icon-question-sign"></i></a>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="plhMessagingAvailableHelp" Visible="false" runat="server"><br /><blockquote><i class="icon-question-sign"></i> Multiple mode describes the way the learning messages were delivered. Modes include: sms, mobile app, web and email. Multiple modes can also be supported in a single campaign.</blockquote></asp:PlaceHolder>
				        </td>
				        <td><asp:Literal ID="litMessagingMode" runat="server">text messaging (SMS)</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Possible Messages</td>
				        <td><asp:Literal ID="litMessagingMessagesPossible" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Mandatory Messages</td>
				        <td><asp:Literal ID="litMessagingMessagesMandatory" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Messages Sent</td>
				        <td><asp:Literal ID="litMessagingNumSent" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Messages Received</td>
				        <td><asp:Literal ID="litMessagingNumReceived" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Average Messages Per Member</td>
				        <td><asp:Literal ID="litMessagingAvgNumberPerMember" runat="server">0</asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>
    </div>
    <div class="span6">
        <asp:Literal ID="litTableStopSpacer" runat="server"></asp:Literal>
        <div class="box">
			<div class="box-title">
				<h3>
                    <i class="icon-remove-sign"></i>
                    STOP Requests
				</h3>
			</div>
        </div>
	    <div class="box-content">
                <table class="table table-hover table-nomargin" <asp:Literal id="litTableSTOPWidth" runat="server"/>>
		            <tbody>
			            <tr>
				            <td>Stop Requests Received
                                <asp:PlaceHolder ID="plhSTOPRequestsTip" runat="server">
                                    &nbsp;<a href="#" rel="popover" data-trigger="hover" title="" 
                                        data-content="This is the number of campaign participants who cancelled their participation in the program. For text messaging programs, this happens when the member texts the word STOP." 
                                        data-original-title="What is this?"><i class="icon-question-sign"></i></a>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhSTOPRequestsHelp" Visible="false" runat="server"><br /><blockquote><i class="icon-question-sign"></i> This is the number of campaign participants who cancelled their participation in the program. For text messaging programs, this happens when the member texts the word STOP.</blockquote></asp:PlaceHolder>
				            </td>
				            <td><asp:Literal ID="litSTOPNum" runat="server">39</asp:Literal></td>
			            </tr>
			            <tr>
				            <td>Avg Day in Campaign STOP Requests Received</td>
				            <td><asp:Literal ID="litSTOPAvgDay" runat="server">day 52 out of 70</asp:Literal></td>
			            </tr>
			            <tr>
				            <td>Earliest STOP Request</td>
				            <td><asp:Literal ID="litSTOPEarliestDay" runat="server">day 6</asp:Literal></td>
			            </tr>
			            <tr>
				            <td>Latest STOP Request</td>
				            <td><asp:Literal ID="litSTOPLatestDay" runat="server">day 66</asp:Literal></td>
			            </tr>
		            </tbody>
	            </table>
			</div>
    </div>
</div>
<div class="row-fluid"> 
    <div class="span12">
        <asp:Literal ID="litTableLearningSpacer" runat="server"></asp:Literal>
            <div class="box">
				<div class="box-title">
				    <h3>
                        <i class="icon-fire"></i>
                        Learning
				    </h3>
				</div>
            </div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTablelearningWidth" runat="server"/>>
                <thead>
				    <tr>
					    <th align="left">Question Type</th>
                        <th align="left">Number of Questions</th>
					    <th align="left">Pre-Test Score</th>
    					<th align="left">Post-Test Score</th>
    					<th align="left">Change</th>
				    </tr>
			    </thead>
		        <tbody>
			        <tr>
				        <td>Knowledge</td>
                        <td><asp:Literal ID="litLearningNumInfo" runat="server">4</asp:Literal></td>
				        <td><asp:Literal ID="litLearningPreTestInfo" runat="server">0</asp:Literal></td>
				        <td><asp:Literal ID="litLearningPostTestInfo" runat="server"></asp:Literal></td>
				        <td><asp:Literal ID="litLearningAvgInfo" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Attitude</td>
                        <td><asp:Literal ID="litLearningNumAttitude" runat="server">0</asp:Literal></td>
				        <td><asp:Literal ID="litLearningPreTestAttitude" runat="server"></asp:Literal></td>
				        <td><asp:Literal ID="litLearningPostTestAttitude" runat="server"></asp:Literal></td>
				        <td><asp:Literal ID="litLearningAvgAttitude" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Intention for Behavior Change</td>
                        <td><asp:Literal ID="litLearningNumBehavior" runat="server">0</asp:Literal></td>
				        <td><asp:Literal ID="litLearningPreTestBehavior" runat="server"></asp:Literal></td>
				        <td><asp:Literal ID="litLearningPostTestBehavior" runat="server"></asp:Literal></td>
				        <td><asp:Literal ID="litLearningAvgBehavior" runat="server"></asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
				</div>
    </div>
</div>