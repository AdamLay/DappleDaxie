using DappleDaxie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace DappleDaxie.Handlers
{
  public class AuthenticateHandler : HandlerBase, IHandler
  {
    public CallResult Process(HttpContext context)
    {
      CallResult result = new CallResult();

      result.Success = true;

      //result.Result = new { IsValid = true, AuthToken = "1234-5678" };

      // TODO: Check Credentials

      // TODO: Generate Auth Token

      return result;
    }
  }
}