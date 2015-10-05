using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Communication
{
    public class qCom_EmailPreference
    {
        protected static qCom_EmailPreference schema = new qCom_EmailPreference();

        protected DbRow container;
        protected readonly DbColumn<Int32> email_preferences_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> header;
        protected readonly DbColumn<String> footer;
        protected readonly DbColumn<String> from_email_address;
        protected readonly DbColumn<String> from_name;
        protected readonly DbColumn<String> unsubscribe;
        protected readonly DbColumn<String> smtp_username;
        protected readonly DbColumn<String> smtp_password;
        protected readonly DbColumn<String> smtp_server;
        protected readonly DbColumn<Int32> smtp_port;
        protected readonly DbColumn<Boolean> smtp_ssl;

        public Int32 EmailPreferencesID { get { return email_preferences_id.Value; } set { email_preferences_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String Header { get { return header.Value; } set { header.Value = value; } }
        public String Footer { get { return footer.Value; } set { footer.Value = value; } }
        public String FromEmailAddress { get { return from_email_address.Value; } set { from_email_address.Value = value; } }
        public String FromName { get { return from_name.Value; } set { from_name.Value = value; } }
        public String Unsubscribe { get { return unsubscribe.Value; } set { unsubscribe.Value = value; } }
        public String SMTPUsername { get { return smtp_username.Value; } set { smtp_username.Value = value; } }
        public String SMTPPassword { get { return smtp_password.Value; } set { smtp_password.Value = value; } }
        public String SMTPServer { get { return smtp_server.Value; } set { smtp_server.Value = value; } }
        public Int32 SMTPPort { get { return smtp_port.Value; } set { smtp_port.Value = value; } }
        public Boolean SMTPSSL { get { return smtp_ssl.Value; } set { smtp_ssl.Value = value; } }

        public qCom_EmailPreference()
            : this(new DbRow())
        {
        }

        protected qCom_EmailPreference(DbRow c)
        {
            container = c;
            container.SetContainerName("qCom_EmailPreferences");
            email_preferences_id = container.NewColumn<Int32>("EmailPreferencesID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            header = container.NewColumn<String>("header");
            footer = container.NewColumn<String>("footer");
            from_email_address = container.NewColumn<String>("FromEmailAddress");
            from_name = container.NewColumn<String>("FromName");
            unsubscribe = container.NewColumn<String>("Unsubscribe");
            smtp_username = container.NewColumn<String>("SMTPUsername");
            smtp_password = container.NewColumn<String>("SMTPPassword");
            smtp_server = container.NewColumn<String>("SMTPServer");
            smtp_port = container.NewColumn<Int32>("SMTPPort");
            smtp_ssl = container.NewColumn<Boolean>("SMTPSSL");
        }

        public qCom_EmailPreference(Int32 email_preferences_id)
            : this()
        {
            container.Select("EmailPreferencesID = @EmailPreferencesID", new SqlQueryParameter("@EmailPreferencesID", email_preferences_id));
        }

        public void Update()
        {
            container.Update("EmailPreferencesID = @EmailPreferencesID");
        }

        public void Insert()
        {
            EmailPreferencesID = Convert.ToInt32(container.Insert());
        }

        public static qCom_EmailPreference GetEmailPreferences()
        {
            qCom_EmailPreference preferences = new qCom_EmailPreference();

            preferences.container.Select("MarkAsDelete = 0 AND Available = 'Yes'");

            return preferences;
        }

        public static qCom_EmailPreference GetEmailPreferencesByScopeID(Int32 scopeID)
        {
            qCom_EmailPreference preferences = new qCom_EmailPreference();

            preferences.container.Select("MarkAsDelete = 0 AND Available = 'Yes' AND ScopeID = " + scopeID);

            return preferences;
        }
    }
}
