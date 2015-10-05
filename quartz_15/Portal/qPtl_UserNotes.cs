using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_UserNote
    {
        protected static qPtl_UserNote schema = new qPtl_UserNote();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_note_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32?> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> note_type;
        protected readonly DbColumn<String> note;

        public Int32 UserNoteID { get { return user_note_id.Value; } set { user_note_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32? LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String NoteType { get { return note_type.Value; } set { note_type.Value = value; } }
        public String Note { get { return note.Value; } set { note.Value = value; } }

        public qPtl_UserNote()
            : this(new DbRow())
        {
        }

        protected qPtl_UserNote(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserNotes");
            user_note_id = container.NewColumn<Int32>("UserNoteID", true);
            user_id = container.NewColumn<Int32>("UserID");
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32?>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            note_type = container.NewColumn<String>("NoteType");
            note = container.NewColumn<String>("Note");
        }

        public qPtl_UserNote(Int32 user_note_id)
            : this()
        {
            container.Select("UserNoteID = @UserNoteID", new SqlQueryParameter("@UserNoteID", user_note_id));
        }

        public void Update(Int32 user_id, Int32 scope_id, String available, DateTime created, Int32 created_by, DateTime last_modified, Int32 last_modified_by, Int32 mark_as_delete, String note_type, String note)
        {
            UserID = user_id;
            ScopeID = scope_id;
            Available = available;
            Created = created;
            CreatedBy = created_by;
            LastModified = last_modified;
            LastModifiedBy = last_modified_by;
            MarkAsDelete = mark_as_delete;
            NoteType = note_type;
            Note = note;

            container.Update(string.Format ("UserNoteID = {0}", UserNoteID));
        }

        public void Insert(Int32 user_id, String note_type, String note)
        {
            UserID = user_id;
            Available = "Yes";
            Created = DateTime.Now;
            CreatedBy = user_id;
            MarkAsDelete = 0;
            NoteType = note_type;
            Note = note;

            UserNoteID = Convert.ToInt32(container.Insert());
        }
    }
}
