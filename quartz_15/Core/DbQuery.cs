using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz
{
    public class DbQuery
    {
        public string Sql { get; set; }
        public string Top { get; set; }
        public string Columns { get; set; }
        public string From { get; set; }
        public string Where { get; set; }
        public string OrderBy { get; set; }
        public string GroupBy { get; set; }
        public DbJoin Join { get; set; }
        public string Alias { get; set; }
        public string ContainerAlias { get; set; }
        public int StartingRow { get; set; }
        public int EndingRow { get; set; }
        public SqlQueryParameter [] Parameters { get; set; }

        public class DbJoin
        {
            public string Type { get; set; }
            public string Condition { get; set; }
            public string Container { get; set; }
            public DbQuery Query { get; set; }
            public DbJoin ChainedJoin { get; set; }

            public DbJoin (string type, DbQuery query, string condition)
            {
                Type = type;
                Query = query;
                Condition = condition;
            }

            public DbJoin(string type, string container, string condition)
            {
                Type = type;
                Container = container;
                Condition = condition;
            }

            public void Join(DbJoin join)
            {
                if (ChainedJoin == null) ChainedJoin = join;
                else ChainedJoin.Join(join);
            }
        }
    }

    public class DbQueryToSql
    {
        public string Query { get; private set; }
        public SqlQueryParameter [] Parameters { get; private set; }

        public DbQueryToSql(DbRow container, DbQuery query)
        {
            BuildSqlQuery(container, query);
        }

        private void BuildSqlQuery (DbRow container, DbQuery query)
        {
            if (string.IsNullOrEmpty(query.Sql))
            {
                string select_columns = query.Columns;
                string where = query.Where;
                string from = query.From;
                string container_alias = query.ContainerAlias;
                string group_by = query.GroupBy;
                string order_by = query.OrderBy;
                string top = query.Top;                

                int starting_row = query.StartingRow;
                int ending_row = query.EndingRow;

                if (string.IsNullOrEmpty(select_columns)) select_columns = DbRow.GetSelectColumns(container, container_alias);
                if (string.IsNullOrEmpty(from)) from = container.GetContainerName();

                if (!string.IsNullOrEmpty(container_alias)) from = string.Format("{0} {1}", from, container_alias);

                if (!string.IsNullOrEmpty(where)) where = string.Format("WHERE {0}", where);
                if (!string.IsNullOrEmpty(group_by)) group_by = string.Format("GROUP BY {0}", group_by);
                if (!string.IsNullOrEmpty(order_by)) order_by = string.Format("ORDER BY {0}", order_by);                

                List<SqlQueryParameter> sql_parameters = new List<SqlQueryParameter>();

                if (query.Parameters != null) sql_parameters.AddRange(query.Parameters);

                string join = BuildSqlJoin(container, query.Join, sql_parameters);

                string select_statement;

                if (starting_row != 0 || ending_row != 0)
                {
                    if (starting_row < 1) throw new Exception("Starting row must be greater than 0");
                    if (ending_row < starting_row) throw new Exception("Ending row must be >= the starting row");

                    select_statement = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) AS RowNumber, {1} FROM {2} {3} {4}) AS PagedView WHERE RowNumber BETWEEN {5} AND {6}",
                                              order_by,
                                              select_columns,
                                              from,
                                              join,
                                              where,
                                              starting_row,
                                              ending_row);
                }
                else
                {
                    select_statement = string.Format("SELECT {0} {1} FROM {2} {3} {4} {5} {6}",
                                                         top,
                                                         select_columns,
                                                         from,
                                                         join,
                                                         where,
                                                         group_by,
                                                         order_by);
                }

                if (!string.IsNullOrEmpty(query.Alias)) select_statement = string.Format("({0}) AS {1}", select_statement, query.Alias);

                Query = select_statement;

                if (sql_parameters != null) Parameters = sql_parameters.ToArray();
            }
            else
            {
                Query = query.Sql;
                Parameters = query.Parameters;
            }            
        }

        private static string BuildSqlJoin(DbRow container, DbQuery.DbJoin join, List<SqlQueryParameter> sql_parameters)
        {
            if (join != null)
            {
                StringBuilder sb_join = new StringBuilder();

                if (!string.IsNullOrEmpty(join.Container))
                    sb_join.AppendFormat ("{0} JOIN {1} ON {2}", join.Type, join.Container, join.Condition);
                else
                {
                    var parsed_join = new DbQueryToSql(container, join.Query);

                    sb_join.AppendFormat ("{0} JOIN {1} ON {2}", join.Type, parsed_join.Query, join.Condition);

                    if (parsed_join.Parameters != null)
                    {
                        if (sql_parameters == null) sql_parameters = new List<SqlQueryParameter>();

                        sql_parameters.AddRange(parsed_join.Parameters);
                    }                   
                }
                if (join.ChainedJoin != null) sb_join.Append(' ').Append(BuildSqlJoin(container, join.ChainedJoin, sql_parameters));

                return sb_join.ToString();
            }
            else return null;
        }
    }
}
