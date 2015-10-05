using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Communication
{
    public class qCom_Contact
    {
       protected static qCom_Contact schema = new qCom_Contact();

       protected DbRow container;
       protected readonly DbColumn<Int32> contact_id;
       protected readonly DbColumn<Int32> scope_id;
       protected readonly DbColumn<String> available;
       protected readonly DbColumn<DateTime> created;
       protected readonly DbColumn<Int32> created_by;
       protected readonly DbColumn<DateTime?> last_modified;
       protected readonly DbColumn<Int32> last_modified_by;
       protected readonly DbColumn<Int32> mark_as_delete;
       protected readonly DbColumn<String> first_name;
       protected readonly DbColumn<String> last_name;
       protected readonly DbColumn<String> email;
       protected readonly DbColumn<String> keywords;
       protected readonly DbColumn<String> source;
       protected readonly DbColumn<Int32> user_id;
       protected readonly DbColumn<String> ok_email;
       protected readonly DbColumn<String> did;
       protected readonly DbColumn<String> partner;
       protected readonly DbColumn<Int32> main_group;
       protected readonly DbColumn<Int32> sub_group;
       protected readonly DbColumn<String> custom_html_element;
       protected readonly DbColumn<String> dm_misc;
       protected readonly DbColumn<DateTime?> unsubscribed;
       protected readonly DbColumn<Int32> unsubscribed_campaign_id;
       protected readonly DbColumn<DateTime?> reported_as_spam;
       protected readonly DbColumn<Int32> reported_as_spam_campaign_id;
       protected readonly DbColumn<String> action_log;

       public Int32 ContactID { get { return contact_id.Value; } set { contact_id.Value = value; } }
       public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
       public String Available { get { return available.Value; } set { available.Value = value; } }
       public DateTime Created { get { return created.Value; } set { created.Value = value; } }
       public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
       public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
       public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
       public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
       public String FirstName { get { return first_name.Value; } set { first_name.Value = value; } }
       public String LastName { get { return last_name.Value; } set { last_name.Value = value; } }
       public String Email { get { return email.Value; } set { email.Value = value; } }
       public String Keywords { get { return keywords.Value; } set { keywords.Value = value; } }
       public String Source { get { return source.Value; } set { source.Value = value; } }
       public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
       public String OKEmail { get { return ok_email.Value; } set { ok_email.Value = value; } }
       public String DID { get { return did.Value; } set { did.Value = value; } }
       public String Partner { get { return partner.Value; } set { partner.Value = value; } }
       public Int32 MainGroup { get { return main_group.Value; } set { main_group.Value = value; } }
       public Int32 SubGroup { get { return sub_group.Value; } set { sub_group.Value = value; } }
       public String CustomHTMLElement { get { return custom_html_element.Value; } set { custom_html_element.Value = value; } }
       public String DM_Misc { get { return dm_misc.Value; } set { dm_misc.Value = value; } }
       public DateTime? Unsubscribed { get { return unsubscribed.Value; } set { unsubscribed.Value = value; } }
       public Int32 UnsubscribedCampaignID { get { return unsubscribed_campaign_id.Value; } set { unsubscribed_campaign_id.Value = value; } }
       public DateTime? ReportedAsSpam { get { return reported_as_spam.Value; } set { reported_as_spam.Value = value; } }
       public Int32 ReportedAsSpamCampaignID { get { return reported_as_spam_campaign_id.Value; } set { reported_as_spam_campaign_id.Value = value; } }
       public String LogAction { get { return action_log.Value; } set { action_log.Value = value; } }

       public qCom_Contact()
           : this(new DbRow())
       {
       }

       protected qCom_Contact(DbRow c)
       {
           container = c;
           container.SetContainerName("qCom_Contacts");
           contact_id = container.NewColumn<Int32>("ContactID", true);
           scope_id = container.NewColumn<Int32>("ScopeID");
           available = container.NewColumn<String>("Available");
           created = container.NewColumn<DateTime>("Created");
           created_by = container.NewColumn<Int32>("CreatedBy");
           last_modified = container.NewColumn<DateTime?>("LastModified");
           last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
           mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
           first_name = container.NewColumn<String>("FirstName");
           last_name = container.NewColumn<String>("LastName");
           email = container.NewColumn<String>("Email");
           keywords = container.NewColumn<String>("Keywords");
           source = container.NewColumn<String>("Source");
           user_id = container.NewColumn<Int32>("UserID");
           ok_email = container.NewColumn<String>("OkEmail");
           did = container.NewColumn<String>("DID");
           partner = container.NewColumn<String>("Partner");
           main_group = container.NewColumn<Int32>("MainGroup");
           sub_group = container.NewColumn<Int32>("SubGroup");
           custom_html_element = container.NewColumn<String>("CustomHTMLElement");
           dm_misc = container.NewColumn<String>("DM_Misc");
           unsubscribed = container.NewColumn<DateTime?>("Unsubscribed");
           unsubscribed_campaign_id = container.NewColumn<Int32>("UnsubscribedCampaignID");
           reported_as_spam = container.NewColumn<DateTime?>("ReportedAsSpam");
           reported_as_spam_campaign_id = container.NewColumn<Int32>("ReportedAsSpamCampaignID");
           action_log = container.NewColumn<String>("ActionLog");
       }

        public qCom_Contact(Int32 contact_id)
            : this()
        {
            container.Select("ContactID = @ContactID", new SqlQueryParameter("@ContactID", contact_id));
        }

        public qCom_Contact(String email)
            : this()
        {
            container.Select("Email = @Email", new SqlQueryParameter("@Email", email));
        }

        public void Update()
        {
            container.Update("ContactID = @ContactID");
        }

        public void Insert()
        {
            ContactID = Convert.ToInt32(container.Insert());
        }

        public void DeleteSingleContact(string email)
        {
            string sql = "Email = '" + email + "'";
            container.Delete(sql);
        }

        public static void DeleteContact(string email)
        {
            //schema.container.Delete(string.Concat("Email = ", email));
            string sql = "DELETE FROM qCom_Contacts WHERE Email = '"+ email + "'";
            SqlQuery.execute_sql(sql);
        }

        public static ICollection<qCom_Contact> GetContacts()
        {
            return schema.container.Select<qCom_Contact>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0"
                }, c => new qCom_Contact(c));
        }

        public static ICollection<qCom_Contact> GetActiveContacts()
        {
            return schema.container.Select<qCom_Contact>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND OkEmail = 'Yes'"
                }, c => new qCom_Contact(c));
        }

        public static ICollection<qCom_Contact> GetActiveContactsNoUserIDs()
        {
            return schema.container.Select<qCom_Contact>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND OkEmail = 'Yes' AND (UserID Is null OR UserID = 0)"
                }, c => new qCom_Contact(c));
        }

        public static ICollection<qCom_Contact> GetContactsByKeyword(String keywords)
        {
            return schema.container.Select<qCom_Contact>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND Keywords LIKE %@Keywords%",
                    Parameters = new[] {
                        new SqlQueryParameter ("@Keywords", keywords)
                    },
                    OrderBy = "Email ASC"
                }, c => new qCom_Contact(c));
        }

        public static qCom_Contact GetContactByEmail(String email)
        {
            return schema.container.SelectSingle<qCom_Contact>(
                new DbQuery
                {
                    Where = "Email = @Email",
                    Parameters = new[] {
                        new SqlQueryParameter ("@Email", email)
                    }
                }, c => new qCom_Contact(c));
        }

        public static DataTable GetAllActiveContacts()
        {
            DataTable dt = SqlQuery.execute_sql(string.Format("SELECT * FROM qCom_Contacts WHERE Active = 'Yes' AND MarkAsDelete = 0 AND OkEmail = 'Yes'"));

            if (dt.Rows.Count == 0) return null;

            return dt;
        }

        public static bool DoesContactExist(string email)
        {
            bool contact_exists = false;

            qCom_Contact contact = new qCom_Contact();
            contact = GetContactByEmail(email);

            if (contact != null)
            {
                if (contact.ContactID > 0)
                    contact_exists = true;
            }
            return contact_exists;
        }
    }
}
