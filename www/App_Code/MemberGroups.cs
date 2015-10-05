using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]

public class MemberGroup : System.Web.Services.WebService
{
    [WebMethod]
    public List<Member> FetchMemberList(string mail, int user_id)
    {
        var mem = new Member();
        var fetchMembers = mem.GetMemberList(user_id)
        .Where(m => m.Email.ToLower().StartsWith(mail.ToLower()));
        return fetchMembers.ToList();
    }
}

