using DappleDaxie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DappleDaxie.Handlers
{
  public interface IHandler
  {
    CallResult Process(HttpContext context);
  }
}