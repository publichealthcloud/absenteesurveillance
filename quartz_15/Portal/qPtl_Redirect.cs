using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Quartz.Portal
{
    public class qPtl_Redirect : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(System.Web.HttpApplication Appl)
        {
            Appl.BeginRequest += new System.EventHandler(PerformRedirect_BeginRequest);
        }

        public void PerformRedirect_BeginRequest(object sender, System.EventArgs args)
        {
            // see if current URL matches any redirects
            System.Web.HttpApplication application = (System.Web.HttpApplication)sender;
            string url = application.Request.Path;                                          //get the url path -- want to grab the "/folder" from the URL and compare this to

            char last = url[url.Length - 1];                                              // make sure ends is "/"
            if (Convert.ToString(last) != "/")
            {
                url += "/";
            }

            qPtl_Redirect redirect = new qPtl_Redirect(url);

            if (redirect.RedirectID > 0)
            {
                if (!String.IsNullOrEmpty(redirect.RedirectURL))
                {
                    // add log
                    int curr_user_id = 0;
                    string email = application.Context.Request.QueryString["email"];
                    string campaign = application.Context.Request.QueryString["campaign"];
                    int curr_campaign_id = 0;

                    // try and find a user with this email address
                    if (!String.IsNullOrEmpty(email))
                    {
                        qPtl_User user = new qPtl_User(email);

                        if (user.UserID > 0)
                            curr_user_id = user.UserID;
                    }

                    qPtl_LogAction logAction = new qPtl_LogAction("View");
                    qPtl_Log log = new qPtl_Log();
                    log.ActorID = curr_user_id;
                    log.Created = DateTime.Now;
                    log.CreatedBy = curr_user_id;
                    log.LastModified = DateTime.Now;
                    log.LastModifiedBy = curr_user_id;
                    log.ScopeID = 1;
                    log.LogActionID = logAction.LogActionID;
                    log.CampaignID = curr_campaign_id;
                    log.ReferenceID = redirect.RedirectID;
                    log.ReferenceData = "Email=" + email + ", Entry URL=" + redirect.EntryURL + ", RedirectURL=" + redirect.RedirectURL + ", CampaignID=" + campaign;
                    log.IPAddress = LogUtilities.GetIPAddress();
                    log.Insert();

                    // redirect to URL
                    (sender as HttpApplication).Response.Redirect(redirect.RedirectURL, true);
                }
            }
        }

        protected static qPtl_Redirect schema = new qPtl_Redirect();

        protected DbRow container;
        protected readonly DbColumn<Int32> redirect_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> entry_url;
        protected readonly DbColumn<String> redirect_url;

        public Int32 RedirectID { get { return redirect_id.Value; } set { redirect_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String EntryURL { get { return entry_url.Value; } set { entry_url.Value = value; } }
        public String RedirectURL { get { return redirect_url.Value; } set { redirect_url.Value = value; } }

        public qPtl_Redirect()
            : this(new DbRow())
        {
        }

        protected qPtl_Redirect(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Redirects");
            redirect_id = container.NewColumn<Int32>("RedirectID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            entry_url = container.NewColumn<String>("EntryURL");
            redirect_url = container.NewColumn<String>("RedirectURL");
        }

        public qPtl_Redirect(Int32 redirect_id)
            : this()
        {
            container.Select("RedirectID = @RedirectID", new SqlQueryParameter("@RedirectID", redirect_id));
        }

        public qPtl_Redirect(String entry_url)
            : this()
        {
            container.Select("EntryURL = @EntryURL", new SqlQueryParameter("@EntryURL", entry_url));
        }

        public qPtl_Redirect(String entry_url, Int32 scope_id)
            : this()
        {
            container.Select("EntryURL = @EntryURL AND ScopeID = @ScopeID", new SqlQueryParameter[] { new SqlQueryParameter("@EntryURL", entry_url), new SqlQueryParameter("@ScopeID", scope_id) });
        }

        public void Update(Int32 scope_id, String available, DateTime created, Int32 created_by, DateTime last_modified, Int32 last_modified_by, Int32 mark_as_delete, String entry_url, String redirect_url)
        {
            ScopeID = scope_id;
            Available = available;
            Created = created;
            CreatedBy = created_by;
            LastModified = last_modified;
            LastModifiedBy = last_modified_by;
            MarkAsDelete = mark_as_delete;
            EntryURL = entry_url;
            RedirectURL = redirect_url;

            container.Update("RedirectID = @RedirectID");
        }

        public void Update()
        {
            LastModified = DateTime.Now;
            container.Update("RedirectID = @RedirectID");
        }

        public void Insert(Int32 scope_id, String available, DateTime created, Int32 created_by, DateTime last_modified, Int32 last_modified_by, Int32 mark_as_delete, String entry_url, String redirect_url)
        {
            ScopeID = scope_id;
            Available = available;
            Created = created;
            CreatedBy = created_by;
            LastModified = last_modified;
            LastModifiedBy = last_modified_by;
            MarkAsDelete = mark_as_delete;
            EntryURL = entry_url;
            RedirectURL = redirect_url;

            RedirectID = Convert.ToInt32(container.Insert());
        }

        public void Insert()
        {
            Created = DateTime.Now;
            Available = "Yes";
            MarkAsDelete = 0;
            RedirectID = Convert.ToInt32(container.Insert());
        }

        public void DeleteRedirect(int redirect_id)
        {
            container.Delete(string.Format(string.Format("RedirectID = {0}", redirect_id)));
        }
    }
}
