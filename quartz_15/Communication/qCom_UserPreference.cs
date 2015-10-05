using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quartz.Portal;

namespace Quartz.Communication
{
    public class qCom_UserPreference
    {
        protected static qCom_UserPreference schema = new qCom_UserPreference();

        protected DbRow container;
        protected readonly DbColumn<Int32> contact_preference_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<DateTime?> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> ok_bulk_email;
        protected readonly DbColumn<String> ok_email;
        protected readonly DbColumn<String> ok_phone;
        protected readonly DbColumn<String> ok_fax;
        protected readonly DbColumn<String> ok_solicit;
        protected readonly DbColumn<String> ok_mail;
        protected readonly DbColumn<String> ok_partner_contact;
        protected readonly DbColumn<String> ok_sms;
        protected readonly DbColumn<String> ok_notifications;
        protected readonly DbColumn<String> confirm_sms;
        protected readonly DbColumn<Int32> language_id;
        protected readonly DbColumn<String> mobile_pin;
        protected readonly DbColumn<DateTime?> mobile_pin_verified;
        protected readonly DbColumn<String> default_sms_did;
        protected readonly DbColumn<Int32> sms_active_minutes_window;

        public Int32 ContactPreferenceID { get { return contact_preference_id.Value; } set { contact_preference_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String OkBulkEmail { get { return ok_bulk_email.Value; } set { ok_bulk_email.Value = value; } }
        public String OkEmail { get { return ok_email.Value; } set { ok_email.Value = value; } }
        public String OkPhone { get { return ok_phone.Value; } set { ok_phone.Value = value; } }
        public String OkFax { get { return ok_fax.Value; } set { ok_fax.Value = value; } }
        public String OkSolicit { get { return ok_solicit.Value; } set { ok_solicit.Value = value; } }
        public String OkMail { get { return ok_mail.Value; } set { ok_mail.Value = value; } }
        public String OkPartnerContact { get { return ok_partner_contact.Value; } set { ok_partner_contact.Value = value; } }
        public String OkSms { get { return ok_sms.Value; } set { ok_sms.Value = value; } }
        public String OkNotifications { get { return ok_notifications.Value; } set { ok_notifications.Value = value; } }
        public String ConfirmSms { get { return confirm_sms.Value; } set { confirm_sms.Value = value; } }
        public Int32 LanguageID { get { return language_id.Value; } set { language_id.Value = value; } }
        public String MobilePIN { get { return mobile_pin.Value; } set { mobile_pin.Value = value; } }
        public DateTime? MobilePINverified { get { return mobile_pin_verified.Value; } set { mobile_pin_verified.Value = value; } }
        public String DefaultSmsDID { get { return default_sms_did.Value; } set { default_sms_did.Value = value; } }
        public Int32 SmsActiveMinutesWindow { get { return sms_active_minutes_window.Value; } set { sms_active_minutes_window.Value = value; } }

        public qCom_UserPreference()
            : this(new DbRow())
        {
        }

        protected qCom_UserPreference(DbRow c)
        {
            container = c;
            container.SetContainerName("qCom_UserPreferences");
            contact_preference_id = container.NewColumn<Int32>("ContactPreferenceID", true);
            user_id = container.NewColumn<Int32>("UserID");
            available = container.NewColumn<String>("Available");
            scope_id = container.NewColumn<Int32>("ScopeID");
            created = container.NewColumn<DateTime?>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            ok_bulk_email = container.NewColumn<String>("OkBulkEmail");
            ok_email = container.NewColumn<String>("OkEmail");
            ok_phone = container.NewColumn<String>("OkPhone");
            ok_fax = container.NewColumn<String>("OkFax");
            ok_solicit = container.NewColumn<String>("OkSolicit");
            ok_mail = container.NewColumn<String>("OkMail");
            ok_partner_contact = container.NewColumn<String>("OkPartnerContact");
            ok_sms = container.NewColumn<String>("OkSms");
            ok_notifications = container.NewColumn<String>("OkNotifications");
            confirm_sms = container.NewColumn<String>("ConfirmSms");
            language_id = container.NewColumn<Int32>("LanguageID");
            mobile_pin = container.NewColumn<String>("MobilePIN");
            mobile_pin_verified = container.NewColumn<DateTime?>("MobilePINVerified");
            default_sms_did = container.NewColumn<String>("DefaultSmsDid");
            sms_active_minutes_window = container.NewColumn<Int32>("SmsActiveMinutesWindow");
        }

        public qCom_UserPreference(Int32 contact_preference_id)
            : this()
        {
            container.Select("ContactPreferenceID = @ContactPreferenceID", new SqlQueryParameter("@ContactPreferenceID", contact_preference_id));
        }

        public void Update()
        {
            container.Update("ContactPreferenceID = @ContactPreferenceID");
        }

        public void Insert()
        {
            MobilePIN = Convert.ToString(GenerateMobilePIN());

            ContactPreferenceID = Convert.ToInt32(container.Insert());
        }

        public static qCom_UserPreference GetUserPreference(Int32 user_id)
        {
            var user_preference = new qCom_UserPreference();

            user_preference.container.Select(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                });

            return user_preference.ContactPreferenceID > 0 ? user_preference : null;
        }

        public static int GenerateMobilePIN()
        {
            int minPIN = 1000;
            int maxPIN = 9999;
            int randomPIN;

            Random random = new Random();
            randomPIN = random.Next(minPIN, maxPIN);

            return randomPIN;
        }
    }

    public class qCom_UserPreference_View
    {
        protected static qCom_UserPreference_View schema = new qCom_UserPreference_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> contact_preference_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<DateTime?> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> ok_bulk_email;
        protected readonly DbColumn<String> ok_email;
        protected readonly DbColumn<String> ok_phone;
        protected readonly DbColumn<String> ok_fax;
        protected readonly DbColumn<String> ok_solicit;
        protected readonly DbColumn<String> ok_mail;
        protected readonly DbColumn<String> ok_partner_contact;
        protected readonly DbColumn<String> ok_sms;
        protected readonly DbColumn<String> ok_notifications;
        protected readonly DbColumn<String> confirm_sms;
        protected readonly DbColumn<Int32> language_id;
        protected readonly DbColumn<String> mobile_pin;
        protected readonly DbColumn<DateTime?> mobile_pin_verified;
        protected readonly DbColumn<String> default_sms_did;
        protected readonly DbColumn<Int32> sms_active_minutes_window;
        protected readonly DbColumn<String> username;
        protected readonly DbColumn<String> email;
        protected readonly DbColumn<String> user_preferences_name;

        public Int32 ContactPreferenceID { get { return contact_preference_id.Value; } set { contact_preference_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String OkBulkEmail { get { return ok_bulk_email.Value; } set { ok_bulk_email.Value = value; } }
        public String OkEmail { get { return ok_email.Value; } set { ok_email.Value = value; } }
        public String OkPhone { get { return ok_phone.Value; } set { ok_phone.Value = value; } }
        public String OkFax { get { return ok_fax.Value; } set { ok_fax.Value = value; } }
        public String OkSolicit { get { return ok_solicit.Value; } set { ok_solicit.Value = value; } }
        public String OkMail { get { return ok_mail.Value; } set { ok_mail.Value = value; } }
        public String OkPartnerContact { get { return ok_partner_contact.Value; } set { ok_partner_contact.Value = value; } }
        public String OkSms { get { return ok_sms.Value; } set { ok_sms.Value = value; } }
        public String OkNotifications { get { return ok_notifications.Value; } set { ok_notifications.Value = value; } }
        public String ConfirmSms { get { return confirm_sms.Value; } set { confirm_sms.Value = value; } }
        public Int32 LanguageID { get { return language_id.Value; } set { language_id.Value = value; } }
        public String MobilePIN { get { return mobile_pin.Value; } set { mobile_pin.Value = value; } }
        public DateTime? MobilePINverified { get { return mobile_pin_verified.Value; } set { mobile_pin_verified.Value = value; } }
        public String DefaultSmsDID { get { return default_sms_did.Value; } set { default_sms_did.Value = value; } }
        public Int32 SmsActiveMinutesWindow { get { return sms_active_minutes_window.Value; } set { sms_active_minutes_window.Value = value; } }
        public String Username { get { return username.Value; } set { username.Value = value; } }
        public String Email { get { return email.Value; } set { email.Value = value; } }
        public String UserPreferences_Name { get { return user_preferences_name.Value; } set { user_preferences_name.Value = value; } }

        public qCom_UserPreference_View()
            : this(new DbRow())
        {
        }

        protected qCom_UserPreference_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qCom_UserPreferences_View");
            contact_preference_id = container.NewColumn<Int32>("ContactPreferenceID", true);
            user_id = container.NewColumn<Int32>("UserID");
            available = container.NewColumn<String>("Available");
            scope_id = container.NewColumn<Int32>("ScopeID");
            created = container.NewColumn<DateTime?>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            ok_bulk_email = container.NewColumn<String>("OkBulkEmail");
            ok_email = container.NewColumn<String>("OkEmail");
            ok_phone = container.NewColumn<String>("OkPhone");
            ok_fax = container.NewColumn<String>("OkFax");
            ok_solicit = container.NewColumn<String>("OkSolicit");
            ok_mail = container.NewColumn<String>("OkMail");
            ok_partner_contact = container.NewColumn<String>("OkPartnerContact");
            ok_sms = container.NewColumn<String>("OkSms");
            ok_notifications = container.NewColumn<String>("OkNotifications");
            confirm_sms = container.NewColumn<String>("ConfirmSms");
            language_id = container.NewColumn<Int32>("LanguageID");
            mobile_pin = container.NewColumn<String>("MobilePIN");
            mobile_pin_verified = container.NewColumn<DateTime?>("MobilePINVerified");
            default_sms_did = container.NewColumn<String>("DefaultSmsDid");
            sms_active_minutes_window = container.NewColumn<Int32>("SmsActiveMinutesWindow");
            username = container.NewColumn<String>("Username");
            email = container.NewColumn<String>("Email");
            user_preferences_name = container.NewColumn<String>("UserPreferences_Name");
        }

        public qCom_UserPreference_View(Int32 contact_preference_id)
            : this()
        {
            container.Select("ContactPreferenceID = @ContactPreferenceID", new SqlQueryParameter("@ContactPreferenceID", contact_preference_id));
        }

        public qCom_UserPreference_View(String email)
            : this()
        {
            container.Select("Email = @Email", new SqlQueryParameter("@Email", email));
        }
    }
}
