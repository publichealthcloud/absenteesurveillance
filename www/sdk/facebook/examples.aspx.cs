using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Facebook;

public partial class sdk_facebook_get_current_app_user : System.Web.UI.Page
{
    protected string fb_token = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FB_UserToken"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        var accessToken = fb_token;
        var client = new FacebookClient(accessToken);
        dynamic me = client.Get("me");
        string first_name = me.name;
        lblFacebookUserName.Text = first_name;

        /* EXAMPLE ME OBJECT
            {
                id: "14812017",
                name: "Nathan Totten",
                first_name: "Nathan",
                last_name: "Totten",
                link: "https://www.facebook.com/totten",
                username: "totten",
                gender: "male",
                locale: "en_US"
            }
         */
    }


    protected void btnPostToWall_Click(object sender, EventArgs e)
    {
        FacebookClient fbClient = new FacebookClient(fb_token);
        var args = new Dictionary<string, object>();
        args["message"] = "Testing 123";
        fbClient.Post("/me/feed", args);
        lblPostSuccess.Text = "successfully posted";
    }
}