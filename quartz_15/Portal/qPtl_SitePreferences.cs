using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_SitePreferences
    {
        protected static qPtl_SitePreferences schema = new qPtl_SitePreferences();

        protected DbRow container;
        protected readonly DbColumn<Int32> site_preferences;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> url;
        protected readonly DbColumn<String> site_online;
        protected readonly DbColumn<String> offline_message;
        protected readonly DbColumn<String> basic_message;
        protected readonly DbColumn<String> critical_message;
        protected readonly DbColumn<Int32> site_ambassador;
        protected readonly DbColumn<String> default_theme;
        protected readonly DbColumn<String> default_cms_layout;
        protected readonly DbColumn<String> default_layout;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<String> keywords;
        protected readonly DbColumn<String> default_telerik_theme;
        protected readonly DbColumn<String> language_support;
        protected readonly DbColumn<String> facebook_support;
        protected readonly DbColumn<String> facebook_panel_code;
        protected readonly DbColumn<String> register_language;
        protected readonly DbColumn<String> register_parental_consent;
        protected readonly DbColumn<String> register_mode;
        protected readonly DbColumn<String> register_username_mode;
        protected readonly DbColumn<Int32> register_min_age;
        protected readonly DbColumn<Int32> register_max_age;
        protected readonly DbColumn<String> account_functional_roles;
        protected readonly DbColumn<String> interface_mode;
        protected readonly DbColumn<Int32> min_password_length;
        protected readonly DbColumn<Int32> max_password_length;
        protected readonly DbColumn<String> password_standard;
        protected readonly DbColumn<String> analytics;

        public Int32 SitePreferences { get { return site_preferences.Value; } set { site_preferences.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String URL { get { return url.Value; } set { url.Value = value; } }
        public String SiteOnline { get { return site_online.Value; } set { site_online.Value = value; } }
        public String OfflineMessage { get { return offline_message.Value; } set { offline_message.Value = value; } }
        public String BasicMessage { get { return basic_message.Value; } set { basic_message.Value = value; } }
        public String CriticalMessage { get { return critical_message.Value; } set { critical_message.Value = value; } }
        public Int32 SiteAmbassador { get { return site_ambassador.Value; } set { site_ambassador.Value = value; } }
        public String DefaultTheme { get { return default_theme.Value; } set { default_theme.Value = value; } }
        public String DefaultCMSLayout { get { return default_cms_layout.Value; } set { default_cms_layout.Value = value; } }
        public String DefaultLayout { get { return default_layout.Value; } set { default_layout.Value = value; } }
        public String Title { get { return title.Value; } set { title.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public String Keywords { get { return keywords.Value; } set { keywords.Value = value; } }
        public String DefaultTelerikTheme { get { return default_telerik_theme.Value; } set { default_telerik_theme.Value = value; } }
        public String LanguageSupport { get { return language_support.Value; } set { language_support.Value = value; } }
        public String FacebookSupport { get { return facebook_support.Value; } set { facebook_support.Value = value; } }
        public String FacebookPanelCode { get { return facebook_panel_code.Value; } set { facebook_panel_code.Value = value; } }
        public String Register_Language { get { return register_language.Value; } set { register_language.Value = value; } }
        public String Register_ParentalConsent { get { return register_parental_consent.Value; } set { register_parental_consent.Value = value; } }
        public String Register_Mode { get { return register_mode.Value; } set { register_mode.Value = value; } }
        public String Register_UsernameMode { get { return register_username_mode.Value; } set { register_username_mode.Value = value; } }
        public Int32 Register_MinAge { get { return register_min_age.Value; } set { register_min_age.Value = value; } }
        public Int32 Register_MaxAge { get { return register_max_age.Value; } set { register_max_age.Value = value; } }
        public String Account_FunctionalRoles { get { return account_functional_roles.Value; } set { account_functional_roles.Value = value; } }
        public String InterfaceMode { get { return interface_mode.Value; } set { interface_mode.Value = value; } }
        public Int32 MinPasswordLength { get { return min_password_length.Value; } set { min_password_length.Value = value; } }
        public Int32 MaxPasswordLength { get { return max_password_length.Value; } set { max_password_length.Value = value; } }
        public String PasswordStandard { get { return password_standard.Value; } set { password_standard.Value = value; } }
        public String Analytics { get { return analytics.Value; } set { analytics.Value = value; } }

        public qPtl_SitePreferences()
            : this(new DbRow())
        {
        }

        protected qPtl_SitePreferences(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_SitePreferences");
            site_preferences = container.NewColumn<Int32>("SitePreferences", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            url = container.NewColumn<String>("URL");
            site_online = container.NewColumn<String>("SiteOnline");
            offline_message = container.NewColumn<String>("OfflineMessage");
            basic_message = container.NewColumn<String>("BasicMessage");
            critical_message = container.NewColumn<String>("CriticalMessage");
            site_ambassador = container.NewColumn<Int32>("SiteAmbassador");
            default_theme = container.NewColumn<String>("DefaultTheme");
            default_cms_layout = container.NewColumn<String>("CMSDefaultLayout");
            default_layout = container.NewColumn<String>("DefaultLayout");
            title = container.NewColumn<String>("Title");
            description = container.NewColumn<String>("Description");
            keywords = container.NewColumn<String>("Keywords");
            default_telerik_theme = container.NewColumn<String>("DefaultTelerikTheme");
            language_support = container.NewColumn<String>("LanguageSupport");
            facebook_support = container.NewColumn<String>("FacebookSupport");
            facebook_panel_code = container.NewColumn<String>("FacebookPanelCode");
            register_language = container.NewColumn<String>("Register_Language");
            register_parental_consent = container.NewColumn<String>("Register_ParentalConsent");
            register_mode = container.NewColumn<String>("Register_Mode");
            register_username_mode = container.NewColumn<String>("Register_UsernameMode");
            register_min_age = container.NewColumn<Int32>("Register_MinAge");
            register_max_age = container.NewColumn<Int32>("Register_MaxAge");
            account_functional_roles = container.NewColumn<String>("Account_FunctionalRoles");
            interface_mode = container.NewColumn<String>("InterfaceMode");
            min_password_length = container.NewColumn<Int32>("MinPasswordLength");
            max_password_length = container.NewColumn<Int32>("MaxPasswordLength");
            password_standard = container.NewColumn<String>("PasswordStandard");
            analytics = container.NewColumn<String>("Analytics");
        }

        public qPtl_SitePreferences(Int32 site_preferences)
            : this()
        {
            container.Select("SitePreferences = @SitePreferences", new SqlQueryParameter("@SitePreferences", site_preferences));
        }

        public void Update()
        {
            container.Update("SitePreferences = @SitePreferences");
        }

        public void Insert()
        {
            SitePreferences = Convert.ToInt32(container.Insert());
        }

        public static qPtl_SitePreferences GetSitePreferences()
        {
            qPtl_SitePreferences preferences = new qPtl_SitePreferences();

            preferences.container.Select("SitePreferences = 1");

            return preferences;
        }

        public static qPtl_SitePreferences GetSitePreferencesByScopeID(Int32 scopeID)
        {
            qPtl_SitePreferences preferences = new qPtl_SitePreferences();

            preferences.container.Select("ScopeID = " + scopeID);

            return preferences;
        }

        public static qPtl_SitePreferences GetSitePreferencesByURL(String url)
        {
            qPtl_SitePreferences preferences = new qPtl_SitePreferences();

            preferences.container.Select("URL = " + url);

            return preferences;
        }
    }
}
