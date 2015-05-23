using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DappleDaxie.Data.Models
{
  public class Parameter
  {
    public string Name { get; set; }
    public DbType type { get; set; }
    public object Value { get; set; }
  }
}