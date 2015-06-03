using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DappleDaxie.Data.Models
{
  public class Parameter
  {
    public Parameter(string name, DbType type, object value)
    {
      this.Name = name;
      this.Type = type;
      this.Value = value;
    }
    public Parameter()
    {

    }

    public string Name { get; set; }
    public DbType Type { get; set; }
    public object Value { get; set; }
  }
}