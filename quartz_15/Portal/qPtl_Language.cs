using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_Language
    {
        protected static qPtl_Language schema = new qPtl_Language();

        protected DbRow container;
        protected readonly DbColumn<Int32> language_id;
        protected readonly DbColumn<String> display_name;
        protected readonly DbColumn<String> code;
        protected readonly DbColumn<Boolean> available;

        public Int32 LanguageID { get { return language_id.Value; } set { language_id.Value = value; } }
        public String DisplayName { get { return display_name.Value; } set { display_name.Value = value; } }
        public String Code { get { return code.Value; } set { code.Value = value; } }
        public Boolean Available { get { return available.Value; } set { available.Value = value; } }

        public qPtl_Language()
            : this(new DbRow())
        {
        }

        protected qPtl_Language(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Languages");
            language_id = container.NewColumn<Int32>("LanguageID", true);
            display_name = container.NewColumn<String>("DisplayName");
            code = container.NewColumn<String>("Code");
            available = container.NewColumn<Boolean>("Available");
        }

        public qPtl_Language(Int32 language_id)
            : this()
        {
            container.Select("LanguageID = @LanguageID", new SqlQueryParameter("@LanguageID", language_id));
        }

        public qPtl_Language(String language_code)
            : this()
        {
            container.Select("Code = @Code", new SqlQueryParameter("@Code", language_code));
        }

        public static ICollection<qPtl_Language> GetLanguages()
        {
            return schema.container.Select<qPtl_Language>(
                new DbQuery
                {
                    Where = "Available = 1"
                }, c => new qPtl_Language(c));
        }
    }
}
