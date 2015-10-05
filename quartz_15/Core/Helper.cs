using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Configuration;

using System.IO;

using System.Text.RegularExpressions;

namespace Quartz
{
    public class SqlQueryParameter
    {
        public string name;
        public object value;
        public SqlDbType? db_type;
        public int? size;

        public SqlQueryParameter(string _name, object _value)
        {
            name = _name;
            value = _value;
            db_type = null;
            size = null;
        }

        public SqlQueryParameter(string _name, object _value, SqlDbType _db_type)
        {
            name = _name;
            value = _value;
            db_type = _db_type;
            size = null;
        }

        public SqlQueryParameter(string _name, object _value, SqlDbType _db_type, int _size)
        {
            name = _name;
            value = _value;
            db_type = _db_type;
            size = _size;
        }
    }

    public class SqlQuery
    {
        public static string connection_string = ConfigurationManager.AppSettings["ConnectionString"];

        public static DataTable execute_sql(string sql_statement)
        {
            return execute_sql(connection_string, sql_statement);
        }

        public static DataTable execute_sql(string connection_string, string sql_statement)
        {
            DataTable tbl_results = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(connection_string))
            {
                using (SqlCommand sql_command = new SqlCommand(sql_statement, sql_connection))
                {
                    using (SqlDataAdapter sql_data_adapter = new SqlDataAdapter(sql_command))
                    {
                        sql_data_adapter.Fill(tbl_results);
                    }
                }
            }

            return tbl_results;
        }

        public static SqlDataReader Select(string sql, CommandType type, CommandBehavior behavior, params SqlQueryParameter[] parameters)
        {
            return Select(connection_string, sql, type, behavior, parameters);
        }

        public static SqlDataReader Select(string connection_string, string sql, CommandType type, CommandBehavior behavior, params SqlQueryParameter[] parameters)
        {
            SqlDataReader reader = null;

            SqlConnection sql_connection = new SqlConnection(connection_string);

            try
            {
                using (SqlCommand sql_command = new SqlCommand(sql, sql_connection))
                {
                    sql_connection.Open();

                    sql_command.CommandType = type;

                    if (parameters != null)
                    {
                        foreach (SqlQueryParameter parameter in parameters)
                        {
                            SqlParameter sql_parameter = new SqlParameter(parameter.name, parameter.value != null ? parameter.value : DBNull.Value);

                            if (parameter.db_type.HasValue) sql_parameter.SqlDbType = parameter.db_type.Value;
                            if (parameter.size.HasValue) sql_parameter.Size = parameter.size.Value;

                            sql_command.Parameters.Add(sql_parameter);
                        }
                    }

                    reader = sql_command.ExecuteReader(behavior);
                }
            }
            catch
            {
                sql_connection.Close();
                throw;
            }

            return reader;
        }

        public static DbRow SelectSingleRow(string sql, CommandType type, params SqlQueryParameter[] parameters)
        {
            return SelectSingleRow (connection_string, sql, type, parameters);
        }

        public static DbRow SelectSingleRow(string connection_string, string sql, CommandType type, params SqlQueryParameter[] parameters)
        {
            DbRow result = null;

            using (SqlDataReader reader = Select (connection_string, sql, type, CommandBehavior.SingleResult, parameters))
            {
                result = new DbRow();
                result.LoadValues(reader);
            }

            return result;
        }

        public static DataTable execute_sql(string command, CommandType command_type, params SqlQueryParameter[] parameters)
        {
            return execute_sql(connection_string, command, command_type, parameters);
        }

        public static DataTable execute_sql(string connection_string, string command, CommandType command_type, params SqlQueryParameter[] parameters)
        {
            DataTable tbl_results = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(connection_string))
            {
                using (SqlCommand sql_command = new SqlCommand(command, sql_connection))
                {
                    sql_connection.Open();

                    sql_command.CommandType = command_type;

                    if (parameters != null)
                    {
                        foreach (SqlQueryParameter parameter in parameters)
                        {
                            SqlParameter sql_parameter = new SqlParameter(parameter.name, parameter.value);

                            if (parameter.db_type.HasValue) sql_parameter.SqlDbType = parameter.db_type.Value;
                            if (parameter.size.HasValue) sql_parameter.Size = parameter.size.Value;

                            sql_command.Parameters.Add(sql_parameter);
                        }
                    }

                    using (SqlDataAdapter sql_data_adapter = new SqlDataAdapter(sql_command))
                    {
                        sql_data_adapter.Fill(tbl_results);
                    }
                }
            }

            return tbl_results;
        }

        public static void execute_sql_non_query(string procedure_name, params SqlQueryParameter[] parameters)
        {
            execute_sql_non_query(connection_string, procedure_name, parameters);
        }

        public static void execute_sql_non_query(string connection_string, string procedure_name, params SqlQueryParameter[] parameters)
        {
            using (SqlConnection sql_connection = new SqlConnection(connection_string))
            {
                using (SqlCommand sql_command = new SqlCommand(procedure_name, sql_connection))
                {
                    sql_connection.Open();

                    sql_command.CommandType = CommandType.StoredProcedure;

                    foreach (SqlQueryParameter parameter in parameters)
                    {
                        sql_command.Parameters.Add(new SqlParameter(parameter.name, parameter.value));
                    }

                    sql_command.ExecuteNonQuery();
                }
            }
        }

        public static void execute_sql_non_query(string command, CommandType command_type, params SqlQueryParameter[] parameters)
        {
            execute_sql_non_query(connection_string, command, command_type, parameters);
        }

        public static void execute_sql_non_query(string connection_string, string command, CommandType command_type, params SqlQueryParameter[] parameters)
        {
            using (SqlConnection sql_connection = new SqlConnection(connection_string))
            {
                using (SqlCommand sql_command = new SqlCommand(command, sql_connection))
                {
                    sql_connection.Open();

                    sql_command.CommandType = command_type;

                    foreach (SqlQueryParameter parameter in parameters)
                    {
                        SqlParameter sql_parameter = new SqlParameter(parameter.name, parameter.value);

                        if (parameter.db_type.HasValue) sql_parameter.SqlDbType = parameter.db_type.Value;
                        if (parameter.size.HasValue) sql_parameter.Size = parameter.size.Value;

                        sql_command.Parameters.Add(sql_parameter);
                    }

                    sql_command.ExecuteNonQuery();
                }
            }
        }

        public static object execute_sql_scalar(string command, CommandType command_type, params SqlQueryParameter[] parameters)
        {
            return execute_sql_scalar(connection_string, command, command_type, parameters);
        }

        public static object execute_sql_scalar(string connection_string, string command, CommandType command_type, params SqlQueryParameter[] parameters)
        {
            object scalar = null;

            using (SqlConnection sql_connection = new SqlConnection(connection_string))
            {
                using (SqlCommand sql_command = new SqlCommand(command, sql_connection))
                {
                    sql_connection.Open();

                    sql_command.CommandType = command_type;

                    if (parameters != null)
                    {
                        foreach (SqlQueryParameter parameter in parameters)
                        {
                            SqlParameter sql_parameter = new SqlParameter(parameter.name, parameter.value);

                            if (parameter.db_type.HasValue) sql_parameter.SqlDbType = parameter.db_type.Value;
                            if (parameter.size.HasValue) sql_parameter.Size = parameter.size.Value;

                            sql_command.Parameters.Add(sql_parameter);
                        }
                    }

                    scalar = sql_command.ExecuteScalar();
                }
            }

            return scalar;
        }
    }

    public static class Helper
    {
        public static object read_database_value(object database_value)
        {
            return (database_value != System.DBNull.Value) ? database_value : null;
        }

        public static string read_database_string(object database_value)
        {
            return Convert.ToString(read_database_value(database_value));
        }

        public static void populate_ddl(DropDownList ddl, string first_item_text, string first_item_value, object data_source, string data_text_field, string data_text_format_string, string data_value_field)
        {
            ddl.DataSource = data_source;
            ddl.DataTextField = data_text_field;
            ddl.DataValueField = data_value_field;
            ddl.DataTextFormatString = data_text_format_string;
            ddl.DataBind();

            if (ddl.Items.Count == 0)
            {
                ddl.Enabled = false;
                return;
            }

            if (!string.IsNullOrEmpty(first_item_text)) ddl.Items.Insert(0, new ListItem(first_item_text, first_item_value));

            ddl.Enabled = (ddl.Items.Count != 1);
        }

        public static void populate_ddl_from_id_list(DropDownList ddl, DataTable dt_id, DataTable dt_full_set, string id_field)
        {
            Helper.populate_ddl(ddl, null, null, dt_full_set, "Name", null, id_field);

            for (int i = ddl.Items.Count - 1; i >= 0; i--)
            {
                DataRow[] dr_results = dt_id.Select(string.Format("{0} = {1}", id_field, ddl.Items[i].Value));

                if (dr_results == null || dr_results.Length == 0)
                    ddl.Items.RemoveAt(i);
            }

            ddl.Enabled = (ddl.Items.Count != 0);
        }

        public static void message_log(Control container, string message)
        {
            container.Controls.Add(new LiteralControl(message));
        }

        public static object get_page_object(HttpContext context, string object_name, object default_value, bool remove_object_from_context)
        {
            if (context.Items.Contains(object_name))
            {
                object value = context.Items[object_name];
                if (remove_object_from_context) context.Items.Remove(object_name);
                return value;
            }
            else if (context.Session[object_name] != null)
            {
                object value = context.Session[object_name];
                if (remove_object_from_context) context.Session.Remove(object_name);
                return value;
            }
            else if (!string.IsNullOrEmpty(context.Request[object_name])) return context.Request[object_name];
            else return default_value;
        }

        public static string get_ddl_selected_value(DropDownList ddl)
        {
            if (string.IsNullOrEmpty(ddl.SelectedValue)) return null;

            return ddl.SelectedValue;
        }

        public static string read_database_varbinary_string(object value)
        {
            byte[] content = (byte[])Helper.read_database_value(value);

            return convert_to_string(content);
        }

        public static string convert_to_string(byte[] content)
        {
            if (content != null) return System.Text.Encoding.Default.GetString(content);
            else return string.Empty;
        }

        static public string base64_encode(string s)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(buffer);
        }

        static public string base64_decode(string s)
        {
            byte[] buffer = Convert.FromBase64String(s);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }

        static public string get_filename_extension(string filename)
        {
            string[] filename_parts = filename.Split('.');
            return filename_parts[filename_parts.Length - 1].ToLower();
        }

        static public string strip_html(string html, int max_chars)
        {
            html = Regex.Replace(html, "<(.|\n)+?>", string.Empty);

            return (html.Length > max_chars) ? string.Format("{0}...", html.Substring(0, max_chars)) : html;
        }

        static public string convert_date_to_reference(DateTime date)
        {
            if (date < DateTime.Now.AddDays(-7))
            {
                if (date < DateTime.Now.AddDays(-1))
                {
                    return string.Format("{0} days ago", date.Date - DateTime.Now.Date);
                }
                else if (date < DateTime.Now.AddHours(-1))
                {
                    return string.Format("{0} hours ago", date.Hour - DateTime.Now.Hour);
                }
                else
                {
                    int minutes = date.Minute - DateTime.Now.Minute;

                    if (minutes < 0)
                        minutes *= minutes;

                    return string.Format("{0} minutes ago", minutes);
                }
            }
            else
                return date.ToString();
        }

        public static string ToRelativeDate(DateTime dateTime, string pretext, string posttext)
        {
            if (dateTime == DateTime.MinValue)
                return string.Empty;

            TimeSpan timeSpan = DateTime.Now - dateTime;

            if (timeSpan < TimeSpan.FromSeconds(60))
            {
                return string.Format("{0} seconds {1}", timeSpan.Seconds, posttext);
            }

            if (timeSpan < TimeSpan.FromMinutes(60))
            {
                return timeSpan.Minutes > 1
                    ? string.Format("{0} {1} minutes {2}", pretext, timeSpan.Minutes, posttext)
                    : string.Format("{0} a minute {1}", pretext, posttext);
            }

            if (timeSpan < TimeSpan.FromHours(24))
            {
                return timeSpan.Hours > 1
                    ? string.Format("{0} {1} hours {2}", pretext, timeSpan.Hours, posttext)
                    : string.Format("{0} an hour {1}", pretext, posttext);
            }

            if (timeSpan < TimeSpan.FromDays(7))
            {
                return timeSpan.Days > 1
                    ? string.Format("{0} {1} days {2}", pretext, timeSpan.Days, posttext)
                    : string.Format("{0} a day {1}", pretext, posttext);
            }

            if (timeSpan < TimeSpan.FromDays(28))
            {
                return timeSpan.Days >= 14
                    ? string.Format("{0} {1} weeks {2}", pretext, timeSpan.Days / 7, posttext)
                    : string.Format("{0} a week {1}", pretext, posttext);
            }

            if (timeSpan < TimeSpan.FromDays(365))
            {
                return timeSpan.Days >= 60
                    ? string.Format("{0} {1} months {2}", pretext, timeSpan.Days / 30, posttext)
                    : string.Format("{0} a month {1}", pretext, posttext);
            }


            return timeSpan.Days >= 730
                ? string.Format("{0} {1} years {2}", pretext, timeSpan.Days / 365, posttext)
                : string.Format("{0} a year {1}", pretext, posttext);
        }

        public static string ToRelativeDate(DateTime dateTime, string resource_prefix)
        {
            if (dateTime == DateTime.MinValue)
                return string.Empty;

            TimeSpan timeSpan = DateTime.Now - dateTime;

            if (timeSpan < TimeSpan.FromSeconds(60))
            {
                return string.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "seconds")), timeSpan.Seconds);
            }

            if (timeSpan < TimeSpan.FromMinutes(60))
            {
                return timeSpan.Minutes > 1
                    ? string.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "minutes")), timeSpan.Minutes)
                    : Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "minute"));
            }

            if (timeSpan < TimeSpan.FromHours(24))
            {
                return timeSpan.Hours > 1
                    ? string.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "hours")), timeSpan.Hours)
                    : Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "hour"));
            }

            if (timeSpan < TimeSpan.FromDays(7))
            {
                return timeSpan.Days > 1
                    ? string.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "days")), timeSpan.Days)
                    : Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "day"));
            }

            if (timeSpan < TimeSpan.FromDays(28))
            {
                return timeSpan.Days >= 14
                    ? string.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "weeks")), timeSpan.Days / 7)
                    : Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "week"));
            }

            if (timeSpan < TimeSpan.FromDays(365))
            {
                return timeSpan.Days >= 60
                    ? string.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "months")), timeSpan.Days / 30)
                    : Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "month"));
            }


            return timeSpan.Days >= 730
                ? string.Format(Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "years")), timeSpan.Days / 365)
                : Convert.ToString(HttpContext.GetGlobalResourceObject("Global", resource_prefix + "year"));
        }

        public static string ParseSanitizedInput(string input)
        {
            return HttpUtility.HtmlDecode(input);
        }

        public static string SanitizeInput(string input)
        {
            return HttpUtility.HtmlEncode(input);
        }

        public static string SanitizeOutput(string output)
        {
            return HttpUtility.HtmlEncode(output);
        }

        public static string SanitizeOutput(string output, int max_line_char_count)
        {
            if (string.IsNullOrEmpty(output)) return string.Empty;

            StringBuilder sb_clean = new StringBuilder();

            string[] parts = HttpUtility.HtmlDecode(output).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string p in parts)
            {
                if (p.Length > max_line_char_count)
                {
                    int lines = p.Length / max_line_char_count;
                    int offset = 0;

                    for (int i = 0; i < lines; i++, offset += max_line_char_count)
                    {
                        if (sb_clean.Length > 0) sb_clean.Append("<br />");
                        sb_clean.Append(HttpUtility.HtmlEncode(p.Substring(offset, max_line_char_count)));
                    }

                    sb_clean.Append("<br />");
                    sb_clean.Append(p.Substring(offset));
                }
                else sb_clean.Append(HttpUtility.HtmlEncode(p));

                sb_clean.Append(' ');
            }

            return sb_clean.ToString().TrimEnd();
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
