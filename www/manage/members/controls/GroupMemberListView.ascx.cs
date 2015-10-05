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
    public partial class GroupMemberListView : System.Web.UI.UserControl
    {
        protected int user_id, space_id;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                qPtl_User user = new qPtl_User(user_id);
                lblUserID.Text = Convert.ToString(user_id);
                litEmail.Text = user.Email;
                litUsername.Text = user.UserName;
                litFullName.Text = user.FirstName + " " + user.LastName;

                var u_space = qSoc_UserSpace.GetUserSpace(user_id, space_id);
                if (u_space != null)
                {
                    litEnrolled.Text = u_space.Created.ToString("d");
                    if (u_space.SpaceRole == "Moderator")
                        litGroupRole.Text = "<span class=\"label label-lightred\">Advisor</span>";
                    else
                        litGroupRole.Text = "<span class=\"label label-satgreen\">Teen</span>";
                }
            }
        }
    }
}