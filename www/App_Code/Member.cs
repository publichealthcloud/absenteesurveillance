using System.Collections.Generic;
using System.Collections;
using System;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Data;

using Quartz.Portal;

/// <summary>
/// Summary description for Employee
/// </summary>
public class Member
{
    public int ID { get; set; }
    public string Email { get; set; }

	public Member()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<Member> GetMemberList(int requester_user_id)
    {
        List<Member> memberList = new List<Member>();

        DataTable dt_members = new DataTable();

        dt_members = qPtl_User.GetUsersByFilters(0, requester_user_id, "alpha");

        if (dt_members != null)
        {
            foreach (DataRow dr in dt_members.Rows)
            {
                memberList.Add(new Member() { ID = Convert.ToInt32(dr["UserID"]), Email = Convert.ToString(dr["UserName"]) });
            }
        }

        return memberList;
    }

}
