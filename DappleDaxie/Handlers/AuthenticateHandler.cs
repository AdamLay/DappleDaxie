using DappleDaxie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using DappleDaxie.Data.Models;

namespace DappleDaxie.Handlers
{
  public class AuthenticateModel
  {
    public string Username { get; set; }
    public string Password { get; set; }
  }

  public class AuthenticateHandler : HandlerBase, IHandler
  {
    public CallResult Process(HttpContext context)
    {
      var model = base.GetInput<AuthenticateModel>(context);

      CallResult result = new CallResult();

      result.Success = true;

      if (Data.Data.AuthenticateUser(model.Username, model.Password))
      {
        Guid token = Guid.NewGuid();

        Data.Data.CreateAuthToken(token);

        result.Result = new { IsValid = true, AuthToken = token };
      }
      else
      {
        result.Result = new { IsValid = false, AuthToken = "" };
      }

      // TODO: Check Credentials

      // TODO: Generate Auth Token

      return result;
    }
  }
}