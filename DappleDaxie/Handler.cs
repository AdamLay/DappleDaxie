using DappleDaxie.Handlers;
using DappleDaxie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DappleDaxie
{
  public class Handler : IHttpHandler
  {
    private static List<IHandler> _handlers;
    private static List<IHandler> Handlers
    {
      get
      {
        if (_handlers == null)
        {
          Type handlerType = typeof(IHandler);

          IEnumerable<Type> handlers = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(m => m.GetTypes())
            .Where(m => handlerType.IsAssignableFrom(m) && m.IsClass);

          _handlers = new List<IHandler>();

          foreach (Type t in handlers)
            _handlers.Add((IHandler)Activator.CreateInstance(t));
        }

        return _handlers;
      }
    }

    public bool IsReusable { get { return false; } }

    public void ProcessRequest(HttpContext context)
    {
      string hName = "DappleDaxie.Handlers." + context.Request.Form["Handler"] + "Handler";

      IHandler handler = Handlers.First(m => m.GetType().FullName == hName);
      
      try
      {
        CallResult result = handler.Process(context);

        context.Response.Write(result.ToJson());
      }
      catch (Exception ex)
      {
        context.Response.Write("{ \"Success\": false, \"Message\":\"" + (ex.Message + " " + ex.StackTrace).Replace("\"", "\\\"") + "\" }");
        context.Response.End();
      }
    }
  }
}