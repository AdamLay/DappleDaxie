using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
  }
}