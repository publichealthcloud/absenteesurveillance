using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_UserLevelRedirect
    {
        protected static qPtl_UserLevelRedirect schema = new qPtl_UserLevelRedirect();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_level_redirect_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<Int32> highest_rank;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<String> request_url;
        protected readonly DbColumn<String> url;

        public Int32 UserLevelRedirectID { get { return user_level_redirect_id.Value; } set { user_level_redirect_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public Int32 HighestRank { get { return highest_rank.Value; } set { highest_rank.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public String RequestUrl { get { return request_url.Value; } set { request_url.Value = value; } }
        public String URL { get { return url.Value; } set { url.Value = value; } }

        public qPtl_UserLevelRedirect()
            : this(new DbRow())
        {
        }

        protected qPtl_UserLevelRedirect(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserLevelRedirects");
            user_level_redirect_id = container.NewColumn<Int32>("UserLevelRedirectID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            highest_rank = container.NewColumn<Int32>("HighestRank");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            request_url = container.NewColumn<String>("RequestUrl");
            url = container.NewColumn<String>("URL");
        }

        public qPtl_UserLevelRedirect(Int32 user_level_redirect_id)
            : this()
        {
            container.Select("UserLevelRedirectID = @UserLevelRedirectID", new SqlQueryParameter("@UserLevelRedirectID", user_level_redirect_id));
        }

        public qPtl_UserLevelRedirect(Int32 scope_id, Int32 highest_rank, Int32 content_type_id)
            : this()
        {
            container.Select("ScopeID = @ScopeID AND HighestRank = @HighestRank AND ContentTypeID = @ContentTypeID", new SqlQueryParameter[] { new SqlQueryParameter("@ScopeID", scope_id), new SqlQueryParameter("@HighestRank", highest_rank), new SqlQueryParameter("@ContentTypeID", content_type_id) });
        }

        public qPtl_UserLevelRedirect(Int32 scope_id, Int32 highest_rank, Int32 content_type_id, String request_url)
            : this()
        {
            container.Select("ScopeID = @ScopeID AND HighestRank = @HighestRank AND ContentTypeID = @ContentTypeID AND RequestUrl = @RequestUrl", new SqlQueryParameter[] { new SqlQueryParameter("@ScopeID", scope_id), new SqlQueryParameter("@HighestRank", highest_rank), new SqlQueryParameter("@ContentTypeID", content_type_id), new SqlQueryParameter("@RequestUrl", request_url) });

            if (user_level_redirect_id.Value == 0)
                container.Select("ScopeID = @ScopeID AND HighestRank = @HighestRank AND ContentTypeID = @ContentTypeID AND RequestUrl = ''", new SqlQueryParameter[] { new SqlQueryParameter("@ScopeID", scope_id), new SqlQueryParameter("@HighestRank", highest_rank), new SqlQueryParameter("@ContentTypeID", content_type_id) });
        }

        public void Update(Int32 scope_id, Int32 highest_rank, Int32 content_type_id, String request_url, String url)
        {
            ScopeID = scope_id;
            HighestRank = highest_rank;
            ContentTypeID = content_type_id;
            RequestUrl = request_url;
            URL = url;
            
            container.Update("UserLevelRedirectID = @UserLevelRedirectID");
        }

        public void Insert(Int32 scope_id, Int32 highest_rank, Int32 content_type_id, String request_url, String url)
        {
            ScopeID = scope_id;
            HighestRank = highest_rank;
            ContentTypeID = content_type_id;
            RequestUrl = request_url;
            URL = url;

            UserLevelRedirectID = Convert.ToInt32(container.Insert());
        }
    }
}
