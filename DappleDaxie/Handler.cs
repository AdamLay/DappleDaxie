using DappleDaxie.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
      string name = context.Request.Form["Handler"];

      string hName = "DappleDaxie." + name + "Handler";

      IHandler handler = Handlers.First(m => m.GetType().FullName == hName);

      handler.Process(context);
    }
  }
}