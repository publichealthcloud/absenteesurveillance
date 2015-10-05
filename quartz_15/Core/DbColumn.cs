using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz
{
    public interface IDbColumn
    {
        string GetColumnName();

        string GetValueAsString(object default_value);
        void SetValue(string text_value, object default_value);
        bool HasValueChanged();
    }

    [Serializable]
    public class DbColumn<FieldType> : IDbColumn
    {
        private string field_id;
        private FieldType default_value;

        private DbRow container;

        public DbColumn(DbRow container, string field_id, FieldType default_value)
        {
            this.field_id = field_id;
            this.default_value = default_value;
            this.container = container;
        }

        public DbColumn(DbRow container, string field_id)
            : this(container, field_id, default(FieldType))
        {
        }

        public FieldType Value
        {
            get
            {
                try
                {
                    return (FieldType)container.GetValue(field_id, default_value);
                }
                catch
                {
                    try
                    {
                        return (FieldType)Convert.ChangeType(container.GetValue(field_id, default_value), typeof(FieldType));
                    }
                    catch
                    {
                        return default_value;
                    }
                }
            }
            set { container.SetValue(field_id, value); }
        }

        public string GetColumnName()
        {
            return string.Format("{0}.{1}", container.GetContainerName(), FieldID);
        }

        public string FieldID
        {
            get { return field_id; }
        }

        public string GetValueAsString(object default_value)
        {
            return container.GetValueAsString(field_id, default_value);
        }

        public void SetValue(string text_value, object default_value)
        {
            try
            {
                Value = (FieldType)Convert.ChangeType(text_value, typeof(FieldType));
            }
            catch
            {
                Value = (FieldType)Convert.ChangeType(default_value, typeof(FieldType));
            }
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public bool HasValueChanged()
        {
            return container.HasValueChanged(field_id);
        }
    }
}
