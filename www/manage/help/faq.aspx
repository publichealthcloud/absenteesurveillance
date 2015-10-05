<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage-simple.master" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="manage_help_faq" %>

<asp:Content ID="Content4" ContentPlaceHolderID="main_features" Runat="Server">
	<div class="row-fluid">
		<div class="span12">
			<div class="box">
				<div class="box-title">
					<h3>
						<i class="icon-reorder"></i>
						HELP
					</h3>
				</div>
				<div class="box-content nopadding">
					<div class="tabs-container">
						<ul class="tabs tabs-inline tabs-left">
							<li class='active'>
								<a href="#site" data-toggle='tab'><i class="icon-globe"></i> Site</a>
							</li>
							<li>
								<a href="#members" data-toggle='tab'><i class="icon-user"></i> Members</a>
							</li>
							<li>
								<a href="#learning" data-toggle='tab'><i class="glyphicon-lightbulb"></i> Learning</a>
							</li>
                            <!--
							<li>
								<a href="#communications" data-toggle='tab'><i class="icon-envelope-alt"></i> Communications</a>
							</li>
                            -->
						</ul>
					</div>
					<div class="tab-content padding tab-content-inline" style="min-height:100px;">
						<div class="tab-pane active" id="site">
							<div class="accordion" id="accordion_site">
								<div class="accordion-group">
									<div class="accordion-heading">
										<a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion_site" href="#site1">
											Uploading Files into the Site
										</a>
									</div>
									<div id="site1" class="accordion-body collapse in">
										<div class="accordion-inner">
											Navigate to <strong>Site > File Manager</strong> and select any of the folders that appear. After selecting your folder, click on the <strong>Upload</strong> button.<br /><br />
                                            The uploaded files will be available at <strong>http://{your site url}/resources/{folder you selected}/{file name you uploaded}</strong><br /><br />
                                            <pre><i class="icon-lightbulb"></i> <strong>TIP</strong> If you double-click on the file you uploaded, it will open in a new window. Most browsers will allow you to open this in a new window. You can then easily ses the URL.</pre>
										</div>
									</div>
								</div>
								<div class="accordion-group">
									<div class="accordion-heading">
										<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion_site" href="#site2">
											Adding Site Content
										</a>
									</div>
									<div id="site2" class="accordion-body collapse">
										<div class="accordion-inner">
											Navigate to <strong>Site > Content</strong> and select any of the following types of content to add. 
										</div>
									</div>
								</div>
								<div class="accordion-group">
									<div class="accordion-heading">
										<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion_site" href="#site3">
											Editing Site Text
										</a>
									</div>
									<div id="site3" class="accordion-body collapse">
										<div class="accordion-inner">
											Many of the text blocks on your site can be directly editing by navigating to <strong>Site > Pages > Page Zones.</strong><br /><br />
                                            This will display a list of site page zones (a part of a page) that you can edit. Find the page zone you need in the list based on the description and then click on the Manage icon.<br /><br />
                                            You will now see a page that allows you directly edit and save the HTML.
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="tab-pane" id="members">
							<div class="accordion" id="accordion_members">
								<div class="accordion-group">
									<div class="accordion-heading">
										<a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion4" href="#members1">
											Create New Members
										</a>
									</div>
									<div id="members1" class="accordion-body collapse in">
										<div class="accordion-inner">
                                            Navigate to <strong>Members > Member</strong> and click on the <strong>Add Member</strong> button at the top of the screen.
                                            <br /><br />
                                            Or follow this shortcut: <a href="/manage/members/add-member.aspx" class="btn btn-small btn-info" target="_blank"><i class="glyphicon-circle_plus"></i> ADD MEMBER</a>
										</div>
									</div>
								</div>
								<div class="accordion-group">
									<div class="accordion-heading">
										<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion4" href="#members2">
											Managing Members
										</a>
									</div>
									<div id="members2" class="accordion-body collapse">
										<div class="accordion-inner">
											Navigate to <strong>Members > Member</strong> and then find the members in the list. Once you find the member, click on the Manage button.
                                            <br /><br />
                                            <pre><i class="icon-lightbulb"></i> <strong>TIP</strong> You can quickly search the member list using the filter fields at the tops of the columns.</pre>
                                            Or follow this shortcut: <a href="/manage/members/member-list.aspx" class="btn btn-small btn-info" target="_blank">Manage Members</a>
										</div>
									</div>
								</div>
                                <div class="accordion-group">
									<div class="accordion-heading">
										<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion4" href="#members3">
											Resetting a Member's Password
										</a>
									</div>
									<div id="members3" class="accordion-body collapse">
										<div class="accordion-inner">
											Navigate to <strong>Members > Member</strong> and then find the members in the list. Once you find the member, click on the Manage button.
                                            <br /><br />
                                            You can then click on the Admin Tools button at the top of the Member Info section.
                                            <pre><i class="icon-lightbulb"></i> <strong>TIP</strong> You can also disable or delete accounts using these tools.</pre>
										</div>
									</div>
								</div>
							</div>
						</div>
						<div class="tab-pane" id="learning">
							<div class="accordion" id="accordion_learning">
								<div class="accordion-group">
									<div class="accordion-heading">
										<a class="accordion-toggle" data-toggle="collapse" data-parent="#learning" href="#learning1">
											How can I find a member learning report?
										</a>
									</div>
									<div id="learning1" class="accordion-body collapse in">
										<div class="accordion-inner">
											The member learning report contains all information about the member's learning progress - when the member registered, any trainings enrolled in, progress, completion, certificate and acknowledgement information.
                                            <br /><br />
                                            From any member page (see Managing Members in the Members section), click on the Learning button at the top. This will provide a collection of information about the member's learning.
										</div>
									</div>
								</div>
							</div>
						</div>
                        <!--
						<div class="tab-pane" id="communications">
							<div class="accordion" id="accordion_communications">
								<div class="accordion-group">
									<div class="accordion-heading">
										<a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion5" href="#collapseOne4">
											Lorem ipsum dolor sit amet, consectetur adipisicing?
										</a>
									</div>
									<div id="collapseComm1" class="accordion-body collapse in">
										<div class="accordion-inner">
											Anim pariatur cliche...Lorem ipsum dolore dolor occaecat dolore elit deserunt incididunt ex sed nostrud aute aliquip ut elit sed nisi. 
										</div>
									</div>
								</div>
							</div>
						</div>
                        -->
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>

