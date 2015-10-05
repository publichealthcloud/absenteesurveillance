using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Core
{
    public class FreeTextQueryBuilder
    {
        public SqlQueryParameter[] SqlQueryParameters { get; protected set; }
        public string FreeTextQuery { get; protected set; }

        public FreeTextQueryBuilder(DbRow container, IList<string> queries)
        {
            BuildFreeTextQuery(string.Format("{0}.*", container.GetContainerName ()), queries);
        }

        public FreeTextQueryBuilder(IEnumerable<IDbColumn> columns, IList<string> queries)
        {
            StringBuilder sb_columns = new StringBuilder();

            foreach (var c in columns)
            {
                if (sb_columns.Length > 0) sb_columns.Append(',');
                sb_columns.Append(c.GetColumnName());
            }

            BuildFreeTextQuery(sb_columns.ToString(), queries);
        }

        private void BuildFreeTextQuery(string column_list, IList<string> queries)
        {
            StringBuilder sb_free_predicate = new StringBuilder();

            List<SqlQueryParameter> sql_query_parameters_list = new List<SqlQueryParameter>();

            for (int i = 0; i < queries.Count; i++)
            {
                if (sb_free_predicate.Length > 0) sb_free_predicate.Append(" OR ");

                sb_free_predicate.AppendFormat("FREETEXT (({0}), @Query{1})", column_list, i);

                sql_query_parameters_list.Add(new SqlQueryParameter(string.Format("@Query{0}", i), queries[i]));
            }

            SqlQueryParameters = sql_query_parameters_list.ToArray();
            FreeTextQuery = sb_free_predicate.ToString();
        }
    }

    public class ContentSearchParameters
    {
        public int UserID { get; set; }
        public IList<string> FreeText { get; set; }
    }
}
