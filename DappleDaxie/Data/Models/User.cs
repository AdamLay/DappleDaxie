using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DappleDaxie.Data.Models
{
  public class User : IBuildable<User>
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public User Build(SqlDataReader dr)
    {
      User result = new User();

      result.Id = (int)dr["Id"];
      result.Name = (string)dr["Name"];
      result.Password = (string)dr["Password"];

      return result;
    }
  }
}