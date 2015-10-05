using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_KeywordReference
    {
        protected static qPtl_KeywordReference schema = new qPtl_KeywordReference();
        protected static DbRow schema2 = new DbRow((new qPtl_KeywordReference()).container);

        protected DbRow container;
        protected readonly DbColumn<Int32> keyword_references_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> keyword_id;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;

        public Int32 KeywordReferencesID { get { return keyword_references_id.Value; } set { keyword_references_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 KeywordID { get { return keyword_id.Value; } set { keyword_id.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }

        public qPtl_KeywordReference()
            : this(new DbRow())
        {
        }

        protected qPtl_KeywordReference(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_KeywordReferences");
            keyword_references_id = container.NewColumn<Int32>("KeywordReferencesID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            keyword_id = container.NewColumn<Int32>("KeywordID");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
        }

        public qPtl_KeywordReference(Int32 keyword_references_id)
            : this()
        {
            container.Select(
                new DbQuery
                {
                    Join = new DbQuery.DbJoin("INNER", "qPtl_Keywords", "qPtl_Keywords.KeywordID = qPtl_KeywordReferences.KeywordID"),
                    Where = "KeywordReferencesID = @KeywordReferencesID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@KeywordReferencesID", keyword_references_id) },
                },
                c => new qPtl_KeywordReference(c));
        }

        public void Update()
        {
            container.Update("KeywordReferencesID = @KeywordReferencesID");
        }

        public void Insert()
        {
            KeywordReferencesID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteAllKeywordReferences(int keyword_id)
        {
            schema.container.Delete(string.Concat("KeywordID = ", keyword_id));
        }

        public static void DeleteKeywordReferencesByContent(int content_type_id, int reference_id)
        {
            schema.container.Delete(string.Concat("ContentTypeID = ", content_type_id, " AND ReferenceID = ", reference_id));
        }

        public static void DeleteKeywordReference(int keyword_id, int content_type_id, int reference_id)
        {
            schema.container.Delete(string.Concat("KeywordID = ", keyword_id, " AND ContentTypeID = ", content_type_id, " AND ReferenceID = ", reference_id));
        }

        public static ICollection<qPtl_KeywordReference> GetReferencesByKeyword(int keyword_id)
        {
            return schema.container.Select<qPtl_KeywordReference>(
                new DbQuery
                {
                    Join = new DbQuery.DbJoin("INNER", "qPtl_Keywords", "qPtl_Keywords.KeywordID = qPtl_KeywordReferences.KeywordID"),
                    Where = "qPtl_KeywordReferences.Available = 'Yes' AND qPtl_KeywordReferences.KeywordID = @KeywordID",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@KeywordID", keyword_id) }
                },
                c => new qPtl_KeywordReference(c));
        }

        public static ICollection<qPtl_KeywordReference> GetKeywordsByContent(int content_type_id, int reference_id)
        {
            return schema.container.Select<qPtl_KeywordReference>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND ContentTypeID = @ContentTypeID AND ReferenceID = @ReferenceID",
                    Parameters = new[] {
                        new SqlQueryParameter ("@ContentTypeID", content_type_id), new SqlQueryParameter ("@ReferenceID", reference_id)
                    }
                }, c => new qPtl_KeywordReference(c));
        }

        public static qPtl_KeywordReference[] GetKeywordReferencesArrayByContent(int content_type_id, int reference_id)
        {
            DbRow[] keywordReferences = DbRow.Select(schema2, string.Concat("SELECT * FROM qPtl_KeywordReferences WHERE Available = 'Yes' AND MarkAsDelete = 0 AND ContentTypeID = ", content_type_id, " AND ReferenceID = ", reference_id), null);

            return DbRow.CreateArray<qPtl_KeywordReference>(keywordReferences, current_container => new qPtl_KeywordReference(current_container));
        }

        public static int GetKeywordReferenceCount(int keyword_id)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(
                "SELECT COUNT (*) FROM qPtl_KeywordReferences INNER JOIN qPtl_Keywords ON qPtl_Keywords.KeywordID = qPtl_KeywordReferences.KeywordID WHERE qPtl_KeywordReferences.Available = 'Yes' AND KeywordID = @KeywordID",
                CommandType.Text,
                new SqlQueryParameter[] { new SqlQueryParameter("@KeywordID", keyword_id) }));
        }
    }

    public class qPtl_KeywordReference_View
    {
        protected static qPtl_KeywordReference_View schema = new qPtl_KeywordReference_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> keyword_references_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> keyword_id;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<String> keyword;
        protected readonly DbColumn<String> content_type;

        public Int32 KeywordReferencesID { get { return keyword_references_id.Value; } set { keyword_references_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 KeywordID { get { return keyword_id.Value; } set { keyword_id.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public String Keyword { get { return keyword.Value; } set { keyword.Value = value; } }
        public String ContentType { get { return content_type.Value; } set { content_type.Value = value; } }

        public qPtl_KeywordReference_View()
            : this(new DbRow())
        {
        }

        protected qPtl_KeywordReference_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_KeywordReferences_View");
            keyword_references_id = container.NewColumn<Int32>("KeywordReferencesID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            keyword_id = container.NewColumn<Int32>("KeywordID");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            keyword = container.NewColumn<String>("Keyword");
            content_type = container.NewColumn<String>("ContentType");
        }

        public qPtl_KeywordReference_View(Int32 keyword_references_id)
            : this()
        {
            container.Select(
                new DbQuery
                {
                    Join = new DbQuery.DbJoin("INNER", "qPtl_Keywords", "qPtl_Keywords.KeywordID = qPtl_KeywordReferneces.KeywordID"),
                    Where = "KeywordReferencesID = @KeywordReferencesID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@KeywordReferencesID", keyword_references_id) },
                },
                c => new qPtl_KeywordReference_View(c));
        }

        public void Update()
        {
            container.Update("KeywordReferencesID = @KeywordReferencesID");
        }

        public void Insert()
        {
            KeywordReferencesID = Convert.ToInt32(container.Insert());
        }
    }
}
