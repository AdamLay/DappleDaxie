using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DappleDaxie
{
  public static class Extensions
  {
    public static void AddParam(this SqlCommand command, string name, DbType type, object value)
    {
      var param = command.CreateParameter();

      param.ParameterName = name;
      param.DbType = type;
      param.Value = value;

      command.Parameters.Add(param);
    }
  }
}