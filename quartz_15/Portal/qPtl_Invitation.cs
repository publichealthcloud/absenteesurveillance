using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Quartz.Portal
{
    public class qPtl_Invitation
    {
        protected static qPtl_Invitation schema = new qPtl_Invitation();

        protected DbRow container;
        protected readonly DbColumn<Int32> invitationID;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime?> created;
        protected readonly DbColumn<Int32> createdBy;
        protected readonly DbColumn<DateTime?> lastModified;
        protected readonly DbColumn<Int32> lastModifiedBy;
        protected readonly DbColumn<Int32> markAsDelete;
        protected readonly DbColumn<DateTime?> startDate;
        protected readonly DbColumn<DateTime?> endDate;
        protected readonly DbColumn<Int32> userID;
        protected readonly DbColumn<String> inviteCode;
        protected readonly DbColumn<Int32> familyID;
        protected readonly DbColumn<String> initFirstName;
        protected readonly DbColumn<String> initLastName;
        protected readonly DbColumn<String> initMobile;
        protected readonly DbColumn<String> initEmail;
        protected readonly DbColumn<String> initPostal;
        protected readonly DbColumn<String> initUserName;
        protected readonly DbColumn<String> initYearBirth;
        protected readonly DbColumn<String> initMonthBirth;
        protected readonly DbColumn<String> initDayBirth;
        protected readonly DbColumn<String> initGender;
        protected readonly DbColumn<String> referenceValue;
        protected readonly DbColumn<String> invitationStatus;
        protected readonly DbColumn<String> invitationType;
        protected readonly DbColumn<Int32> roleID;
        protected readonly DbColumn<Int32> functionalRoleID;
        protected readonly DbColumn<String> invitationAudience;
        protected readonly DbColumn<String> invitationAudienceName;
        protected readonly DbColumn<Int32> maxRedemptions;
        protected readonly DbColumn<Int32> currRedemptions;
        protected readonly DbColumn<Int32> spaceID;
        protected readonly DbColumn<Int32> campaignID;
        protected readonly DbColumn<String> preassigned_mobile_number;

        public Int32 InvitationID { get { return invitationID.Value; } set { invitationID.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return createdBy.Value; } set { createdBy.Value = value; } }
        public DateTime? LastModified { get { return lastModified.Value; } set { lastModified.Value = value; } }
        public Int32 LastModifiedBy { get { return lastModifiedBy.Value; } set { lastModifiedBy.Value = value; } }
        public Int32 MarkAsDelete { get { return markAsDelete.Value; } set { markAsDelete.Value = value; } }
        public DateTime? StartDate { get { return startDate.Value; } set { startDate.Value = value; } }
        public DateTime? EndDate { get { return endDate.Value; } set { endDate.Value = value; } }
        public Int32 UserID { get { return userID.Value; } set { userID.Value = value; } }
        public String InviteCode { get { return inviteCode.Value; } set { inviteCode.Value = value; } }
        public Int32 FamilyID { get { return familyID.Value; } set { familyID.Value = value; } }
        public String InitFirstName { get { return initFirstName.Value; } set { initFirstName.Value = value; } }
        public String InitLastName { get { return initLastName.Value; } set { initLastName.Value = value; } }
        public String InitMobile { get { return initMobile.Value; } set { initMobile.Value = value; } }
        public String InitEmail { get { return initEmail.Value; } set { initEmail.Value = value; } }
        public String InitPostal { get { return initPostal.Value; } set { initPostal.Value = value; } }
        public String InitUserName { get { return initUserName.Value; } set { initUserName.Value = value; } }
        public String InitYearBirth { get { return initYearBirth.Value; } set { initYearBirth.Value = value; } }
        public String InitMonthBirth { get { return initMonthBirth.Value; } set { initMonthBirth.Value = value; } }
        public String InitDayBirth { get { return initDayBirth.Value; } set { initDayBirth.Value = value; } }
        public String InitGender { get { return initGender.Value; } set { initGender.Value = value; } }
        public String ReferenceValue { get { return referenceValue.Value; } set { referenceValue.Value = value; } }
        public String InvitationStatus { get { return invitationStatus.Value; } set { invitationStatus.Value = value; } }
        public String InvitationType { get { return invitationType.Value; } set { invitationType.Value = value; } }
        public Int32 RoleID { get { return roleID.Value; } set { roleID.Value = value; } }
        public Int32 FunctionalRoleID { get { return functionalRoleID.Value; } set { functionalRoleID.Value = value; } }
        public String InvitationAudience { get { return invitationAudience.Value; } set { invitationAudience.Value = value; } }
        public String InvitationAudienceName { get { return invitationAudienceName.Value; } set { invitationAudienceName.Value = value; } }
        public Int32 MaxRedemptions { get { return maxRedemptions.Value; } set { maxRedemptions.Value = value; } }
        public Int32 CurrRedemptions { get { return currRedemptions.Value; } set { currRedemptions.Value = value; } }
        public Int32 SpaceID { get { return spaceID.Value; } set { spaceID.Value = value; } }
        public Int32 CampaignID { get { return campaignID.Value; } set { campaignID.Value = value; } }
        public String PreassignedMobileNumber { get { return preassigned_mobile_number.Value; } set { preassigned_mobile_number.Value = value; } }

        public qPtl_Invitation()
		    : this (new DbRow ())
	    {
	    }

        public qPtl_Invitation (DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Invitations");
            invitationID = container.NewColumn<Int32>("InvitationID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime?>("Created");
            createdBy = container.NewColumn<Int32>("CreatedBy");
            lastModified = container.NewColumn<DateTime?>("LastModified");
            lastModifiedBy = container.NewColumn<Int32>("LastModifiedBy");
            markAsDelete = container.NewColumn<Int32>("MarkAsDelete");
            startDate = container.NewColumn<DateTime?>("StartDate");
            endDate = container.NewColumn<DateTime?>("EndDate");
            userID = container.NewColumn<Int32>("UserID");
            inviteCode = container.NewColumn<String>("InviteCode");
            familyID = container.NewColumn<Int32>("FamilyID");
            initFirstName = container.NewColumn<String>("InitFirstName");
            initLastName = container.NewColumn<String>("InitLastName");
            initMobile = container.NewColumn<String>("InitMobile");
            initEmail = container.NewColumn<String>("InitEmail");
            initPostal = container.NewColumn<String>("InitPostal");
            initUserName = container.NewColumn<String>("InitUserName");
            initYearBirth = container.NewColumn<String>("InitYearBirth");
            initMonthBirth = container.NewColumn<String>("InitMonthBirth");
            initDayBirth = container.NewColumn<String>("InitDayBirth");
            initGender = container.NewColumn<String>("InitGender");
            referenceValue = container.NewColumn<String>("ReferenceValue");
            invitationStatus = container.NewColumn<String>("InvitationStatus");
            invitationType = container.NewColumn<String>("InvitationType");
            roleID = container.NewColumn<Int32>("RoleID");
            functionalRoleID = container.NewColumn<Int32>("FunctionalRoleID");
            invitationAudience = container.NewColumn<String>("InvitationAudience");
            invitationAudienceName = container.NewColumn<String>("InvitationAudienceName");
            maxRedemptions = container.NewColumn<Int32>("MaxRedemptions");
            currRedemptions = container.NewColumn<Int32>("CurrRedemptions");
            spaceID = container.NewColumn<Int32>("SpaceID");
            campaignID = container.NewColumn<Int32>("CampaignID");
            preassigned_mobile_number = container.NewColumn<String>("PreassignedMobileNumber");
        }

        public qPtl_Invitation(int invitationID)
            : this()
        {
            container.Select("InvitationID = @InvitationID", new SqlQueryParameter("@InvitationID", invitationID));
        }

        public qPtl_Invitation(string inviteCode)
            : this()
        {
            container.Select("InviteCode = @InviteCode", new SqlQueryParameter("@InviteCode", inviteCode));
        }

        public qPtl_Invitation(int userID, string inviteCode)
            : this()
        {
            container.Select("UserID = @UserID AND InviteCode = @InviteCode", new SqlQueryParameter[] { new SqlQueryParameter("@UserID", userID), new SqlQueryParameter("@InviteCode", inviteCode) });
        }

        public int AddInvitation()
        {
            return Convert.ToInt32(container.Insert());
        }

        private static string GenerateInviteCode(int length)
        {
            // get new Guid and strip out dashes '-' then trim it to length
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, length);
        }

        public void UpdateInvitation()
        {
            container.Update("qPtl_Invitations", "InvitationID = @InvitationID");
        }

        public void Update()
        {
            container.Update("InvitationID = @InvitationID");
        }

        public void Insert()
        {
            InvitationID = Convert.ToInt32(container.Insert());
        }

        public static qPtl_Invitation[] GenerateInvites(int count, DateTime startDate, DateTime endDate, int createdBy, int familyID, int inviteLength, string invitationType, string invitationStatus, int roleID, int functionalRoleID)
        {
            List<qPtl_Invitation> invites = new List<qPtl_Invitation>();

            if (inviteLength > 32)
                inviteLength = 32;

            for (int i = 0; i < count; i++)
            {
                qPtl_Invitation invite = new qPtl_Invitation();

                string inviteCode = GenerateInviteCode(inviteLength);
                qPtl_Invitation existingInvitation = new qPtl_Invitation(inviteCode);
                
                for (int attemptCount = 0; existingInvitation.InvitationID != 0 && attemptCount < 100; attemptCount++)
                {
                    inviteCode = GenerateInviteCode(inviteLength);
                    existingInvitation = new qPtl_Invitation(GenerateInviteCode(inviteLength));
                }

                invite.InviteCode = inviteCode;

                invite.Created = DateTime.Now;
                invite.LastModified = invite.Created;
                invite.CreatedBy = createdBy;
                invite.StartDate = startDate;
                invite.EndDate = endDate;
                invite.UserID = 0;
                invite.FamilyID = familyID;
                invite.InvitationType = invitationType;
                invite.InvitationStatus = invitationStatus;
                invite.RoleID = roleID;
                invite.FunctionalRoleID = functionalRoleID;

                invite.InvitationID = invite.AddInvitation();

                invites.Add(invite);
            }

            return invites.ToArray();
        }

        public static qPtl_Invitation GenerateInvite(int userID, DateTime startDate, DateTime endDate, int createdBy, int familyID, int inviteLength, string invitationType, int roleID, int functionalRoleID)
        {
            qPtl_Invitation invite = new qPtl_Invitation();

            if (inviteLength > 32)
                inviteLength = 32;

            string inviteCode = GenerateInviteCode(inviteLength);
            qPtl_Invitation existingInvitation = new qPtl_Invitation(inviteCode);
            for (int attemptCount = 0; existingInvitation.InvitationID != 0 && attemptCount < 100; attemptCount++)
            {
                inviteCode = GenerateInviteCode(inviteLength);
                existingInvitation = new qPtl_Invitation(inviteCode);
            }

            invite.InviteCode = inviteCode;

            invite.Created = DateTime.Now;
            invite.LastModified = invite.Created;
            invite.CreatedBy = createdBy;
            invite.StartDate = startDate;
            invite.EndDate = endDate;
            invite.UserID = userID;
            invite.FamilyID = familyID;
            invite.InvitationType = invitationType;
            invite.RoleID = roleID;
            invite.FunctionalRoleID = functionalRoleID;

            invite.InvitationID = invite.AddInvitation();

            return invite;
        }

        static public bool InvitationValid(int userID, string inviteCode)
        {
            qPtl_Invitation invite = new qPtl_Invitation(userID, inviteCode);
            if (invite.invitationID.Value == -1 || (invite.StartDate >= DateTime.Now || invite.InvitationStatus == "Redeemed" || invite.EndDate <= DateTime.Now))
                return false;
            else
                return true;
        }

        static public bool InvitationValid(string inviteCode)
        {
            qPtl_Invitation invite = new qPtl_Invitation(inviteCode);
            if (invite.invitationID.Value == -1 || (invite.StartDate >= DateTime.Now || invite.InvitationStatus == "Redeemed" || invite.EndDate <= DateTime.Now))
                return false;
            else
            {
                if (invite.MaxRedemptions == -1)
                    return true;
                else if (invite.MaxRedemptions > 0 && invite.MaxRedemptions > invite.CurrRedemptions)
                    return true;
                else
                    return false;
            }
        }

        public static qPtl_Invitation GetInvitationByUserID(Int32 user_id)
        {
            var invitation = new qPtl_Invitation();

            invitation.container.Select(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                });

            return invitation.InvitationID > 0 ? invitation : null;
        }

        public static ICollection<qPtl_Invitation> GetInvitationsByFamilyID(int family_id)
        {
            return schema.container.Select<qPtl_Invitation>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND FamilyID = @FamilyID",
                    OrderBy = "RoleID DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@FamilyID", family_id) }
                },
                c => new qPtl_Invitation(c));
        }


        public static ICollection<qPtl_Invitation> GetInvitationsBySpaceID(int space_id)
        {
            return schema.container.Select<qPtl_Invitation>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND SpaceID = @SpaceID",
                    OrderBy = "RoleID DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@SpaceID", space_id) }
                },
                c => new qPtl_Invitation(c));
        }

        public void DeleteInvitation(int invitation_id)
        {
            container.Delete(string.Format(string.Format("InvitationID = {0}", invitation_id)));
        }

    }

    public class qPtl_Invitation_View
    {
        protected static qPtl_Invitation_View schema = new qPtl_Invitation_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> invitationID;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime?> created;
        protected readonly DbColumn<Int32> createdBy;
        protected readonly DbColumn<DateTime?> lastModified;
        protected readonly DbColumn<Int32> lastModifiedBy;
        protected readonly DbColumn<Int32> markAsDelete;
        protected readonly DbColumn<DateTime?> startDate;
        protected readonly DbColumn<DateTime?> endDate;
        protected readonly DbColumn<Int32> userID;
        protected readonly DbColumn<String> inviteCode;
        protected readonly DbColumn<Int32> familyID;
        protected readonly DbColumn<String> initFirstName;
        protected readonly DbColumn<String> initLastName;
        protected readonly DbColumn<String> initMobile;
        protected readonly DbColumn<String> initEmail;
        protected readonly DbColumn<String> initPostal;
        protected readonly DbColumn<String> initUserName;
        protected readonly DbColumn<String> initYearBirth;
        protected readonly DbColumn<String> initMonthBirth;
        protected readonly DbColumn<String> initDayBirth;
        protected readonly DbColumn<String> initGender;
        protected readonly DbColumn<String> referenceValue;
        protected readonly DbColumn<String> invitationStatus;
        protected readonly DbColumn<String> invitationType;
        protected readonly DbColumn<Int32> roleID;
        protected readonly DbColumn<Int32> functionalRoleID;
        protected readonly DbColumn<String> invitationAudience;
        protected readonly DbColumn<String> invitationAudienceName;
        protected readonly DbColumn<Int32> maxRedemptions;
        protected readonly DbColumn<Int32> currRedemptions;
        protected readonly DbColumn<Int32> spaceID;
        protected readonly DbColumn<Int32> campaignID;
        protected readonly DbColumn<String> familyName;
        protected readonly DbColumn<String> userName;        
        protected readonly DbColumn<String> roleName;
        protected readonly DbColumn<DateTime?> userCreated;

        public Int32 InvitationID { get { return invitationID.Value; } set { invitationID.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return createdBy.Value; } set { createdBy.Value = value; } }
        public DateTime? LastModified { get { return lastModified.Value; } set { lastModified.Value = value; } }
        public Int32 LastModifiedBy { get { return lastModifiedBy.Value; } set { lastModifiedBy.Value = value; } }
        public Int32 MarkAsDelete { get { return markAsDelete.Value; } set { markAsDelete.Value = value; } }
        public DateTime? StartDate { get { return startDate.Value; } set { startDate.Value = value; } }
        public DateTime? EndDate { get { return endDate.Value; } set { endDate.Value = value; } }
        public Int32 UserID { get { return userID.Value; } set { userID.Value = value; } }
        public String InviteCode { get { return inviteCode.Value; } set { inviteCode.Value = value; } }
        public Int32 FamilyID { get { return familyID.Value; } set { familyID.Value = value; } }
        public String InitFirstName { get { return initFirstName.Value; } set { initFirstName.Value = value; } }
        public String InitLastName { get { return initLastName.Value; } set { initLastName.Value = value; } }
        public String InitMobile { get { return initMobile.Value; } set { initMobile.Value = value; } }
        public String InitEmail { get { return initEmail.Value; } set { initEmail.Value = value; } }
        public String InitPostal { get { return initPostal.Value; } set { initPostal.Value = value; } }
        public String InitUserName { get { return initUserName.Value; } set { initUserName.Value = value; } }
        public String InitYearBirth { get { return initYearBirth.Value; } set { initYearBirth.Value = value; } }
        public String InitMonthBirth { get { return initMonthBirth.Value; } set { initMonthBirth.Value = value; } }
        public String InitDayBirth { get { return initDayBirth.Value; } set { initDayBirth.Value = value; } }
        public String InitGender { get { return initGender.Value; } set { initGender.Value = value; } }
        public String ReferenceValue { get { return referenceValue.Value; } set { referenceValue.Value = value; } }
        public String InvitationStatus { get { return invitationStatus.Value; } set { invitationStatus.Value = value; } }
        public String InvitationType { get { return invitationType.Value; } set { invitationType.Value = value; } }
        public Int32 RoleID { get { return roleID.Value; } set { roleID.Value = value; } }
        public Int32 FunctionalRoleID { get { return functionalRoleID.Value; } set { functionalRoleID.Value = value; } }
        public String InvitationAudience { get { return invitationAudience.Value; } set { invitationAudience.Value = value; } }
        public String InvitationAudienceName { get { return invitationAudienceName.Value; } set { invitationAudienceName.Value = value; } }
        public Int32 MaxRedemptions { get { return maxRedemptions.Value; } set { maxRedemptions.Value = value; } }
        public Int32 CurrRedemptions { get { return currRedemptions.Value; } set { currRedemptions.Value = value; } }
        public Int32 SpaceID { get { return spaceID.Value; } set { spaceID.Value = value; } }
        public Int32 CampaignID { get { return campaignID.Value; } set { campaignID.Value = value; } }
        public String FamilyName { get { return familyName.Value; } set { familyName.Value = value; } }
        public String UserName { get { return userName.Value; } set { userName.Value = value; } }
        public String RoleName { get { return roleName.Value; } set { roleName.Value = value; } }
        public DateTime? UserCreated { get { return userCreated.Value; } set { userCreated.Value = value; } }

        public qPtl_Invitation_View()
		    : this (new DbRow ())
	    {
	    }

        public qPtl_Invitation_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Invitations_View");
            invitationID = container.NewColumn<Int32>("InvitationID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime?>("Created");
            createdBy = container.NewColumn<Int32>("CreatedBy");
            lastModified = container.NewColumn<DateTime?>("LastModified");
            lastModifiedBy = container.NewColumn<Int32>("LastModifiedBy");
            markAsDelete = container.NewColumn<Int32>("MarkAsDelete");
            startDate = container.NewColumn<DateTime?>("StartDate");
            endDate = container.NewColumn<DateTime?>("EndDate");
            userID = container.NewColumn<Int32>("UserID");
            inviteCode = container.NewColumn<String>("InviteCode");
            familyID = container.NewColumn<Int32>("FamilyID");
            initFirstName = container.NewColumn<String>("InitFirstName");
            initLastName = container.NewColumn<String>("InitLastName");
            initMobile = container.NewColumn<String>("InitMobile");
            initEmail = container.NewColumn<String>("InitEmail");
            initPostal = container.NewColumn<String>("InitPostal");
            initUserName = container.NewColumn<String>("InitUserName");
            initYearBirth = container.NewColumn<String>("InitYearBirth");
            initMonthBirth = container.NewColumn<String>("InitMonthBirth");
            initDayBirth = container.NewColumn<String>("InitDayBirth");
            initGender = container.NewColumn<String>("InitGender");
            referenceValue = container.NewColumn<String>("ReferenceValue");
            invitationStatus = container.NewColumn<String>("InvitationStatus");
            invitationType = container.NewColumn<String>("InvitationType");
            roleID = container.NewColumn<Int32>("RoleID");
            functionalRoleID = container.NewColumn<Int32>("FunctionalRoleID");
            invitationAudience = container.NewColumn<String>("InvitationAudience");
            invitationAudienceName = container.NewColumn<String>("InvitationAudienceName");
            maxRedemptions = container.NewColumn<Int32>("MaxRedemptions");
            currRedemptions = container.NewColumn<Int32>("CurrRedemptions");
            spaceID = container.NewColumn<Int32>("SpaceID");
            campaignID = container.NewColumn<Int32>("CampaignID");
            familyName = container.NewColumn<String>("FamilyName");
            userName = container.NewColumn<String>("UserName");
            roleName = container.NewColumn<String>("RoleName");
            userCreated = container.NewColumn<DateTime?>("UserCreated");
        }

        public qPtl_Invitation_View(int invitationID)
            : this()
        {
            container.Select("InvitationID = @InvitationID", new SqlQueryParameter("@InvitationID", invitationID));
        }

        public static ICollection<qPtl_Invitation_View> GetInvitationsByFamilyID(int family_id)
        {
            return schema.container.Select<qPtl_Invitation_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND FamilyID = @FamilyID",
                    OrderBy = "RoleID DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@FamilyID", family_id) }
                },
                c => new qPtl_Invitation_View(c));
        }

        public static ICollection<qPtl_Invitation_View> GetInvitationsBySpaceID(int space_id)
        {
            return schema.container.Select<qPtl_Invitation_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND SpaceID = @SpaceID",
                    OrderBy = "RoleID DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@SpaceID", space_id) }
                },
                c => new qPtl_Invitation_View(c));
        }
    }
}
