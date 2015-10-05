using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Help
{
    public class qHlp_HelpTopic
    {
       protected static qHlp_HelpTopic schema = new qHlp_HelpTopic();

        protected DbRow container;
        protected readonly DbColumn<Int32> help_topic_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> summary;
        protected readonly DbColumn<String> keywords;
        protected readonly DbColumn<String> body;
        protected readonly DbColumn<Double> topic_order;
        protected readonly DbColumn<Int32> parent_topic_id;
        protected readonly DbColumn<Boolean> is_category;
        protected readonly DbColumn<Boolean> is_system_help;

        public Int32 HelpTopicID { get { return help_topic_id.Value; } set { help_topic_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String Title { get { return title.Value; } set { title.Value = value; } }
        public String Summary { get { return summary.Value; } set { summary.Value = value; } }
        public String Keywords { get { return keywords.Value; } set { keywords.Value = value; } }
        public String Body { get { return body.Value; } set { body.Value = value; } }
        public Double TopicOrder { get { return topic_order.Value; } set { topic_order.Value = value; } }
        public Int32 ParentTopicID { get { return parent_topic_id.Value; } set { parent_topic_id.Value = value; } }
        public Boolean IsCategory { get { return is_category.Value; } set { is_category.Value = value; } }
        public Boolean IsSystemHelp { get { return is_system_help.Value; } set { is_system_help.Value = value; } }

        public qHlp_HelpTopic()
            : this(new DbRow())
        {
        }

        protected qHlp_HelpTopic(DbRow c)
        {
            container = c;
            container.SetContainerName("qHlp_HelpTopics");
            help_topic_id = container.NewColumn<Int32>("HelpTopicID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            title = container.NewColumn<String>("Title");
            summary = container.NewColumn<String>("Summary");
            keywords = container.NewColumn<String>("Keywords");
            body = container.NewColumn<String>("Body");
            topic_order = container.NewColumn<Double>("TopicOrder");
            parent_topic_id = container.NewColumn<Int32>("ParentTopicID");
            is_category = container.NewColumn<Boolean>("IsCategory");
            is_system_help = container.NewColumn<Boolean>("IsSystemHelp");
        }

        public qHlp_HelpTopic(Int32 help_topic_id)
            : this()
        {
            container.Select("HelpTopicID = @HelpTopicID", new SqlQueryParameter("@HelpTopicID", help_topic_id));
        }

        public void Update()
        {
            container.Update("HelpTopicID = @HelpTopicID");
        }

        public void Insert()
        {
            HelpTopicID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qHlp_HelpTopic> GetHelpTopics()
        {
            return schema.container.Select<qHlp_HelpTopic>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "TopicOrder ASC",
                }, c => new qHlp_HelpTopic(c));
        }

        public static ICollection<qHlp_HelpTopic> GetHelpCategories()
        {
            return schema.container.Select<qHlp_HelpTopic>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND IsCategory = 'true'",
                    OrderBy = "TopicOrder ASC",
                }, c => new qHlp_HelpTopic(c));
        }


        public static ICollection<qHlp_HelpTopic> GetHelpTopicsByKeyword(string keywords)
        {
            return schema.container.Select<qHlp_HelpTopic>(
                new DbQuery
                {
                    Where = string.Format("Available = 'Yes' AND MarkAsDelete = 0 AND (Keywords LIKE '%{0}%' OR Title LIKE '%{0}%')", keywords),
                    OrderBy = "TopicOrder ASC",
                }, 
                c => new qHlp_HelpTopic(c));
        }

        public static qHlp_HelpTopic GetHelpTopicByTitle(string title)
        {
            var topic = new qHlp_HelpTopic();

            topic.container.Select(new DbQuery
            {
                Where = "Available = 'Yes' AND MarkAsDelete = 0 AND Title = @Title",
                Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@Title", title) }
            });

            return topic;
        }

        public static double GetLastTopicOrderInCategory(int parent_topic_id)
        {
            return Convert.ToDouble(SqlQuery.execute_sql_scalar(
                "SELECT TOP(1) TopicOrder FROM qHlp_HelpTopics WHERE ParentTopicID = @ParentTopicID ORDER BY TopicOrder DESC",
                CommandType.Text,
                new SqlQueryParameter[] { new SqlQueryParameter("@ParentTopicID", parent_topic_id) }));
        }
    }
}
