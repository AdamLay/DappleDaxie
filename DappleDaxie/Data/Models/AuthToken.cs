using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DappleDaxie.Data.Models
{
  public class AuthToken : IBuildable<AuthToken>
  {
    public Guid Token { get; set; }
    public DateTime Expiry { get; set; }

    public AuthToken Build(SqlDataReader dr)
    {
      AuthToken result = new AuthToken();

      result.Token = Guid.Parse((string)dr["Token"]);
      result.Expiry = DateTime.Parse((string)dr["Expiry"]);

      return result;
    }
  }
}