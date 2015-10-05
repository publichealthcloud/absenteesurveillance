using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;

using Telerik.Web.UI;

using Viddler.Users;
using Viddler.Videos;

using Quartz;
using Quartz.Portal;
using Quartz.Social;

public partial class upload_video : System.Web.UI.Page
{
    private Thread uploadThread;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            plhExternal.Visible = false;
            plhInternal.Visible = false;
            plhSubmit.Visible = false;
        }
    }

    protected void loadVideoMethod(object sender, EventArgs e)
    {
        if (ddlVideoType.SelectedValue == "internal")
        {
            plhExternal.Visible = false;
            plhInternal.Visible = true;
            plhSubmit.Visible = true;
            rfvEmbed.Enabled = false;
        }
        else
        {
            plhExternal.Visible = true;
            plhInternal.Visible = false;
            plhSubmit.Visible = true;
            rfvEmbed.Enabled = true;
        }
    }

    protected void btn_submit_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);
        int new_video_id = 0;

        if (ddlVideoType.SelectedValue == "internal")
        {
            if (rad_upload.UploadedFiles.Count > 0 && user_id > 0)
            {
                string user_name = (new qPtl_User(user_id)).UserName;

                foreach (UploadedFile file in rad_upload.UploadedFiles)
                {
                    string video_path = Server.MapPath(string.Format("~/user_data/{0}", user_name));

                    if (!Directory.Exists(video_path))
                    {
                        Directory.CreateDirectory(video_path);
                    }

                    // create video
                    string title = txtTitle.Text;
                    string description = "";
                    qSoc_Videos video = new qSoc_Videos(user_id, string.Empty, title, description, TimeSpan.Zero, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, (int)qSoc_Videos.statusCodes.Waiting, string.Empty, string.Empty, 0, "manager");
                    video.AddVideo();

                    int videoID = video.videoID;
                    string video_extension = Helper.get_filename_extension(file.FileName);
                    string video_file_name = string.Format("{0}_{1}.{2}", user_id, video.videoID, video_extension);
                    string video_full_path = Path.Combine(video_path, video_file_name);

                    file.SaveAs(video_full_path);

                    // update to viddler + update video
                    Viddler.ViddlerService viddlerService = video.GetVidlerService();
                    viddlerService.Users.Auth(ConfigurationManager.AppSettings["ViddlerUsername"], ConfigurationManager.AppSettings["ViddlerPassword"]);
                    uploadThread = new Thread(video.UploadVideoManager);
                    uploadThread.Start(new object[] { viddlerService, video_full_path, video_file_name, title, description, videoID });
                    video.UpdateVideo();

                    new_video_id = video.videoID;
                }
            }
        }
        else
        {
            // process for external upload
            string title = txtTitle.Text;
            string description = "";
            qSoc_Videos video = new qSoc_Videos(user_id, string.Empty, title, description, TimeSpan.Zero, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, (int)qSoc_Videos.statusCodes.Waiting, string.Empty, string.Empty, 0, "manager");
            video.AddVideo();

            new_video_id = video.videoID;
        }

        // Add primary comment
        qSoc_Comment comment = new qSoc_Comment();
        comment.Insert(user_id, qSoc_ContentType.Types.Video, new_video_id);

        qSoc_Video video2 = new qSoc_Video(new_video_id);
        video2.ApprovedStatus = "Approved";
        video2.Source = "manager";
        video2.ApprovedBy = user_id;
        video2.CreatedBy = user_id;
        video2.Created = DateTime.Now;
        video2.LastModified = DateTime.Now;
        video2.LastModifiedBy = user_id;
        video2.Source = ddlVideoType.SelectedValue;
        if (ddlVideoType.SelectedValue == "external")
        {
            video2.EmbedCode = txtEmbedCode.Text;
            video2.SourceVideoID = txtSourceVideoID.Text;
            video2.ExternalSourceName = ddlExternalSource.SelectedValue;
            
        }
        video2.UploadedFrom = "manager";
        video2.Update();

        rad_upload.Visible = false;

        // redirect to page to add theme + keywords
        Response.Redirect("video-edit.aspx?videoID=" + new_video_id);
    }
}
