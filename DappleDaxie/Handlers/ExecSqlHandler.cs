using DappleDaxie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace DappleDaxie.Handlers
{
  public class ExecSqlModel
  {
    public string Sql { get; set; }
  }

  public class ExecSqlHandler : HandlerBase, IHandler
  {
    public CallResult Process(HttpContext context)
    {
      var model = base.GetInput<ExecSqlModel>(context);

      CallResult result = new CallResult();

      result.Success = true;
      try
      {
        Data.Data.ExecuteText(model.Sql);
      }
      catch (Exception)
      {
        result.Success = false;
      }

      //result.Result = new { IsValid = true, AuthToken = "1234-5678" };

      // TODO: Check Credentials

      // TODO: Generate Auth Token

      return result;
    }
  }
}