using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Quartz.Portal;

namespace Quartz.Organization
{
    public class qOrg_GroupRequests
    {
        protected static qOrg_GroupRequests schema = new qOrg_GroupRequests();

        protected DbRow container;
        protected readonly DbColumn<Int32> group_request_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> type;
        protected readonly DbColumn<String> status;
        protected readonly DbColumn<DateTime?> when_approved;
        protected readonly DbColumn<String> approved_by;
        protected readonly DbColumn<String> advisor_first_name;
        protected readonly DbColumn<String> advisor_last_name;
        protected readonly DbColumn<String> advisor_position;
        protected readonly DbColumn<String> advisor_position_other;
        protected readonly DbColumn<String> advisor_email;
        protected readonly DbColumn<String> advisor_phone;
        protected readonly DbColumn<String> group_full_name;
        protected readonly DbColumn<String> group_short_name;
        protected readonly DbColumn<Int32> space_category_id;
        protected readonly DbColumn<String> category_other;
        protected readonly DbColumn<String> group_description_other;
        protected readonly DbColumn<String> why_join;
        protected readonly DbColumn<String> num_members;
        protected readonly DbColumn<String> when_founded;
        protected readonly DbColumn<String> school_type;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<String> school_district_other;
        protected readonly DbColumn<Int32> school_id;
        protected readonly DbColumn<String> other_school;
        protected readonly DbColumn<String> other_school_address1;
        protected readonly DbColumn<String> other_school_address2;
        protected readonly DbColumn<String> other_school_city;
        protected readonly DbColumn<String> other_school_state_province;
        protected readonly DbColumn<String> other_school_postal_code;
        protected readonly DbColumn<String> other_school_phone;
        protected readonly DbColumn<String> principal_first_name;
        protected readonly DbColumn<String> principal_last_name;
        protected readonly DbColumn<String> principal_role;
        protected readonly DbColumn<String> principal_email;
        protected readonly DbColumn<String> principal_phone;
        protected readonly DbColumn<String> advisor_notes;
        protected readonly DbColumn<String> group_notes;
        protected readonly DbColumn<String> school_notes;
        protected readonly DbColumn<String> principal_notes;
        protected readonly DbColumn<String> principal_initials;
        protected readonly DbColumn<DateTime?> principal_when_approved;
        protected readonly DbColumn<Int32> health_provider_id;
        protected readonly DbColumn<String> health_provider_name;
        protected readonly DbColumn<String> health_provider_info;
        protected readonly DbColumn<String> health_provider_notes;
        protected readonly DbColumn<Int32> space_id;
        protected readonly DbColumn<Int32> advisor_invite_id;

        public Int32 GroupRequestID { get { return group_request_id.Value; } set { group_request_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String Type { get { return type.Value; } set { type.Value = value; } }
        public String Status { get { return status.Value; } set { status.Value = value; } }
        public DateTime? WhenApproved { get { return when_approved.Value; } set { when_approved.Value = value; } }
        public String ApprovedBy { get { return approved_by.Value; } set { approved_by.Value = value; } }
        public String AdvisorFirstName { get { return advisor_first_name.Value; } set { advisor_first_name.Value = value; } }
        public String AdvisorLastName { get { return advisor_last_name.Value; } set { advisor_last_name.Value = value; } }
        public String AdvisorPosition { get { return advisor_position.Value; } set { advisor_position.Value = value; } }
        public String AdvisorPositionOther { get { return advisor_position_other.Value; } set { advisor_position_other.Value = value; } }
        public String AdvisorEmail { get { return advisor_email.Value; } set { advisor_email.Value = value; } }
        public String AdvisorPhone { get { return advisor_phone.Value; } set { advisor_phone.Value = value; } }
        public String GroupFullName { get { return group_full_name.Value; } set { group_full_name.Value = value; } }
        public String GroupShortName { get { return group_short_name.Value; } set { group_short_name.Value = value; } }
        public Int32 SpaceCategoryID { get { return space_category_id.Value; } set { space_category_id.Value = value; } }
        public String CategoryOther { get { return category_other.Value; } set { category_other.Value = value; } }
        public String GroupDescriptionOther { get { return group_description_other.Value; } set { group_description_other.Value = value; } }
        public String WhyJoin { get { return why_join.Value; } set { why_join.Value = value; } }
        public String NumNumbers { get { return num_members.Value; } set { num_members.Value = value; } }
        public String WhenFounded { get { return when_founded.Value; } set { when_founded.Value = value; } }
        public String SchoolType { get { return school_type.Value; } set { school_type.Value = value; } }
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public String SchoolDistrictOther { get { return school_district_other.Value; } set { school_district_other.Value = value; } }
        public Int32 SchoolID { get { return school_id.Value; } set { school_id.Value = value; } }
        public String OtherSchool { get { return other_school.Value; } set { other_school.Value = value; } }
        public String OtherSchoolAddress1 { get { return other_school_address1.Value; } set { other_school_address1.Value = value; } }
        public String OtherSchoolAddress2 { get { return other_school_address2.Value; } set { other_school_address2.Value = value; } }
        public String OtherSchoolCity { get { return other_school_city.Value; } set { other_school_city.Value = value; } }
        public String OtherSchoolStateProvince { get { return other_school_state_province.Value; } set { other_school_state_province.Value = value; } }
        public String OtherSchoolPostalCode { get { return other_school_postal_code.Value; } set { other_school_postal_code.Value = value; } }
        public String OtherSchoolPhone { get { return other_school_phone.Value; } set { other_school_phone.Value = value; } }
        public String PrincipalFirstName { get { return principal_first_name.Value; } set { principal_first_name.Value = value; } }
        public String PrincipalLastName { get { return principal_last_name.Value; } set { principal_last_name.Value = value; } }
        public String PrincipalRole { get { return principal_role.Value; } set { principal_role.Value = value; } }
        public String PrincipalEmail { get { return principal_email.Value; } set { principal_email.Value = value; } }
        public String PrincipalPhone { get { return principal_phone.Value; } set { principal_phone.Value = value; } }
        public String AdvisorNotes { get { return advisor_notes.Value; } set { advisor_notes.Value = value; } }
        public String GroupNotes { get { return group_notes.Value; } set { group_notes.Value = value; } }
        public String SchoolNotes { get { return school_notes.Value; } set { school_notes.Value = value; } }
        public String PrincipalNotes { get { return principal_notes.Value; } set { principal_notes.Value = value; } }
        public String PrincipalInitials { get { return principal_initials.Value; } set { principal_initials.Value = value; } }
        public DateTime? PrincipalWhenApproved { get { return principal_when_approved.Value; } set { principal_when_approved.Value = value; } }
        public Int32 HealthProviderID { get { return health_provider_id.Value; } set { health_provider_id.Value = value; } }
        public String HealthProviderName { get { return health_provider_name.Value; } set { health_provider_name.Value = value; } }
        public String HealthProviderInfo { get { return health_provider_info.Value; } set { health_provider_info.Value = value; } }
        public String HealthProviderNotes { get { return health_provider_notes.Value; } set { health_provider_notes.Value = value; } }
        public Int32 SpaceID { get { return space_id.Value; } set { space_id.Value = value; } }
        public Int32 AdvisorInviteID { get { return advisor_invite_id.Value; } set { advisor_invite_id.Value = value; } }

        public qOrg_GroupRequests()
            : this(new DbRow())
        {
        }

        protected qOrg_GroupRequests(DbRow c)
        {
            container = c;
            container.SetContainerName("qOrg_GroupRequests");
            group_request_id = container.NewColumn<Int32>("GroupRequestID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            type = container.NewColumn<String>("Type");
            status = container.NewColumn<String>("Status");
            when_approved = container.NewColumn<DateTime?>("WhenApproved");
            approved_by = container.NewColumn<String>("ApprovedBy");
            advisor_first_name = container.NewColumn<String>("AdvisorFirstName");
            advisor_last_name = container.NewColumn<String>("AdvisorLastName");
            advisor_position = container.NewColumn<String>("AdvisorPosition");
            advisor_position_other = container.NewColumn<String>("AdvisorPositionOther");
            advisor_email = container.NewColumn<String>("AdvisorEmail");
            advisor_phone = container.NewColumn<String>("AdvisorPhone");
            group_full_name = container.NewColumn<String>("GroupFullName");
            group_short_name = container.NewColumn<String>("GroupShortName");
            space_category_id = container.NewColumn<Int32>("SpaceCategoryID");
            category_other = container.NewColumn<String>("CategoryOther");
            group_description_other = container.NewColumn<String>("GroupDescriptionOther");
            why_join = container.NewColumn<String>("WhyJoin");
            num_members = container.NewColumn<String>("NumMembers");
            when_founded = container.NewColumn<String>("WhenFounded");
            school_type = container.NewColumn<String>("SchoolType");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            school_district_other = container.NewColumn<String>("SchoolDistrictOther");
            school_id = container.NewColumn<Int32>("SchoolID");
            other_school = container.NewColumn<String>("OtherSchool");
            other_school_address1 = container.NewColumn<String>("OtherSchoolAddress1");
            other_school_address2 = container.NewColumn<String>("OtherSchoolAddress2");
            other_school_city = container.NewColumn<String>("OtherSchoolCity");
            other_school_postal_code = container.NewColumn<String>("OtherSchoolPostalCode");
            other_school_state_province = container.NewColumn<String>("OtherSchoolStateProvince");
            other_school_phone = container.NewColumn<String>("OtherSchoolPhone");
            principal_first_name = container.NewColumn<String>("PrincipalFirstName");
            principal_last_name = container.NewColumn<String>("PrincipalLastName");
            principal_email = container.NewColumn<String>("PrincipalEmail");
            principal_phone = container.NewColumn<String>("PrincipalPhone");
            principal_role = container.NewColumn<String>("PrincipalRole");
            advisor_notes = container.NewColumn<String>("AdvisorNotes");
            group_notes = container.NewColumn<String>("GroupNotes");
            school_notes = container.NewColumn<String>("SchoolNotes");
            principal_notes = container.NewColumn<String>("PrincipalNotes");
            principal_initials = container.NewColumn<String>("PrincipalInitials");
            principal_when_approved = container.NewColumn<DateTime?>("PrincipalWhenApproved");
            health_provider_id = container.NewColumn<Int32>("HealthProviderID");
            health_provider_name = container.NewColumn<String>("HealthProviderName");
            health_provider_info = container.NewColumn<String>("HealthProviderInfo");
            health_provider_notes = container.NewColumn<String>("HealthProviderNotes");
            space_id = container.NewColumn<Int32>("SpaceID");
            advisor_invite_id = container.NewColumn<Int32>("AdvisorInviteID");
        }

        public qOrg_GroupRequests(Int32 group_request_id)
            : this()
        {
            container.Select("GroupRequestID = @GroupRequestID", new SqlQueryParameter("@GroupRequestID", group_request_id));
        }

        public void Update()
        {
            container.Update("GroupRequestID = @GroupRequestID");
        }

        public void Insert()
        {
            GroupRequestID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qOrg_GroupRequests> GetGroupRequests()
        {
            return schema.container.Select<qOrg_GroupRequests>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "School ASC"
                }, c => new qOrg_GroupRequests(c));
        }

        public static ICollection<qOrg_GroupRequests> GetPendingGroupRequests()
        {
            return schema.container.Select<qOrg_GroupRequests>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND Status = 'Pending'",
                    OrderBy = "Created DESC"
                }, c => new qOrg_GroupRequests(c));
        }

        public static ICollection<qOrg_GroupRequests> GetWaitingPrincipalApprovalGroupRequests()
        {
            return schema.container.Select<qOrg_GroupRequests>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND Status = 'Waiting Principal Approval'",
                    OrderBy = "Created DESC"
                }, c => new qOrg_GroupRequests(c));
        }


        public static ICollection<qOrg_GroupRequests> GetApprovedGroupRequests()
        {
            return schema.container.Select<qOrg_GroupRequests>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND Status = 'Approved'",
                    OrderBy = "Created DESC"
                }, c => new qOrg_GroupRequests(c));
        }

        public static ICollection<qOrg_GroupRequests> GetDeclinedGroupRequests()
        {
            return schema.container.Select<qOrg_GroupRequests>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND Status = 'Declined'",
                    OrderBy = "Created DESC"
                }, c => new qOrg_GroupRequests(c));
        }

        public static ICollection<qOrg_GroupRequests> GetSchoolsByState(string state_province)
        {
            return schema.container.Select<qOrg_GroupRequests>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND StateProvince = @StateProvince",
                    OrderBy = "School ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@StateProvince", state_province) }
                }, c => new qOrg_GroupRequests(c));
        }

        public static ICollection<qOrg_GroupRequests> GetRequestsBySchoolDistrict(int school_district_id)
        {
            return schema.container.Select<qOrg_GroupRequests>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND SchoolDistrictID = @SchoolDistrictID",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@SchoolDistrictID", school_district_id) }
                }, c => new qOrg_GroupRequests(c));
        }
    }
}
