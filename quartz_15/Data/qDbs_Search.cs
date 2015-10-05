using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text;

namespace Quartz.Data
{
    public class qDbs_Search
    {
        private DbRow container;

        public readonly static qDbs_Search Schema = new qDbs_Search();

        private readonly DbColumn<int> search_id;
        private readonly DbColumn<int> search_group_id;
        private readonly DbColumn<int> user_id;
        private readonly DbColumn<DateTime> created;
        private readonly DbColumn<int> data_group_id;
        private readonly DbColumn<string> custom_columns;
        private readonly DbColumn<string> sql_pre_select_declarations;
        private readonly DbColumn<string> sql_select;
        private readonly DbColumn<string> sql_from;
        private readonly DbColumn<string> sql_where;
        private readonly DbColumn<string> saved;
        private readonly DbColumn<string> saved_name;
        private readonly DbColumn<string> saved_description;
        private readonly DbColumn<int> scope_id;
        private readonly DbColumn<string> yes_email;

        public int SearchID { get { return search_id.Value; } set { search_id.Value = value; } }
        public int SearchGroupID { get { return search_group_id.Value; } set { search_group_id.Value = value; } }
        public int UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public int DataGroupID { get { return data_group_id.Value; } set { data_group_id.Value = value; } }
        public string CustomColumns { get { return custom_columns.Value; } set { custom_columns.Value = value; } }
        public string SqlPreSelectDeclarations { get { return sql_pre_select_declarations.Value; } set { sql_pre_select_declarations.Value = value; } }
        public string SqlSelect { get { return sql_select.Value; } set { sql_select.Value = value; } }
        public string SqlFrom { get { return sql_from.Value; } set { sql_from.Value = value; } }
        public string SqlWhere { get { return sql_where.Value; } set { sql_where.Value = value; } }
        public string Saved { get { return saved.Value; } set { saved.Value = value; } }
        public string SavedName { get { return saved_name.Value; } set { saved_name.Value = value; } }
        public string SavedDescription { get { return saved_description.Value; } set { saved_description.Value = value; } }
        public int ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public string YesEmail { get { return yes_email.Value; } set { yes_email.Value = value; } }

        public qDbs_Search()
            : this(new DbRow())
        {
        }

        public qDbs_Search(DbRow existing_container)
        {
            container = existing_container;

            container.SetContainerName ("qDbs_Searches");

            search_id = container.NewColumn<int>("SearchID", true);
            search_group_id = container.NewColumn<int>("SearchGroupID");
            user_id = container.NewColumn<int>("UserID");
            created = container.NewColumn<DateTime>("Created");
            data_group_id = container.NewColumn<int>("DataGroupID");
            custom_columns = container.NewColumn<string>("CustomColumns");
            sql_pre_select_declarations = container.NewColumn<string>("sqlPreSelectDeclarations");
            sql_select = container.NewColumn<string>("sqlSELECT");
            sql_from = container.NewColumn<string>("sqlFROM");
            sql_where = container.NewColumn<string>("sqlWHERE");
            saved = container.NewColumn<string>("Saved");
            saved_name = container.NewColumn<string>("SavedName");
            saved_description = container.NewColumn<string>("SavedDescription");
            scope_id = container.NewColumn<int>("ScopeID");
            yes_email = container.NewColumn<string>("YesEmail");
        }

        public qDbs_Search(int search_id)
            : this ()
        {
            container.Select("SearchID = @SearchID", new SqlQueryParameter("@SearchID", search_id));
        }

        public DbRow [] GetResults (DbRow schema, string join_clause, string filter, params SqlQueryParameter [] parameters)
        {
            if (!string.IsNullOrEmpty(filter)) filter = string.Format("AND ({0})", filter);

            string sql = string.Format("{0}; SELECT {1} FROM {2} {3} WHERE {4} {5}",
                                         SqlPreSelectDeclarations,
                                         SqlSelect,
                                         SqlFrom,
                                         join_clause,
                                         SqlWhere,
                                         filter);

            return DbRow.Select(schema, sql, parameters);
        }

        public static ICollection<qDbs_Search> GetSearches(int data_group_id)
        {
            return Schema.container.Select<qDbs_Search>(string.Format("DataGroupID = {0} AND SavedName IS NOT NULL AND SavedName <> ''", data_group_id), c => new qDbs_Search(c), null);
        }
    }
}