using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Data;
using System.Web.UI;
using System.IO;
using System.Reflection;
using System.Threading;

using Quartz.Portal;


public class ViewManager
{
    public static string RenderView(string path)
    {
        return RenderView(path, null);
    }

    public static string RenderView(string path, object data)
    {
        Page pageHolder = new Page();
        UserControl viewControl = (UserControl)pageHolder.LoadControl(path);

        if (data != null)
        {
            Type viewControlType = viewControl.GetType();
            FieldInfo field = viewControlType.GetField("Data");

            if (field != null)
            {
                field.SetValue(viewControl, data);
            }
            else
            {
                throw new Exception("View file: " + path + " does not have a public Data property");
            }
        }

        pageHolder.Controls.Add(viewControl);

        StringWriter output = new StringWriter();
        HttpContext.Current.Server.Execute(pageHolder, output, false);

        return output.ToString();
    }
}


[ScriptService]
public class ToolTipWebService : System.Web.Services.WebService
{
    [WebMethod]
    public string GetToolTipData(object context)
    {
        IDictionary<string, object> contextDictionary = (IDictionary<string, object>)context;
        string elementID = ((string)contextDictionary["Value"]);

        // elementID is the user_id
        // will send a single row of a datatable to the tooltip

        DataTable dt = new DataTable();
        dt = qPtl_User.GetSingleUserTooltip(Convert.ToInt32(elementID));
        DataRow row = dt.Rows[0];

        return ViewManager.RenderView("~/social/controls/profile/tooltip.ascx", dt);
    }
}