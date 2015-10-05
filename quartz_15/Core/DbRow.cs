using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using System.Configuration;

using Quartz.Core;

namespace Quartz
{
    [Serializable]
    public class DbRow
    {    
        [Serializable]
        internal class DbRowValue
        {
            public DbRowValue()
            {
            }

            public DbRowValue(bool is_read_only, bool is_managed, string table_name)
            {
                this.is_read_only = is_read_only;
                this.is_managed = is_managed;
                this.table_name = table_name;
            }

            public DbRowValue(bool is_key, bool is_read_only, bool is_managed, string table_name)
                : this(is_read_only, is_managed, table_name)
            {
                this.is_key = is_key;
            }

            public DbRowValue(bool is_key, bool is_read_only, bool allows_dbnull, bool is_managed, string table_name)
                : this(is_key, is_read_only, is_managed, table_name)
            {
                this.allows_dbnull = allows_dbnull;
            }

            public DbRowValue(DbRowValue copy_schema)
                : this(copy_schema.is_key, copy_schema.is_read_only, copy_schema.allows_dbnull, copy_schema.is_managed, copy_schema.table_name)
            {
            }

            public bool is_key;
            public bool is_read_only;
            public bool allows_dbnull;
            public bool is_managed;
            public bool has_value_changed;
            public string table_name;

            public object default_value;

            public object value;
        }

        private string connection_name;
        private string connection_string;
        private string table_name;
        private bool is_managed;

        private Dictionary<string, DbRowValue> properties;

        public DbRow()
        {
            properties = new Dictionary<string, DbRowValue>();

            connection_string = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public DbRow(string table_name)
            : this()
        {
            this.table_name = table_name;
        }

        public DbRow(string table_name, params string[] keys)
            : this(table_name)
        {
            foreach (string key in keys)
                properties[key] = new DbRowValue(true, true, false, true, table_name);
        }

        public DbRow(DbRow copy_schema)
            : this(copy_schema, copy_schema.table_name)
        {
        }

        public DbRow(DbRow copy_schema, string new_schema_name)
            : this()
        {
            connection_string = copy_schema.connection_string;
            connection_name = copy_schema.connection_name;
            table_name = new_schema_name;
            is_managed = copy_schema.is_managed;

            foreach (string key in copy_schema.properties.Keys)
            {
                properties[key] = new DbRowValue(copy_schema.properties[key]);
            }
        }

        public void SetConnectionName(string connection_name)
        {
            this.connection_name = connection_name;

            connection_string = ConfigurationManager.ConnectionStrings[connection_name].ConnectionString;
        }

        public void SetContainerName(string name)
        {
            table_name = name;
        }

        public string GetContainerName()
        {
            return table_name;
        }

        public DbColumn<FieldType> NewColumn<FieldType>(string name)
        {
            return NewColumn<FieldType>(null, name, false, false, true, true, default(FieldType));
        }

        public DbColumn<FieldType> NewColumn<FieldType>(string name, bool is_read_only)
        {
            return NewColumn<FieldType>(null, name, false, is_read_only, true, true, default(FieldType));
        }

        public DbColumn<FieldType> NewColumn<FieldType>(string name, bool is_read_only, FieldType default_value)
        {
            return NewColumn<FieldType>(null, name, false, is_read_only, true, true, default_value);
        }

        public DbColumn<FieldType> NewColumn<FieldType>(string table_name, string name, bool is_read_only)
        {
            return NewColumn<FieldType>(table_name, name, false, is_read_only, true, true, default(FieldType));
        }

        public DbColumn<FieldType> NewColumn<FieldType>(string table_name, string name, bool is_read_only, bool is_managed)
        {
            return NewColumn<FieldType>(table_name, name, false, is_read_only, is_managed, true, default(FieldType));
        }

        public DbColumn<FieldType> NewColumn<FieldType>(string table_name, string name, bool is_key, bool is_read_only, bool is_managed, bool allows_dbnull, FieldType default_value)
        {
            is_managed = true;

            DbColumn<FieldType> new_field = new DbColumn<FieldType>(this, name, default_value);

            if (!properties.ContainsKey(name))
            {
                properties[name] = new DbRowValue(is_key, is_read_only, allows_dbnull, true, table_name);
                properties[name].default_value = default_value;
            }

            return new_field;
        }

        public void LoadValues(DataRow data_row)
        {
            foreach (DataColumn data_col in data_row.Table.Columns)
            {
                string current_col_name = data_col.ColumnName;

                if (!properties.ContainsKey(current_col_name))
                {
                    properties[current_col_name] = new DbRowValue(is_managed, false, null);
                }

                properties[current_col_name].value = Helper.read_database_value(data_row[current_col_name]);
            }
        }

        public void LoadValues(SqlDataReader reader)
        {
            if (reader.HasRows && reader.Read())
            {
                LoadValuesWithoutRead(reader);
            }
        }

        private void LoadValuesWithoutRead(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string column = reader.GetName(i);

                //field wasn't added via NewColumn, most likely an unmanaged column
                if (!properties.ContainsKey(column))
                {
                    properties[column] = new DbRowValue(is_managed, false, null);
                }

                properties[column].value = Helper.read_database_value(reader.GetValue(i));
            }
        }

        public static DbRow[] Select(DbRow container_schema, string extra_select, int top, string from_clause, string where_clause, string order_clause)
        {
            return Select(container_schema, null, extra_select, null, top, from_clause, where_clause, order_clause, -1, -1);
        }

        public static DbRow[] Select(DbRow container_schema, string container_column_prefix, string extra_select, SqlQueryParameter[] extra_params, int top, string from_clause, string where_clause, string order_clause, int start_row, int end_row)
        {
            bool is_paged = start_row > 0 && end_row > 0;

            StringBuilder sb_columns = new StringBuilder();

            if (top > 0 && !is_paged)
            {
                sb_columns.AppendFormat("TOP {0} ", top);
            }

            if (container_schema != null)
            {
                bool first_time = true;

                foreach (string key in container_schema.properties.Keys)
                {
                    DbRowValue current_field = container_schema.properties[key];

                    if (current_field.is_managed)
                    {
                        if (!first_time) sb_columns.Append(',');
                        else first_time = false;

                        if (!string.IsNullOrEmpty(container_column_prefix))
                        {
                            sb_columns.Append(container_column_prefix);
                            sb_columns.Append('.');
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(current_field.table_name))
                            {
                                sb_columns.Append(current_field.table_name);
                                sb_columns.Append('.');
                            }
                            else if (!string.IsNullOrEmpty(container_schema.table_name))
                            {
                                sb_columns.Append(container_schema.table_name);
                                sb_columns.Append('.');
                            }

                            sb_columns.Append(key);
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(extra_select))
            {
                if (sb_columns.Length > 0) sb_columns.Append(',');
                sb_columns.Append(extra_select);
            }

            if (!string.IsNullOrEmpty(order_clause))
            {
                order_clause = string.Format("ORDER BY {0}", order_clause);
            }

            string select_statement;

            if (is_paged)
            {
                select_statement = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) AS RowNumber, {1} FROM {2} WHERE {3}) AS PagedView WHERE RowNumber BETWEEN {4} AND {5}",
                                                  order_clause,
                                                  sb_columns.ToString(),
                                                  from_clause,
                                                  where_clause,
                                                  start_row,
                                                  end_row);
            }
            else
            {
                select_statement = string.Format("SELECT {0} FROM {1} WHERE {2} {3}", sb_columns.ToString(), from_clause, where_clause, order_clause);
            }

            return Select(container_schema, select_statement, extra_params);
        }

        //public static DbRow[] select_dt(DbRow container_schema, string select_statement, SqlQueryParameter[] extra_params)
        //{
        //    DataTable dt_results = SqlQuery.execute_sql(select_statement, CommandType.Text, extra_params);

        //    //return dt_results;

        //    if (dt_results.Rows.Count == 0) return null;

        //    List<DbRow> db_items = new List<DbRow>(dt_results.Rows.Count);

        //    foreach (DataRow dr_result in dt_results.Rows)
        //    {
        //        DbRow db_item = new DbRow(container_schema);

        //        db_item.LoadValues(dr_result);

        //        db_items.Add(db_item);
        //    }

        //    return db_items.ToArray();
        //}

        public static DbRow[] Select(DbRow container_schema, string select_statement, params SqlQueryParameter[] extra_params)
        {
            List<DbRow> rows = null;

            using (SqlDataReader reader = SqlQuery.Select(container_schema.connection_string, select_statement, CommandType.Text, CommandBehavior.CloseConnection, extra_params))
            {
                if (reader.HasRows)
                {
                    rows = new List<DbRow>();

                    while (reader.Read())
                    {
                        DbRow row = new DbRow(container_schema);

                        row.LoadValuesWithoutRead(reader);

                        rows.Add(row);
                    }
                }
            }

            return rows != null ? rows.ToArray() : null;
        }

        public void Select(string where_clause, params SqlQueryParameter[] extra_params)
        {
            Select(null, null, where_clause, extra_params);
        }

        public void Select(string select_columns, string join_clause, string where_clause, params SqlQueryParameter[] sql_params)
        {
            if (!string.IsNullOrEmpty(where_clause)) where_clause = string.Format("WHERE {0}", where_clause);

            string select_statement = string.Format("SELECT {0} FROM {1} {2} {3}",
                                                     string.IsNullOrEmpty (select_columns) ? GetSelectColumns() : select_columns,
                                                     table_name,
                                                     join_clause,
                                                     where_clause);

            using (SqlDataReader reader = SqlQuery.Select(connection_string, select_statement, CommandType.Text, CommandBehavior.CloseConnection | CommandBehavior.SingleResult, sql_params))
            {
                LoadValues(reader);
            }
        }

        public Type [] Select <Type> (string where_clause, Func<DbRow, Type> construct_from_db_row, params SqlQueryParameter[] extra_params)
        {
            return Select<Type>(null, null, where_clause, construct_from_db_row, extra_params);
        }

        public Type[] Select<Type>(string select_columns, string join_clause, string where_clause, Func<DbRow, Type> construct_from_db_row, params SqlQueryParameter[] extra_params)
        {
            return Select<Type>(null, select_columns, join_clause, where_clause, null, construct_from_db_row, extra_params);
        }

        public Type[] Select<Type>(string top_clause, string select_columns, string join_clause, string where_clause, string order_by_clause, Func<DbRow, Type> construct_from_db_row, params SqlQueryParameter[] extra_params)
        {
            if (string.IsNullOrEmpty(select_columns)) select_columns = GetSelectColumns();
            if (!string.IsNullOrEmpty(where_clause)) where_clause = string.Format("WHERE {0}", where_clause);
            if (!string.IsNullOrEmpty(order_by_clause)) order_by_clause = string.Format("ORDER BY {0}", order_by_clause);

            string select_statement = string.Format("SELECT {0} {1} FROM {2} {3} {4} {5}",
                                                     top_clause,
                                                     select_columns,
                                                     table_name,
                                                     join_clause,
                                                     where_clause,
                                                     order_by_clause);

            List<Type> results = null;

            using (SqlDataReader reader = SqlQuery.Select(connection_string, select_statement, CommandType.Text, CommandBehavior.CloseConnection, extra_params))
            {
                if (reader.HasRows)
                {
                    results = new List<Type>();

                    while (reader.Read())
                    {
                        DbRow temp = new DbRow(this);

                        temp.LoadValuesWithoutRead(reader);

                        results.Add(construct_from_db_row(temp));
                    }
                }
            }

            return results != null ? results.ToArray() : null;
        }

        public int SelectCount(DbQuery query)
        {
            string select_columns = query.Columns;
            string from = query.From;
            string where = query.Where;
            string join = null;
            
            if (string.IsNullOrEmpty(select_columns)) select_columns = GetSelectColumns();
            if (string.IsNullOrEmpty(from)) from = table_name;
            if (!string.IsNullOrEmpty(where)) where = string.Format("WHERE {0}", where);

            List<SqlQueryParameter> sql_parameters = null;
            
            if (query.Parameters != null) sql_parameters = new List<SqlQueryParameter>(query.Parameters);

            if (query.Join != null)
            {
                var join_clause = query.Join;
                var join_query = new DbQueryToSql(this, join_clause.Query);

                join = string.Format("{0} JOIN {1} ON {2}", join_clause.Type, join_query.Query, join_clause.Condition);

                if (join_query.Parameters != null)
                {
                    if (sql_parameters == null) sql_parameters = new List<SqlQueryParameter>();

                    sql_parameters.AddRange(join_query.Parameters);
                }
            }

            string select_statement;

            select_statement = string.Format("SELECT COUNT ({0}) FROM {1} {2} {3}",
                                                    select_columns,
                                                    from,
                                                    join,
                                                    where);

            return Convert.ToInt32(SqlQuery.execute_sql_scalar(connection_string, select_statement, CommandType.Text, sql_parameters != null ? sql_parameters.ToArray () : null));
        }

        public void Select (DbQuery query)
        {
            var sql_query = new DbQueryToSql(this, query);

            using (SqlDataReader reader = SqlQuery.Select(connection_string, sql_query.Query, CommandType.Text, CommandBehavior.SingleResult | CommandBehavior.CloseConnection, sql_query.Parameters))
            {
                LoadValues(reader);
            }
        }

        public ICollection <Type> Select <Type> (DbQuery query, Func <DbRow, Type> construct_from_container) 
        {
            var sql_query = new DbQueryToSql(this, query);

            Collection <Type> results = new Collection <Type>();
            
            using (SqlDataReader reader = SqlQuery.Select(connection_string, sql_query.Query, CommandType.Text, CommandBehavior.CloseConnection, sql_query.Parameters))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DbRow c = new DbRow (this);
                        
                        c.LoadValuesWithoutRead(reader);

                        Type t = construct_from_container(c);

                        results.Add(t);
                    }
                }
            }

            return results;
        }

        public Type SelectSingle <Type>(DbQuery query, Func<DbRow, Type> construct_from_container) where Type : class
        {
            var sql_query = new DbQueryToSql(this, query);

            Type t = null;

            using (SqlDataReader reader = SqlQuery.Select(connection_string, sql_query.Query, CommandType.Text, CommandBehavior.CloseConnection, sql_query.Parameters))
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        DbRow c = new DbRow(this);

                        c.LoadValuesWithoutRead(reader);

                        t = construct_from_container(c);
                    }
                }
            }

            return t;
        }

        public string GetSelectColumns()
        {
            return GetSelectColumns(this, null);
        }

        static public string GetSelectColumns(DbRow container, string container_column_prefix)
        {
            StringBuilder sb_columns = new StringBuilder();

            foreach (string key in container.properties.Keys)
            {
                DbRowValue current_field = container.properties[key];

                if (current_field.is_managed)
                {
                    if (sb_columns.Length > 0) sb_columns.Append(',');

                    if (!string.IsNullOrEmpty(container_column_prefix))
                    {
                        sb_columns.Append(container_column_prefix);
                        sb_columns.Append('.');
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(current_field.table_name))
                        {
                            sb_columns.Append(current_field.table_name);
                            sb_columns.Append('.');
                        }
                        else if (!string.IsNullOrEmpty(container.table_name))
                        {
                            sb_columns.Append(container.table_name);
                            sb_columns.Append('.');
                        }                        
                    }

                    sb_columns.Append(key);
                }
            }

            return sb_columns.ToString();
        }

        public static Type[] CreateArray<Type>(DbRow[] items, Func<DbRow, Type> construct_from)
        {
            if (items == null) return null;

            List<Type> type_instances = new List<Type>(items.Length);

            foreach (DbRow item in items)
            {
                Type image = construct_from(item);

                type_instances.Add(image);
            }

            return type_instances.ToArray();
        }

        public object Insert()
        {
            return Insert(table_name);
        }

        public object Insert(string table_name)
        {
            StringBuilder sb_columns = new StringBuilder();
            StringBuilder sb_values = new StringBuilder();

            foreach (string key in properties.Keys)
            {
                //TODO:
                if (!properties[key].is_read_only)// && properties [key].has_value_changed)
                {
                    if (sb_columns.Length > 0) sb_columns.Append(',');
                    if (sb_values.Length > 0) sb_values.Append(',');

                    sb_columns.Append(key);
                    sb_values.AppendFormat("@{0}", key);
                }
            }

            string sql_insert = string.Format("INSERT INTO {0} ({1}) VALUES ({2}); SELECT SCOPE_IDENTITY ()", table_name, sb_columns.ToString(), sb_values.ToString());

            return SqlQuery.execute_sql_scalar(connection_string, sql_insert, CommandType.Text, GetSqlQueryParameters());
        }

        public void Update(string where_clause)
        {
            Update(table_name, where_clause, null);
        }

        public void Update(string table_name, string where_clause)
        {
            Update(table_name, where_clause, null);
        }

        public void Update(string table_name, string where_clause, string extra_set)
        {
            if (string.IsNullOrEmpty(table_name)) table_name = this.table_name;

            StringBuilder sb_set_clause = new StringBuilder();

            foreach (string key in properties.Keys)
            {
                //TODO:
                if (!properties[key].is_read_only)// && properties [key].has_value_changed)
                {
                    if (sb_set_clause.Length > 0) sb_set_clause.Append(',');

                    sb_set_clause.Append(string.Format("{0} = @{0}", key));
                }
            }

            string set_clause = sb_set_clause.ToString();

            if (!string.IsNullOrEmpty(set_clause) && !string.IsNullOrEmpty(extra_set)) set_clause += ',';

            string sql_update = string.Format("UPDATE {0} SET {1} {2} WHERE {3}", table_name, set_clause, extra_set, where_clause);

            SqlQuery.execute_sql_non_query(connection_string, sql_update, CommandType.Text, GetSqlQueryParameters());
        }

        public void Delete(string where_clause)
        {
            string sql_delete = string.Format("DELETE FROM {0} WHERE {1}", table_name, where_clause);

            SqlQuery.execute_sql_non_query(connection_string, sql_delete, CommandType.Text);
        }

        public SqlQueryParameter[] GetSqlQueryParameters()
        {
            List<SqlQueryParameter> parameters = new List<SqlQueryParameter>(properties.Count);

            foreach (string key in properties.Keys)
            {
                
                if (properties[key].is_key && properties[key].value == null && properties[key].default_value == null) continue;

                //TODO:
                parameters.Add(GetSqlQueryParameter(key, properties[key].default_value));
            }

            return parameters.ToArray();
        }

        public SqlQueryParameter GetSqlQueryParameter(string column_name, object default_value)
        {
            return new SqlQueryParameter("@" + column_name, GetDbValue(column_name, default_value));
        }

        public SqlQueryParameter GetSqlQueryParameter(string column_name, object default_value, SqlDbType db_type)
        {
            return new SqlQueryParameter("@" + column_name, GetDbValue(column_name, default_value), db_type);
        }

        public object GetValue(string column_name)
        {
            return properties.ContainsKey(column_name) ? GetValue(column_name, properties[column_name].default_value) : null;
        }

        public object GetValue(string column_name, object default_value)
        {
            if (properties.ContainsKey(column_name))
            {
                DbRowValue row_value = properties[column_name];

                return (row_value.value != null) ? row_value.value : default_value;
            }
            return default_value;
        }

        public Type GetValue<Type>(string column_name)
        {
            return (Type)GetValue(column_name);
        }

        //this should be mostly for internal use see GetSqlQueryParameter
        public object GetDbValue(string column_name, object default_value)
        {
            if (properties.ContainsKey(column_name))
            {
                DbRowValue row_value = properties[column_name];

                return (row_value.value != null) ? row_value.value : ((row_value.allows_dbnull && default_value == null) ? DBNull.Value : default_value);
            }
            return default_value;
        }

        public string GetValueAsString(string column_name, object default_value)
        {
            return Convert.ToString(GetValue(column_name, default_value));
        }

        public void SetValue(string column_name, object value)
        {
            if (!properties.ContainsKey(column_name)) properties[column_name] = new DbRowValue();
            
            properties[column_name].has_value_changed = true;
            properties[column_name].value = value;
        }

        public object this[string column]
        {
            get { return GetValue(column); }
            set { SetValue(column, value); }
        }

        public bool HasValueChanged(string column_name)
        {
            return properties.ContainsKey(column_name) && properties[column_name].has_value_changed;
        }

    }
}