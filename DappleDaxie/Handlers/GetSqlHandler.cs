using DappleDaxie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace DappleDaxie.Handlers
{
  

  public class GetSqlHandler : HandlerBase, IHandler
  {
    class Model
    {
      public string Sql { get; set; }
    }

    public CallResult Process(HttpContext context)
    {
      var model = base.GetInput<Model>(context);

      CallResult result = new CallResult();

      result.Success = true;
      
      try
      {
        result.Result = Data.Data.GetFromText(model.Sql);
      }
      catch (Exception ex)
      {
        result.Success = false;
        result.Result = ex.Message;
      }

      //result.Result = new { IsValid = true, AuthToken = "1234-5678" };

      // TODO: Check Credentials

      // TODO: Generate Auth Token

      return result;
    }
  }
}