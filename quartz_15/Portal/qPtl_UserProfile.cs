using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_UserProfile
    {
        protected static qPtl_UserProfile schema = new qPtl_UserProfile();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_profile_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> address1;
        protected readonly DbColumn<String> address2;
        protected readonly DbColumn<String> city;
        protected readonly DbColumn<String> state_province;
        protected readonly DbColumn<String> postal_code;
        protected readonly DbColumn<String> country;
        protected readonly DbColumn<String> phone1;
        protected readonly DbColumn<String> phone1type;
        protected readonly DbColumn<String> phone2;
        protected readonly DbColumn<String> phone2type;
        protected readonly DbColumn<String> website;
        protected readonly DbColumn<String> comments;
        protected readonly DbColumn<DateTime?> dob;
        protected readonly DbColumn<Int32> age;
        protected readonly DbColumn<String> gender;
        protected readonly DbColumn<String> style;
        protected readonly DbColumn<String> visibility;
        protected readonly DbColumn<String> selected_language;
        protected readonly DbColumn<String> translation_preference;
        protected readonly DbColumn<String> selected_region;
        protected readonly DbColumn<String> hear_about_us;
        protected readonly DbColumn<String> other_hear_about_us;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<String> agree_status;
        protected readonly DbColumn<DateTime?> agree_rules;
        protected readonly DbColumn<Int32> num_points;
        protected readonly DbColumn<Int32> percentage_complete;
        protected readonly DbColumn<Int32> theme_id;
        protected readonly DbColumn<String> ethnicity;
        protected readonly DbColumn<String> race;
        protected readonly DbColumn<String> first_involvement;
        protected readonly DbColumn<String> profession;
        protected readonly DbColumn<String> employment_location;
        protected readonly DbColumn<String> employment_setting;
        protected readonly DbColumn<String> work_sites;
        protected readonly DbColumn<String> degrees;
        protected readonly DbColumn<String> position;
        protected readonly DbColumn<String> agency;
        protected readonly DbColumn<String> division;

        public Int32 UserProfileID { get { return user_profile_id.Value; } set { user_profile_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String Address1 { get { return address1.Value; } set { address1.Value = value; } }
        public String Address2 { get { return address2.Value; } set { address2.Value = value; } }
        public String City { get { return city.Value; } set { city.Value = value; } }
        public String StateProvince { get { return state_province.Value; } set { state_province.Value = value; } }
        public String PostalCode { get { return postal_code.Value; } set { postal_code.Value = value; } }
        public String Country { get { return country.Value; } set { country.Value = value; } }
        public String Phone1 { get { return phone1.Value; } set { phone1.Value = value; } }
        public String Phone1Type { get { return phone1type.Value; } set { phone1type.Value = value; } }
        public String Phone2 { get { return phone2.Value; } set { phone2.Value = value; } }
        public String Phone2Type { get { return phone2type.Value; } set { phone2type.Value = value; } }
        public String Website { get { return website.Value; } set { website.Value = value; } }
        public String Comments { get { return comments.Value; } set { comments.Value = value; } }
        public DateTime? DOB { get { return dob.Value; } set { dob.Value = value; } }
        public Int32 Age { get { return age.Value; } set { age.Value = value; } }
        public String Gender { get { return gender.Value; } set { gender.Value = value; } }
        public String Style { get { return style.Value; } set { style.Value = value; } }
        public String Visibility { get { return visibility.Value; } set { visibility.Value = value; } }
        public String SelectedLanguage { get { return selected_language.Value; } set { selected_language.Value = value; } }
        public String TranslationPreference { get { return translation_preference.Value; } set { translation_preference.Value = value; } }
        public String SelectedRegion { get { return selected_region.Value; } set { selected_region.Value = value; } }
        public String HearAboutUs { get { return hear_about_us.Value; } set { hear_about_us.Value = value; } }
        public String OtherHearAboutUs { get { return other_hear_about_us.Value; } set { other_hear_about_us.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public String AgreeStatus { get { return agree_status.Value; } set { agree_status.Value = value; } }
        public DateTime? AgreeRules { get { return agree_rules.Value; } set { agree_rules.Value = value; } }
        public Int32 NumPoints { get { return num_points.Value; } set { num_points.Value = value; } }
        public Int32 PercentageComplete { get { return percentage_complete.Value; } set { percentage_complete.Value = value; } }
        public Int32 ThemeID { get { return theme_id.Value; } set { theme_id.Value = value; } }
        public String Ethnicity { get { return ethnicity.Value; } set { ethnicity.Value = value; } }
        public String Race { get { return race.Value; } set { race.Value = value; } }
        public String FirstInvolvement { get { return first_involvement.Value; } set { first_involvement.Value = value; } }
        public String Profession { get { return profession.Value; } set { profession.Value = value; } }
        public String EmploymentLocation { get { return employment_location.Value; } set { employment_location.Value = value; } }
        public String EmploymentSetting { get { return employment_setting.Value; } set { employment_setting.Value = value; } }
        public String WorkSites { get { return work_sites.Value; } set { work_sites.Value = value; } }
        public String Degrees { get { return degrees.Value; } set { degrees.Value = value; } }
        public String Position { get { return position.Value; } set { position.Value = value; } }
        public String Agency { get { return agency.Value; } set { agency.Value = value; } }
        public String Division { get { return division.Value; } set { division.Value = value; } }

        public qPtl_UserProfile()
            : this(new DbRow())
        {
        }

        protected qPtl_UserProfile(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserProfiles");
            user_profile_id = container.NewColumn<Int32>("UserProfileID", true);
            user_id = container.NewColumn<Int32>("UserID");
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            address1 = container.NewColumn<String>("Address1");
            address2 = container.NewColumn<String>("Address2");
            city = container.NewColumn<String>("City");
            state_province = container.NewColumn<String>("StateProvince");
            postal_code = container.NewColumn<String>("PostalCode");
            country = container.NewColumn<String>("Country");
            phone1 = container.NewColumn<String>("Phone1");
            phone1type = container.NewColumn<String>("Phone1Type");
            phone2 = container.NewColumn<String>("Phone2");
            phone2type = container.NewColumn<String>("Phone2Type");
            website = container.NewColumn<String>("Website");
            comments = container.NewColumn<String>("Comments");
            dob = container.NewColumn<DateTime?>("DOB");
            age = container.NewColumn<Int32>("Age", true);
            gender = container.NewColumn<String>("Gender");
            style = container.NewColumn<String>("Style");
            visibility = container.NewColumn<String>("Visibility");
            selected_language = container.NewColumn<String>("SelectedLanguage");
            translation_preference = container.NewColumn<String>("TranslationPreference");
            selected_region = container.NewColumn<String>("SelectedRegion");
            hear_about_us = container.NewColumn<String>("HearAboutUs");
            other_hear_about_us = container.NewColumn<String>("OtherHearAboutUs");
            description = container.NewColumn<String>("Description");
            agree_status = container.NewColumn<String>("AgreeStatus");
            agree_rules = container.NewColumn<DateTime?>("AgreeRules");
            num_points = container.NewColumn<Int32>("NumPoints");
            percentage_complete = container.NewColumn<Int32>("PercentageComplete");
            theme_id = container.NewColumn<Int32>("ThemeID");
            ethnicity = container.NewColumn<String>("Ethnicity");
            race = container.NewColumn<String>("Race");
            first_involvement = container.NewColumn<String>("FirstInvolvement");
            profession = container.NewColumn<String>("Profession");
            employment_location = container.NewColumn<String>("EmploymentLocation");
            employment_setting = container.NewColumn<String>("EmploymentSetting");
            work_sites = container.NewColumn<String>("WorkSites");
            degrees = container.NewColumn<String>("Degrees");
            position = container.NewColumn<String>("Position");
            agency = container.NewColumn<String>("Agency");
            division = container.NewColumn<String>("Division");
        }

        public qPtl_UserProfile(Int32 user_id)
            : this()
        {
            container.Select("UserID = @UserID", new SqlQueryParameter("@UserID", user_id));
        }

        public void Update()
        {
            container.Update("UserID = @UserID");
        }

        public void Insert()
        {
            UserID = Convert.ToInt32(container.Insert());
        }

        public void SetTheme(int theme_id)
        {
            ThemeID = theme_id;
            LastModified = DateTime.Now;

            container.Update("UserID = @UserID");
        }

        public static qPtl_UserProfile GetProfileByMobileNumber(string phone)
        {
            var profile = new qPtl_UserProfile();
            string sql = string.Format("(Phone1Type = 'Mobile' AND Phone1 = '{0}') OR (Phone2Type = 'Mobile' AND Phone2 = '{0}')", phone);

            profile.container.Select(
                new DbQuery
                {
                    Where = sql
                });

            return profile;
        }

        public static qPtl_UserProfile GetMobileNumberByUserID(int user_id)
        {
            var profile = new qPtl_UserProfile();
            string sql = string.Format("(Phone1Type = 'Mobile' AND UserID = {0}) OR (Phone2Type = 'Mobile' AND UserID = {0})", user_id);

            profile.container.Select(
                new DbQuery
                {
                    Where = sql
                });

            return profile;
        }

        public static ICollection<qPtl_UserProfile> GetPointLeaders(int num_users_to_return)
        {
            return schema.container.Select<qPtl_UserProfile>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND URL Is Not Null",
                    OrderBy = "NumPoints DESC",
                }, c => new qPtl_UserProfile(c));
        }
    }

    public class qPtl_UserProfile_View
    {
        protected static qPtl_UserProfile_View schema = new qPtl_UserProfile_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_profile_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> address1;
        protected readonly DbColumn<String> address2;
        protected readonly DbColumn<String> city;
        protected readonly DbColumn<String> state_province;
        protected readonly DbColumn<String> postal_code;
        protected readonly DbColumn<String> country;
        protected readonly DbColumn<String> phone1;
        protected readonly DbColumn<String> phone1type;
        protected readonly DbColumn<String> phone2;
        protected readonly DbColumn<String> phone2type;
        protected readonly DbColumn<String> website;
        protected readonly DbColumn<String> comments;
        protected readonly DbColumn<DateTime?> dob;
        protected readonly DbColumn<Int32> age;
        protected readonly DbColumn<String> gender;
        protected readonly DbColumn<String> style;
        protected readonly DbColumn<String> visibility;
        protected readonly DbColumn<String> selected_language;
        protected readonly DbColumn<String> translation_preference;
        protected readonly DbColumn<String> selected_region;
        protected readonly DbColumn<String> hear_about_us;
        protected readonly DbColumn<String> other_hear_about_us;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<String> agree_status;
        protected readonly DbColumn<DateTime?> agree_rules;
        protected readonly DbColumn<Int32> num_points;
        protected readonly DbColumn<Int32> percentage_complete;
        protected readonly DbColumn<Int32> theme_id;
        protected readonly DbColumn<String> ethnicity;
        protected readonly DbColumn<String> race;
        protected readonly DbColumn<String> first_involvement;
        protected readonly DbColumn<String> profession;
        protected readonly DbColumn<String> employment_location;
        protected readonly DbColumn<String> employment_setting;
        protected readonly DbColumn<String> work_sites;
        protected readonly DbColumn<String> degrees;
        protected readonly DbColumn<String> position;
        protected readonly DbColumn<String> agency;
        protected readonly DbColumn<String> division;
        protected readonly DbColumn<String> username;
        protected readonly DbColumn<String> highest_role;
        protected readonly DbColumn<String> profile_pict;

        public Int32 UserProfileID { get { return user_profile_id.Value; } set { user_profile_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String Address1 { get { return address1.Value; } set { address1.Value = value; } }
        public String Address2 { get { return address2.Value; } set { address2.Value = value; } }
        public String City { get { return city.Value; } set { city.Value = value; } }
        public String StateProvince { get { return state_province.Value; } set { state_province.Value = value; } }
        public String PostalCode { get { return postal_code.Value; } set { postal_code.Value = value; } }
        public String Country { get { return country.Value; } set { country.Value = value; } }
        public String Phone1 { get { return phone1.Value; } set { phone1.Value = value; } }
        public String Phone1Type { get { return phone1type.Value; } set { phone1type.Value = value; } }
        public String Phone2 { get { return phone2.Value; } set { phone2.Value = value; } }
        public String Phone2Type { get { return phone2type.Value; } set { phone2type.Value = value; } }
        public String Website { get { return website.Value; } set { website.Value = value; } }
        public String Comments { get { return comments.Value; } set { comments.Value = value; } }
        public DateTime? DOB { get { return dob.Value; } set { dob.Value = value; } }
        public Int32 Age { get { return age.Value; } set { age.Value = value; } }
        public String Gender { get { return gender.Value; } set { gender.Value = value; } }
        public String Style { get { return style.Value; } set { style.Value = value; } }
        public String Visibility { get { return visibility.Value; } set { visibility.Value = value; } }
        public String SelectedLanguage { get { return selected_language.Value; } set { selected_language.Value = value; } }
        public String TranslationPreference { get { return translation_preference.Value; } set { translation_preference.Value = value; } }
        public String SelectedRegion { get { return selected_region.Value; } set { selected_region.Value = value; } }
        public String HearAboutUs { get { return hear_about_us.Value; } set { hear_about_us.Value = value; } }
        public String OtherHearAboutUs { get { return other_hear_about_us.Value; } set { other_hear_about_us.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public String AgreeStatus { get { return agree_status.Value; } set { agree_status.Value = value; } }
        public DateTime? AgreeRules { get { return agree_rules.Value; } set { agree_rules.Value = value; } }
        public Int32 NumPoints { get { return num_points.Value; } set { num_points.Value = value; } }
        public Int32 PercentageComplete { get { return percentage_complete.Value; } set { percentage_complete.Value = value; } }
        public Int32 ThemeID { get { return theme_id.Value; } set { theme_id.Value = value; } }
        public String Ethnicity { get { return ethnicity.Value; } set { ethnicity.Value = value; } }
        public String Race { get { return race.Value; } set { race.Value = value; } }
        public String FirstInvolvement { get { return first_involvement.Value; } set { first_involvement.Value = value; } }
        public String Profession { get { return profession.Value; } set { profession.Value = value; } }
        public String EmploymentLocation { get { return employment_location.Value; } set { employment_location.Value = value; } }
        public String EmploymentSetting { get { return employment_setting.Value; } set { employment_setting.Value = value; } }
        public String WorkSites { get { return work_sites.Value; } set { work_sites.Value = value; } }
        public String Degrees { get { return degrees.Value; } set { degrees.Value = value; } }
        public String Position { get { return position.Value; } set { position.Value = value; } }
        public String Agency { get { return agency.Value; } set { agency.Value = value; } }
        public String Division { get { return division.Value; } set { division.Value = value; } }
        public String UserName { get { return username.Value; } set { username.Value = value; } }
        public String HighestRole { get { return highest_role.Value; } set { highest_role.Value = value; } }
        public String ProfilePict { get { return profile_pict.Value; } set { profile_pict.Value = value; } }

        public qPtl_UserProfile_View()
            : this(new DbRow())
        {
        }

        protected qPtl_UserProfile_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserProfiles_View");
            user_profile_id = container.NewColumn<Int32>("UserProfileID", true);
            user_id = container.NewColumn<Int32>("UserID");
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            address1 = container.NewColumn<String>("Address1");
            address2 = container.NewColumn<String>("Address2");
            city = container.NewColumn<String>("City");
            state_province = container.NewColumn<String>("StateProvince");
            postal_code = container.NewColumn<String>("PostalCode");
            country = container.NewColumn<String>("Country");
            phone1 = container.NewColumn<String>("Phone1");
            phone1type = container.NewColumn<String>("Phone1Type");
            phone2 = container.NewColumn<String>("Phone2");
            phone2type = container.NewColumn<String>("Phone2Type");
            website = container.NewColumn<String>("Website");
            comments = container.NewColumn<String>("Comments");
            dob = container.NewColumn<DateTime?>("DOB");
            age = container.NewColumn<Int32>("Age", true);
            gender = container.NewColumn<String>("Gender");
            style = container.NewColumn<String>("Style");
            visibility = container.NewColumn<String>("Visibility");
            selected_language = container.NewColumn<String>("SelectedLanguage");
            translation_preference = container.NewColumn<String>("TranslationPreference");
            selected_region = container.NewColumn<String>("SelectedRegion");
            hear_about_us = container.NewColumn<String>("HearAboutUs");
            other_hear_about_us = container.NewColumn<String>("OtherHearAboutUs");
            description = container.NewColumn<String>("Description");
            agree_status = container.NewColumn<String>("AgreeStatus");
            agree_rules = container.NewColumn<DateTime?>("AgreeRules");
            num_points = container.NewColumn<Int32>("NumPoints");
            percentage_complete = container.NewColumn<Int32>("PercentageComplete");
            theme_id = container.NewColumn<Int32>("ThemeID");
            ethnicity = container.NewColumn<String>("Ethnicity");
            race = container.NewColumn<String>("Race");
            first_involvement = container.NewColumn<String>("FirstInvolvement");
            profession = container.NewColumn<String>("Profession");
            employment_location = container.NewColumn<String>("EmploymentLocation");
            employment_setting = container.NewColumn<String>("EmploymentSetting");
            work_sites = container.NewColumn<String>("WorkSites");
            degrees = container.NewColumn<String>("Degrees");
            position = container.NewColumn<String>("Position");
            agency = container.NewColumn<String>("Agency");
            division = container.NewColumn<String>("Division");
            username = container.NewColumn<String>("UserName");
            highest_role = container.NewColumn<String>("HighestRole");
            profile_pict = container.NewColumn<String>("ProfilePict");
        }

        public qPtl_UserProfile_View(Int32 user_id)
            : this()
        {
            container.Select("UserID = @UserID", new SqlQueryParameter("@UserID", user_id));
        }

        public static ICollection<qPtl_UserProfile_View> GetPointLeaders(int num_users_to_return, string limit_to_role)
        {
            string top_sql = "TOP(100)";
            if (num_users_to_return > 0)
                top_sql = "TOP(" + num_users_to_return + ")";

            string where_sql = "Available = 'Yes' AND MarkAsDelete = 0";
            if (!String.IsNullOrEmpty(limit_to_role))
                where_sql = "Available = 'Yes' AND MarkAsDelete = 0 AND HighestRole = '" + limit_to_role + "'";

            return schema.container.Select<qPtl_UserProfile_View>(
                new DbQuery
                {
                    Top = top_sql,
                    Where = where_sql,
                    OrderBy = "NumPoints DESC",
                }, c => new qPtl_UserProfile_View(c));
        }
    }
}
