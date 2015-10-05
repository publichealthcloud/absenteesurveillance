using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;

namespace Quartz.Controls
{
    public partial class MemberEnrolledGroup : System.Web.UI.UserControl
    {
        protected int user_id, space_id, user_space_id;
        protected bool is_primary;

        public int UserID
        {
            get { return user_id; }
            set { user_id = value; }
        }

        public int SpaceID
        {
            get { return space_id; }
            set { space_id = value; }
        }

        public int UserSpaceID
        {
            get { return user_space_id; }
            set { user_space_id = value; }
        }

        public bool IsPrimary
        {
            get { return is_primary; }
            set { is_primary = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                qSoc_UserSpace_View space = new qSoc_UserSpace_View(user_space_id);
                user_id = space.UserID;
                space_id = space.SpaceID;
                litGroupName.Text = space.SpaceName;
                litCreated.Text = Convert.ToString(space.Created);

                if (is_primary == true)
                    litGroupType.Text = "<span class=\"label label-lightred\">Primary Group</span>";
            }
        }
    }
}