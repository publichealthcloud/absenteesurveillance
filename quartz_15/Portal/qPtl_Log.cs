using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for qPtl_Log
/// </summary>
///
namespace Quartz.Portal
{
    public class qPtl_Log
    {
	    protected static qPtl_Log schema = new qPtl_Log ();

	    protected DbRow container;
	    protected readonly DbColumn <Int32> log_id;
	    protected readonly DbColumn <Int32> actor_id;
	    protected readonly DbColumn <Int32> scope_id;
	    protected readonly DbColumn <String> available;
	    protected readonly DbColumn <DateTime?> created;
	    protected readonly DbColumn <Int32> created_by;
	    protected readonly DbColumn <DateTime?> last_modified;
	    protected readonly DbColumn <Int32> last_modified_by;
	    protected readonly DbColumn <Int32> mark_as_delete;
	    protected readonly DbColumn <Int32> log_action_id;
        protected readonly DbColumn<Int32> campaign_id;
        protected readonly DbColumn<Int32> campaign_action_id;
	    protected readonly DbColumn <Int32> content_type_id;
	    protected readonly DbColumn <Int32> reference_id;
        protected readonly DbColumn<String> reference_data;
        protected readonly DbColumn<String> ip_address;

	    public Int32 LogID { get { return log_id.Value; } set { log_id.Value = value; } }
	    public Int32 ActorID { get { return actor_id.Value; } set { actor_id.Value = value; } }
	    public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
	    public String Available { get { return available.Value; } set { available.Value = value; } }
	    public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
	    public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
	    public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
	    public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
	    public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
	    public Int32 LogActionID { get { return log_action_id.Value; } set { log_action_id.Value = value; } }
        public Int32 CampaignID { get { return campaign_id.Value; } set { campaign_id.Value = value; } }
        public Int32 CampaignActionID { get { return campaign_action_id.Value; } set { campaign_action_id.Value = value; } }
	    public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
	    public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public String ReferenceData { get { return reference_data.Value; } set { reference_data.Value = value; } }
        public String IPAddress { get { return ip_address.Value; } set { ip_address.Value = value; } }

	    public qPtl_Log ()
		    : this (new DbRow ())
	    {
	    }

        public qPtl_Log(Int32 log_id)
		    : this () 
	    {
            container.Select("LogID = @LogID", new SqlQueryParameter("@LogID", log_id));
	    }

        protected qPtl_Log(DbRow c)
	    {
		    container = c;
		    container.SetContainerName ("qPtl_Logs");
		    log_id = container.NewColumn <Int32> ("LogID", true);
		    actor_id = container.NewColumn <Int32> ("ActorID");
		    scope_id = container.NewColumn <Int32> ("ScopeID");
		    available = container.NewColumn <String> ("Available");
		    created = container.NewColumn <DateTime?> ("Created");
		    created_by = container.NewColumn <Int32> ("CreatedBy");
		    last_modified = container.NewColumn <DateTime?> ("LastModified");
		    last_modified_by = container.NewColumn <Int32> ("LastModifiedBy");
		    mark_as_delete = container.NewColumn <Int32> ("MarkAsDelete");
		    log_action_id = container.NewColumn <Int32> ("LogActionID");
            campaign_id = container.NewColumn<Int32>("CampaignID");
            campaign_action_id = container.NewColumn<Int32>("CampaignActionID");
		    content_type_id = container.NewColumn <Int32> ("ContentTypeID");
		    reference_id = container.NewColumn <Int32> ("ReferenceID");
            reference_data = container.NewColumn<String>("ReferenceData");
            ip_address = container.NewColumn<String>("IPAddress");
	    }

        public void Update(Int32 log_id, Int32 actor_id, Int32 scope_id, String available, DateTime? created, Int32 created_by, DateTime? last_modified, Int32 last_modified_by, Int32 mark_as_delete, Int32 log_action_id, Int32 content_type_id, Int32 reference_id, String reference_data)
	    {
		    LogID = log_id;
		    ActorID = actor_id;
		    ScopeID = scope_id;
		    Available = available;
		    Created = created;
		    CreatedBy = created_by;
		    LastModified = last_modified;
		    LastModifiedBy = last_modified_by;
		    MarkAsDelete = mark_as_delete;
		    LogActionID = log_action_id;
		    ContentTypeID = content_type_id;
		    ReferenceID = reference_id;
            ReferenceData = reference_data;

		    container.Update ("LogID = @LogID");
	    }

        public void Update()
        {
            container.Update("LogID = @LogID");
        }

	    public void Insert (Int32 actor_id, Int32 scope_id, String available, DateTime? created, Int32 created_by, DateTime? last_modified, Int32 last_modified_by, Int32 mark_as_delete, Int32 log_action_id, Int32 content_type_id, Int32 reference_id)
	    {
		    ActorID = actor_id;
		    ScopeID = scope_id;
		    Available = available;
		    Created = created;
		    CreatedBy = created_by;
		    LastModified = last_modified;
		    LastModifiedBy = last_modified_by;
		    MarkAsDelete = mark_as_delete;
		    LogActionID = log_action_id;
		    ContentTypeID = content_type_id;
		    ReferenceID = reference_id;
            IPAddress = LogUtilities.GetIPAddress();

            LogID = Convert.ToInt32(container.Insert());
	    }

        public void Insert()
        {
            LogID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_Log> GetLogs(int actor_id, int content_type_id, int reference_id)
        {
            return schema.container.Select<qPtl_Log>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND ActorID = @ActorID AND ContentTypeID = @ContentTypeID AND ReferenceID = @ReferenceID",
                    Parameters = new[] { 
                        new SqlQueryParameter ("@ActorID", actor_id),
                        new SqlQueryParameter ("@ContentTypeID", content_type_id),
                        new SqlQueryParameter ("@ReferenceID", reference_id)
                    }
                }, c => new qPtl_Log(c));
        }
    }

    public class qPtl_Logs_View
    {
        protected static qPtl_Logs_View schema = new qPtl_Logs_View();

        protected DbRow container;
	    protected readonly DbColumn <Int32> log_id;
	    protected readonly DbColumn <Int32> actor_id;
	    protected readonly DbColumn <Int32> scope_id;
	    protected readonly DbColumn <String> available;
	    protected readonly DbColumn <DateTime?> created;
	    protected readonly DbColumn <Int32> created_by;
	    protected readonly DbColumn <DateTime?> last_modified;
	    protected readonly DbColumn <Int32> last_modified_by;
	    protected readonly DbColumn <Int32> mark_as_delete;
	    protected readonly DbColumn <Int32> log_action_id;
        protected readonly DbColumn<Int32> campaign_id;
        protected readonly DbColumn<Int32> campaign_action_id;
	    protected readonly DbColumn <Int32> content_type_id;
	    protected readonly DbColumn <Int32> reference_id;
        protected readonly DbColumn<String> reference_data;
        protected readonly DbColumn<String> action_name;
        protected readonly DbColumn<String> name;
        protected readonly DbColumn<String> url;
        protected readonly DbColumn<String> preview_url;
        protected readonly DbColumn<String> preview_qs;
        protected readonly DbColumn<String> action_type;
        protected readonly DbColumn<String> campaign_action_name;
        protected readonly DbColumn<String> ip_address;

	    public Int32 LogID { get { return log_id.Value; } set { log_id.Value = value; } }
	    public Int32 ActorID { get { return actor_id.Value; } set { actor_id.Value = value; } }
	    public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
	    public String Available { get { return available.Value; } set { available.Value = value; } }
	    public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
	    public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
	    public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
	    public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
	    public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
	    public Int32 LogActionID { get { return log_action_id.Value; } set { log_action_id.Value = value; } }
        public Int32 CampaignID { get { return campaign_id.Value; } set { campaign_id.Value = value; } }
        public Int32 CampaignActionID { get { return campaign_action_id.Value; } set { campaign_action_id.Value = value; } }
	    public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
	    public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public String ReferenceData { get { return reference_data.Value; } set { reference_data.Value = value; } }
        public String ActionName { get { return action_name.Value; } set { action_name.Value = value; } }
        public String Name { get { return name.Value; } set { name.Value = value; } }
        public String URL { get { return url.Value; } set { url.Value = value; } }
        public String PreviewURL { get { return preview_url.Value; } set { preview_url.Value = value; } }
        public String PreviewQS { get { return preview_qs.Value; } set { preview_qs.Value = value; } }
        public String ActionType { get { return action_type.Value; } set { action_type.Value = value; } }
        public String CampaignActionName { get { return campaign_action_name.Value; } set { campaign_action_name.Value = value; } }
        public String IPAddress { get { return ip_address.Value; } set { ip_address.Value = value; } }

        public qPtl_Logs_View()
            : this(new DbRow())
        {
        }

        protected qPtl_Logs_View(DbRow c)
        {
            container = c;
		    container.SetContainerName ("qPtl_Logs_View");
		    log_id = container.NewColumn <Int32> ("LogID", true);
		    actor_id = container.NewColumn <Int32> ("ActorID");
		    scope_id = container.NewColumn <Int32> ("ScopeID");
		    available = container.NewColumn <String> ("Available");
		    created = container.NewColumn <DateTime?> ("Created");
		    created_by = container.NewColumn <Int32> ("CreatedBy");
		    last_modified = container.NewColumn <DateTime?> ("LastModified");
		    last_modified_by = container.NewColumn <Int32> ("LastModifiedBy");
		    mark_as_delete = container.NewColumn <Int32> ("MarkAsDelete");
		    log_action_id = container.NewColumn <Int32> ("LogActionID");
            campaign_id = container.NewColumn<Int32>("CampaignID");
            campaign_action_id = container.NewColumn<Int32>("CampaignActionID");
		    content_type_id = container.NewColumn <Int32> ("ContentTypeID");
		    reference_id = container.NewColumn <Int32> ("ReferenceID");
            reference_data = container.NewColumn<String>("ReferenceData");
            action_name = container.NewColumn<String>("ActionName");
            name = container.NewColumn<String>("Name");
            url = container.NewColumn<String>("URL");
            preview_url = container.NewColumn<String>("PreviewURL");
            preview_qs = container.NewColumn<String>("PreviewQS");
            action_type = container.NewColumn<String>("ActionType"); 
            campaign_action_name = container.NewColumn<String>("CampaignActionName");
            ip_address = container.NewColumn<String>("IPAddress");
        }

        public qPtl_Logs_View(Int32 log_id)
            : this()
        {
            container.Select("LogID = @logID", new SqlQueryParameter("@LogID", log_id));
        }

        public static ICollection<qPtl_Logs_View> GetUserLogs(int user_id)
        {
            return schema.container.Select<qPtl_Logs_View>(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                },
                c => new qPtl_Logs_View(c));
        }

        public static ICollection<qPtl_Logs_View> GetUserLogsByAction(int user_id, int log_action_id)
        {
            return schema.container.Select<qPtl_Logs_View>(
                new DbQuery
                {
                    Where = "UserID = @UserID AND LogActionID = @LogActionID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@LogActionID", log_action_id) }
                },
                c => new qPtl_Logs_View(c));
        }

        public static DataTable GetLogsByFilter(int num_returned, string order_by, int user_id)
        {
            string sql = string.Empty;
            if (num_returned > 0)
                sql = "SELECT TOP(" + num_returned + ")* FROM qPtl_Logs_View WHERE Available = 'Yes' AND MarkAsDelete = 0";
            else
                sql = "SELECT * FROM qPtl_Logs_View WHERE Available = 'Yes' AND MarkAsDelete = 0";

            if (user_id > 0)
                sql += " AND UserID = " + user_id;
            
            if (!String.IsNullOrEmpty(order_by))
            {
                switch (order_by)
                {
                    case "alpha":
                        sql += " ORDER BY Title ASC";
                        break;
                    case "newest-to-oldest":
                        sql += " ORDER BY Created DESC";
                        break;
                    case "oldest-to-newest":
                        sql += " ORDER BY Created ASC";
                        break;
                    default:
                        sql += " ORDER BY Title ASC";
                        break;
                }
            }

            return SqlQuery.execute_sql(sql);
        }
    }

    public class qPtl_LogAction
    {
        protected static qPtl_LogAction schema = new qPtl_LogAction();

        protected DbRow container;
        protected readonly DbColumn<Int32> log_action_id;
        protected readonly DbColumn<String> action_name;

        public Int32 LogActionID { get { return log_action_id.Value; } set { log_action_id.Value = value; } }
        public String ActionName { get { return action_name.Value; } set { action_name.Value = value; } }

        public qPtl_LogAction()
            : this(new DbRow())
        {
        }

        protected qPtl_LogAction(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_LogActions");
            log_action_id = container.NewColumn<Int32>("LogActionID", true);
            action_name = container.NewColumn<String>("ActionName");
        }

        public qPtl_LogAction(Int32 log_action_id)
            : this()
        {
            container.Select("LogActionID = @LogActionID", new SqlQueryParameter("@LogActionID", log_action_id));
        }

        public qPtl_LogAction(String action_name)
            : this()
        {
            container.Select("ActionName = @ActionName", new SqlQueryParameter("@ActionName", action_name));
        }

        public void Update(String action_name)
        {
            ActionName = action_name;

            container.Update("LogActionID = @LogActionID");
        }

        public void Insert(String action_name)
        {
            ActionName = action_name;

            LogActionID = Convert.ToInt32(container.Insert());
        }
    }

    public class LogUtilities
    {
        public static string GetIPAddress()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }

            return VisitorsIPAddr;
        }
    }
}