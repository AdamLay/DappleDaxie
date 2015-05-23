using DappleDaxie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace DappleDaxie.Handlers
{
  public class ImagesHandler : HandlerBase, IHandler
  {
    public CallResult Process(HttpContext context)
    {
      CallResult result = new CallResult();

      result.Success = true;

      string url = base.GetInstagramApiUrl("users/" + USER_ID + "/media/recent");

      using (var client = new WebClient())
      using (StreamReader sr = new StreamReader(client.OpenRead(url)))
      {
        result.Result = new { Response = sr.ReadToEnd() };
      }

      return result;
    }
  }
}