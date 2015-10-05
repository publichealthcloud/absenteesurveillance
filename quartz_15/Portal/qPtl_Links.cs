using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_Link {

	    protected static qPtl_Link schema = new qPtl_Link ();

	    protected DbRow container;
	    protected readonly DbColumn <Int32> link_id;
	    protected readonly DbColumn <Int32> scope_id;
	    protected readonly DbColumn <String> available;
	    protected readonly DbColumn <DateTime?> created;
	    protected readonly DbColumn <Int32> created_by;
	    protected readonly DbColumn <DateTime?> last_modified;
	    protected readonly DbColumn <Int32> last_modified_by;
	    protected readonly DbColumn <Int32> mark_as_delete;
	    protected readonly DbColumn <String> title;
	    protected readonly DbColumn <String> description;
	    protected readonly DbColumn <String> type;
	    protected readonly DbColumn <String> url;
	    protected readonly DbColumn <String> target;
        protected readonly DbColumn<String> source;
        protected readonly DbColumn<String> external_source_name;
        protected readonly DbColumn<Int32> theme_id;
        protected readonly DbColumn<Int32> author_id;
        protected readonly DbColumn<String> uploaded_from;
        protected readonly DbColumn<String> link_type;
        protected readonly DbColumn<String> language;

	    public Int32 LinkID { get { return link_id.Value; } set { link_id.Value = value; } }
	    public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
	    public String Available { get { return available.Value; } set { available.Value = value; } }
	    public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
	    public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
	    public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
	    public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
	    public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
	    public String Title { get { return title.Value; } set { title.Value = value; } }
	    public String Description { get { return description.Value; } set { description.Value = value; } }
	    public String Type { get { return type.Value; } set { type.Value = value; } }
	    public String URL { get { return url.Value; } set { url.Value = value; } }
	    public String Target { get { return target.Value; } set { target.Value = value; } }
        public String Source { get { return source.Value; } set { source.Value = value; } }
        public String ExternalSourceName { get { return external_source_name.Value; } set { external_source_name.Value = value; } }
        public Int32 ThemeID { get { return theme_id.Value; } set { theme_id.Value = value; } }
        public Int32 AuthorID { get { return author_id.Value; } set { author_id.Value = value; } }
        public String UploadedFrom { get { return uploaded_from.Value; } set { uploaded_from.Value = value; } }
        public String LinkType { get { return link_type.Value; } set { link_type.Value = value; } }
        public String Language { get { return language.Value; } set { language.Value = value; } }

	    public qPtl_Link ()
		    : this (new DbRow ())
	    {
	    }

	    protected qPtl_Link (DbRow c)
	    {
		    container = c;
		    container.SetContainerName ("qPtl_Links");
		    link_id = container.NewColumn <Int32> ("LinkID", true);
		    scope_id = container.NewColumn <Int32> ("ScopeID");
		    available = container.NewColumn <String> ("Available");
		    created = container.NewColumn <DateTime?> ("Created");
		    created_by = container.NewColumn <Int32> ("CreatedBy");
		    last_modified = container.NewColumn <DateTime?> ("LastModified");
		    last_modified_by = container.NewColumn <Int32> ("LastModifiedBy");
		    mark_as_delete = container.NewColumn <Int32> ("MarkAsDelete");
		    title = container.NewColumn <String> ("Title");
		    description = container.NewColumn <String> ("Description");
		    type = container.NewColumn <String> ("Type");
		    url = container.NewColumn <String> ("URL");
		    target = container.NewColumn <String> ("Target");
            source = container.NewColumn<String>("Source");
            external_source_name = container.NewColumn<String>("ExternalSourceName");
            theme_id = container.NewColumn<Int32>("ThemeID");
            author_id = container.NewColumn<Int32>("AuthorID");
            uploaded_from = container.NewColumn<String>("UploadedFrom");
            link_type = container.NewColumn<String>("LinkType");
            language = container.NewColumn<String>("Language");
	    }

	    public qPtl_Link (Int32 link_id)
		    : this () 
	    {
		    container.Select ("LinkID = @LinkID", new SqlQueryParameter ("@LinkID", link_id));
	    }

	    public void Update (Int32 scope_id, String available, DateTime? created, Int32 created_by, DateTime? last_modified, Int32 last_modified_by, Int32 mark_as_delete, String title, String description, String type, String url, String target)
	    {
		    ScopeID = scope_id;
		    Available = available;
		    Created = created;
		    CreatedBy = created_by;
		    LastModified = last_modified;
		    LastModifiedBy = last_modified_by;
		    MarkAsDelete = mark_as_delete;
		    Title = title;
		    Description = description;
		    Type = type;
		    URL = url;
		    Target = target;

		    container.Update ("LinkID = @LinkID");
	    }

        public void Update()
        {
            LastModified = DateTime.Now;
            container.Update("LinkID = @LinkID");
        }

	    public void Insert (Int32 scope_id, String available, DateTime? created, Int32 created_by, DateTime? last_modified, Int32 last_modified_by, Int32 mark_as_delete, String title, String description, String type, String url, String target)
	    {
		    ScopeID = scope_id;
		    Available = available;
		    Created = created;
		    CreatedBy = created_by;
		    LastModified = last_modified;
		    LastModifiedBy = last_modified_by;
		    MarkAsDelete = mark_as_delete;
		    Title = title;
		    Description = description;
		    Type = type;
		    URL = url;
		    Target = target;

		    LinkID = Convert.ToInt32 (container.Insert ());
	    }

        public void Insert()
        {
            Created = DateTime.Now;
            Available = "Yes";
            MarkAsDelete = 0;
            LinkID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_Link> GetLinks()
        {
            return schema.container.Select<qPtl_Link>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "Title ASC"
                }, c => new qPtl_Link(c));
        }

        public static ICollection<qPtl_Link> GetLinksByTheme(int theme_id)
        {
            return schema.container.Select<qPtl_Link>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND ThemeID = @ThemeID",
                    OrderBy = "Title ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@ThemeID", theme_id) },
                }, c => new qPtl_Link(c));
        }
    }
}
