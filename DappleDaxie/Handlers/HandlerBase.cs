using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;

namespace DappleDaxie.Handlers
{
  public class HandlerBase
  {
    public const string CLIENT_ID = "dc98707a7af94576a919b0ea41364c55";
    public const string USER_ID = "1985262398";

    protected string GetInstagramApiUrl(string endpoint)
    {
      return "https://api.instagram.com/v1/" + endpoint + "?client_id=" + CLIENT_ID;
    }

    protected T GetInput<T>(HttpContext context = null)
    {
      if (context == null)
        context = HttpContext.Current;

      return new JavaScriptSerializer().Deserialize<T>(context.Request.Form["Data"]);
    }
  }
}